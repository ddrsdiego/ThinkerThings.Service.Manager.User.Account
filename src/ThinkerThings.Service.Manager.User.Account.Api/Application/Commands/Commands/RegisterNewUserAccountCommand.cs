using MediatR;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Responses;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Commands
{
    public class RegisterNewUserAccountCommand : IRequest<Result<RegisterNewUserAccountResponse>>
    {
        public RegisterNewUserAccountCommand(string nomeUsuario, string emailUsuario, string cpfUsuario, string telefoneUsuario)
        {
            NomeUsuario = nomeUsuario;
            EmailUsuario = emailUsuario;
            CpfUsuario = cpfUsuario;
            TelefoneUsuario = telefoneUsuario;
        }

        public string NomeUsuario { get; }
        public string EmailUsuario { get; }
        public string CpfUsuario { get; }
        public string TelefoneUsuario { get; }
    }
}