using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class ServiceManagerUserAccountContainer
    {
        public static IServiceCollection AddServiceManagerUserAccount(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwagger();
            services.AddHandlers();
            services.AddServices();
            services.AddRepositories();
            services.AddOptions(configuration);

            return services;
        }
    }
}