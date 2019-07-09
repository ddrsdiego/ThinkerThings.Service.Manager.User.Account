using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ThinkerThings.Service.Manager.User.Account.Api.IoC
{
    public static class SwaggerContainerEx
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "ThinkerThings.Service.Manager.User.Account.Api",
                    Version = "v1",
                    Description = "Api para forneceer prateleira e produtos da Easynvest",
                    Contact = new Contact
                    {
                        Url = "https://bitbucket.org/easynvest/easynvest.orders.FixedIncome"
                    }
                });
                options.AddSecurityDefinition("bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "Favor gerar um token jwt para efetuar a requisição",
                    Name = "Authorization",
                    Type = "apiKey"
                });
            });

            return services;
        }
    }
}