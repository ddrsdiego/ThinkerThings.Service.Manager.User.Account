using System.Collections.Generic;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel
{
    public interface IUserAccountRepository : IRepository<UserAccount>
    {
        Task<IEnumerable<UserAccount>> GetUserAccounts();
        Task DeleteUserAccount(int userAccount);

        Task RegisterUserAccount(UserAccount userAccount);
        Task<UserAccount> GetUserAccountById(int userAccountId);
        Task<UserAccount> GetUserAccountByEmail(string userAccountEmail);
        Task<UserAccount> GetUserAccountByDocumentNumber(string documentNumber);
    }
}