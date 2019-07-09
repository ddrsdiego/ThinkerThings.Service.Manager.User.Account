using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Services;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class ServicesContainer
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccountService, UserAccountService>();
            return services;
        }
    }
}