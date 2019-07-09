using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;
using ThinkerThings.Service.Manager.User.Account.Infra.Repositories;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class RepositoriesContainer
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountRepository, UserAccountSqlServerRepository>();
            return services;
        }
    }
}