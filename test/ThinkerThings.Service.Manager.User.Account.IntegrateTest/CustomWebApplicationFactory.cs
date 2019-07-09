using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using ThinkerThings.Service.Manager.User.Account.Api;
using Microsoft.Extensions.DependencyInjection;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    internal sealed class CustomWebApplicationFactory<TEntryPoint> : WebApplicationFactory<Startup>
    {
        private const string ENVIRONMENT = "Test";
        private static CustomWebApplicationFactory<TEntryPoint> webApplicationFactory;

        private CustomWebApplicationFactory()
        {
        }

        public static CustomWebApplicationFactory<TEntryPoint> CreateWebApp()
        {
            webApplicationFactory = new CustomWebApplicationFactory<TEntryPoint>();
            webApplicationFactory.CreateDefaultClient();

            return webApplicationFactory;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(configureServices => 
                configureServices.AddScoped<IIntegratedTestRepository, IntegratedTestRepository>());

            base.ConfigureWebHost(builder);
        }

        protected override TestServer CreateServer(IWebHostBuilder builder)
        {
            builder.UseEnvironment(ENVIRONMENT);
            return base.CreateServer(builder);
        }
    }
}