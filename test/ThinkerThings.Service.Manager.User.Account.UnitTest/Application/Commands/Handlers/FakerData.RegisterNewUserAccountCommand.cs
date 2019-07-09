using Bogus;
using Bogus.Extensions.Brazil;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Commands;

namespace ThinkerThings.Service.Manager.User.Account.UnitTest.Application.Commands.Handlers
{
    internal static class FakerData
    {
        public static RegisterNewUserAccountCommand RegisterNewUserAccountCommandInvalid
        {
            get
            {
                const string LOCALE = "pt_BR";

                return new Faker<RegisterNewUserAccountCommand>(LOCALE)
                    .CustomInstantiator(f => new RegisterNewUserAccountCommand(nomeUsuario: string.Empty,
                                                                               emailUsuario: string.Empty,
                                                                               cpfUsuario: string.Empty,
                                                                               telefoneUsuario: string.Empty));
            }
        }

        public static RegisterNewUserAccountCommand RegisterNewUserAccountCommandValid
        {
            get
            {
                const string LOCALE = "pt_BR";

                return new Faker<RegisterNewUserAccountCommand>(LOCALE)
                    .CustomInstantiator(f => new RegisterNewUserAccountCommand(nomeUsuario: f.Person.FullName,
                                                                               emailUsuario: f.Person.Email,
                                                                               cpfUsuario: f.Person.Cpf(),
                                                                               telefoneUsuario: f.Person.Phone));
            }
        }

        public static RegisterNewUserAccountCommand RegisterNewUserAccountCommandWithoutUserName
        {
            get
            {
                const string LOCALE = "pt_BR";

                return new Faker<RegisterNewUserAccountCommand>(LOCALE)
                    .CustomInstantiator(f => new RegisterNewUserAccountCommand(nomeUsuario: string.Empty,
                                                                               emailUsuario: f.Person.Email,
                                                                               cpfUsuario: f.Person.Cpf(),
                                                                               telefoneUsuario: f.Person.Phone));
            }
        }
    }
}