using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Handlers;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Handlers;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class HandlerContainer
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetUserAccountByIdHandler).Assembly);
            services.AddMediatR(typeof(RegisterNewUserAccountHandler).Assembly);

            return services;
        }
    }
}