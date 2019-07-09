using FluentValidation;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Commands;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Validators
{
    public class RegisterNewUserAccountCommandValidator : AbstractValidator<RegisterNewUserAccountCommand>
    {
        public RegisterNewUserAccountCommandValidator()
        {
            RuleFor(command => command.NomeUsuario).NotEmpty().Length(3, 30);
            RuleFor(command => command.CpfUsuario).NotEmpty();
            RuleFor(command => command.TelefoneUsuario).NotEmpty();
            RuleFor(command => command.EmailUsuario).NotEmpty().EmailAddress();
        }
    }
}