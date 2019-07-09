using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net.Http;
using ThinkerThings.Service.Manager.User.Account.Api;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    public abstract class BaseIntegrationTest
    {
        private IServiceScope scope;
        protected IServiceProvider ServiceProvider;
        protected HttpClient Client => WebApp.HttpClient;

        public static WebAppTest<Startup> WebApp;

        [OneTimeSetUp]
        public void BaseIntegrationOneTimeSetUp()
        {
            scope = WebApp.ServiceScope;
            ServiceProvider = scope.ServiceProvider;
        }

        [OneTimeTearDown]
        public void BaseIntegrationOneTimeTearDown() => scope?.Dispose();
    }
}