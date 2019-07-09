using Microsoft.EntityFrameworkCore;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.Infra.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
    }
}