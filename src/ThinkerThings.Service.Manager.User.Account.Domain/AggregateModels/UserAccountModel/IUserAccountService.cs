using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel
{
    public interface IUserAccountService
    {
        Task<Result<UserAccount>> GetUserAccountById(int userAccountId);
        Task<Result<UserAccount>> RegisterUserAccount(UserAccount userAccount);
        Task<Result<SituationRegistrationAccount>> CheckAccountAlreadyRegistered(string documentNumber, string userEmail);
    }
}