using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using ThinkerThings.Service.Manager.User.Account.Api;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    public class WebAppTest<TEntryPoint>
        where TEntryPoint : class
    {
        private static CustomWebApplicationFactory<TEntryPoint> webApplicationFactory;

        public WebAppTest()
        {
            webApplicationFactory = CustomWebApplicationFactory<TEntryPoint>.CreateWebApp();
        }

        public HttpClient HttpClient => webApplicationFactory.CreateDefaultClient();
        public IServiceScope ServiceScope => webApplicationFactory.Server.Host.Services.CreateScope();
    }

    public static class WebAppTestEx
    {
        public static WebAppTest<Startup> EnsureCreateDataBase(this WebAppTest<Startup> webAppTest)
        {
            using (var scope = webAppTest.ServiceScope)
            {
                var serviceProvider = scope.ServiceProvider;

                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var respository = serviceProvider.GetRequiredService<IIntegratedTestRepository>();

                respository.CreateDataBase().GetAwaiter().GetResult();
            }

            return webAppTest;
        }
    }
}