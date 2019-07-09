using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Service.Manager.User.Account.Infra.Options;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class OptionsContainer
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOption>(connectionStringOptions =>
                connectionStringOptions.ConnectionString = configuration.GetConnectionString("API_USER_ACCOUNT"));

            return services;
        }
    }
}