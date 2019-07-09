using Bogus;
using Bogus.Extensions.Brazil;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.UnitTest
{
    internal static class FakeData
    {
        public static UserAccount UserAccountValid
        {
            get
            {
                const string LOCALE = "pt_BR";

                return new Faker<UserAccount>(LOCALE)
                    .RuleFor(x => x.UserAccountId, f => f.Random.Int(1, 100))
                    .RuleFor(x => x.Name, f => f.Person.FullName)
                    .RuleFor(x => x.Name, f => f.Person.Cpf())
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.CellPhoneNumber, f => f.Person.Phone);
            }
        }
    }
}