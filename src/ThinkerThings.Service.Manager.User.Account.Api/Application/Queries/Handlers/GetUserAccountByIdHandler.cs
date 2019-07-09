using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Queries;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Responses;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Validators;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Handlers
{
    public class GetUserAccountByIdHandler : IRequestHandler<GetUserAccountByIdQuery, Result<GetUserAccountByIdResponse>>
    {
        private readonly IUserAccountService _userAccountService;

        public GetUserAccountByIdHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task<Result<GetUserAccountByIdResponse>> Handle(GetUserAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var validateRequestResult = ValidateRequest(request);
            if (validateRequestResult.IsFailure)
                return Result<GetUserAccountByIdResponse>.Fail(validateRequestResult.Messages);

            var getUserAccountByIdResult = await _userAccountService.GetUserAccountById(request.UserAccountId);
            if (getUserAccountByIdResult.IsFailure)
                return Result<GetUserAccountByIdResponse>.Fail(getUserAccountByIdResult.Messages);

            return Result<GetUserAccountByIdResponse>.Ok(CreateResponse(getUserAccountByIdResult.Value));
        }

        private Result ValidateRequest(GetUserAccountByIdQuery request)
        {
            var validator = new GetUserAccountByIdQueryValidator();

            var resultValidator = validator.Validate(request);
            if (!resultValidator.IsValid)
                return Result.Fail(resultValidator.Errors.Select(x => x.ErrorMessage));

            return Result.Ok();
        }

        private static GetUserAccountByIdResponse CreateResponse(UserAccount userAccount)
        {
            return new GetUserAccountByIdResponse
            {
                UserAccountId = userAccount.UserAccountId,
                UserCellPhoneNumber = userAccount.CellPhoneNumber,
                UserDocumentNumber = userAccount.DocumentNumber,
                UserEmail = userAccount.Email,
                UserName = userAccount.Name
            };
        }
    }
}