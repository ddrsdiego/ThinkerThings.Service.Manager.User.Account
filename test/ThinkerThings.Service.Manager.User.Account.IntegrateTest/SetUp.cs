using NUnit.Framework;
using ThinkerThings.Service.Manager.User.Account.Api;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    [SetUpFixture]
    public class SetUp
    {
        [OneTimeSetUp]
        public void OneTimeSetUp() =>
            BaseIntegrationTest.WebApp = new WebAppTest<Startup>()
                                            .EnsureCreateDataBase();

        [OneTimeTearDown]
        public void OneTimeTearDown() => BaseIntegrationTest.WebApp = null;
    }
}