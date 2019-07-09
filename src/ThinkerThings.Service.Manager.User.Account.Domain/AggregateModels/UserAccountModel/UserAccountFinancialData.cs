using System;

namespace ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel
{
    public class UserAccountFinancialData
    {
        public int UserAccountId { get; set; }
        public DateTimeOffset CreationDate { get; } = DateTimeOffset.Now;
        public DateTimeOffset LasUpdateDate { get; set; }
    }
}