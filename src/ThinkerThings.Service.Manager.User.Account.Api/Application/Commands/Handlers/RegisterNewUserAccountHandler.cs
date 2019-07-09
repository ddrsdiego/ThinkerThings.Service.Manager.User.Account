using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Commands;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Responses;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Validators;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Events;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Handlers
{
    public class RegisterNewUserAccountHandler : IRequestHandler<RegisterNewUserAccountCommand, Result<RegisterNewUserAccountResponse>>
    {
        private readonly IMediator _mediator;
        private readonly IUserAccountService _userAccountService;

        public RegisterNewUserAccountHandler(IMediator mediator, IUserAccountService userAccountService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _userAccountService = userAccountService ?? throw new ArgumentNullException(nameof(userAccountService));
        }

        public async Task<Result<RegisterNewUserAccountResponse>> Handle(RegisterNewUserAccountCommand request, CancellationToken cancellationToken)
        {
            var validarRequestResult = ValidarRequest(request);
            if (validarRequestResult.IsFailure)
                return Result<RegisterNewUserAccountResponse>.Fail(validarRequestResult.Messages);

            var verificarUsuarioJaCadastroResult = await VerificarUsuarioJaCadastrado(request);
            if (verificarUsuarioJaCadastroResult.IsFailure)
                return Result<RegisterNewUserAccountResponse>.Fail(verificarUsuarioJaCadastroResult.Messages);

            if (verificarUsuarioJaCadastroResult.Value != SituationRegistrationAccount.AccountNotRegistered)
                return Result<RegisterNewUserAccountResponse>.Fail(verificarUsuarioJaCadastroResult.Value.ToString());

            var novoUsuario = new UserAccount
            {
                Name = request.NomeUsuario,
                DocumentNumber = request.CpfUsuario,
                Email = request.EmailUsuario,
                CellPhoneNumber = request.TelefoneUsuario,
            };

            var registrarNovoUsuarioResult = await RegistrarNovoUsuario(novoUsuario).ConfigureAwait(false);
            if (registrarNovoUsuarioResult.IsFailure)
                return Result<RegisterNewUserAccountResponse>.Fail(registrarNovoUsuarioResult.Messages);

            _ = _mediator.Publish(new NewUserAccountRegisteredEvent(registrarNovoUsuarioResult.Value.UserAccountId), cancellationToken);

            return Result<RegisterNewUserAccountResponse>.Ok(CriarResponse(registrarNovoUsuarioResult.Value));
        }

        private static RegisterNewUserAccountResponse CriarResponse(UserAccount userAccount)
        {
            return new RegisterNewUserAccountResponse
            {
                UserAccountId = userAccount.UserAccountId,
                UserDocumentNumber = userAccount.DocumentNumber,
                UserEmail = userAccount.Email,
                UserName = userAccount.Name
            };
        }

        private static Result ValidarRequest(RegisterNewUserAccountCommand request)
        {
            if (request == null)
                return Result.Fail(nameof(request));

            var requestValidator = new RegisterNewUserAccountCommandValidator();
            var resultValidator = requestValidator.Validate(request);
            if (!resultValidator.IsValid)
                return Result.Fail(resultValidator.Errors.Select(x => x.ErrorMessage));

            return Result.Ok();
        }

        private async Task<Result<UserAccount>> RegistrarNovoUsuario(UserAccount userAccount)
        {
            try
            {
                var registrarNovoUsuarioResult = await _userAccountService.RegisterUserAccount(userAccount);
                if (registrarNovoUsuarioResult.IsFailure)
                    return Result<UserAccount>.Fail(registrarNovoUsuarioResult.Messages);

                return Result<UserAccount>.Ok(registrarNovoUsuarioResult.Value);
            }
            catch (Exception ex)
            {
                return Result<UserAccount>.Fail(ex.ToString());
            }
        }

        private async Task<Result<SituationRegistrationAccount>> VerificarUsuarioJaCadastrado(RegisterNewUserAccountCommand request)
        {
            try
            {
                var verificarUsuarioJaCadastroResult = await _userAccountService.CheckAccountAlreadyRegistered(request.CpfUsuario, request.EmailUsuario);
                if (verificarUsuarioJaCadastroResult.IsFailure)
                    return Result<SituationRegistrationAccount>.Fail(verificarUsuarioJaCadastroResult.Messages);

                return Result<SituationRegistrationAccount>.Ok(verificarUsuarioJaCadastroResult.Value);
            }
            catch (Exception ex)
            {
                return Result<SituationRegistrationAccount>.Fail(ex.ToString());
            }
        }
    }
}