using System;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel
{
    public class UserAccount : IAggregateRoot
    {
        public static UserAccount Default() => new UserAccount();

        public int UserAccountId { get; set; }
        public string DocumentNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhoneNumber { get; set; }
        public string ParticularPhoneNumber { get; set; }

        public UserAccountFinancialData FinancialData{ get; set; }

        public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
        public DateTimeOffset LastUpdateDate { get; set; }
    }
}