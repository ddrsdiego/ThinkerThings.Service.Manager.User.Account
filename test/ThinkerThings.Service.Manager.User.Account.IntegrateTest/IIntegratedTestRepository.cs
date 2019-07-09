using System.Threading.Tasks;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    public interface IIntegratedTestRepository
    {
        Task CreateDataBase();
    }

    public class IntegratedTestRepository : IIntegratedTestRepository
    {
        public async Task CreateDataBase()
        {
            await Task.CompletedTask;
        }
    }
}