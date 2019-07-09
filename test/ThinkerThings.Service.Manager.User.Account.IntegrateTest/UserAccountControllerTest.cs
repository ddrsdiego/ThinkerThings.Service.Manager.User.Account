using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Responses;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.IntegrateTest
{
    public class UserAccountControllerHttpStatusCodeOkTest : BaseIntegrationTest
    {
        IEnumerable<UserAccount> userAccounts;
        private IUserAccountRepository userAccountRepository;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            userAccountRepository = ServiceProvider.GetRequiredService<IUserAccountRepository>();

            userAccounts = await userAccountRepository.GetUserAccounts().ConfigureAwait(false);
            foreach (var account in userAccounts.ToList())
                await userAccountRepository.DeleteUserAccount(account.UserAccountId).ConfigureAwait(false);

            userAccounts = FakerData.UserAccountsValid;
            foreach (var account in userAccounts)
                await userAccountRepository.RegisterUserAccount(account).ConfigureAwait(false);
        }

        [Test]
        public async Task HttpStatusCodeOK()
        {
            var userAccount = await userAccountRepository.GetUserAccountByEmail(userAccounts.Last().Email).ConfigureAwait(false);

            var response = await Client.GetAsync(UserAccountScenarios.Get.GetUserAccountById(userAccount.UserAccountId)).ConfigureAwait(false);

            var getUserAccountByIdResponse = await response.Content.ReadAsAsync<GetUserAccountByIdResponse>();
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            getUserAccountByIdResponse.UserAccountId.Should().Be(userAccount.UserAccountId);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            userAccountRepository = null;
        }
    }

    public class UserAccountControllerHttpStatusCodeNotFoundTest : BaseIntegrationTest
    {
        IEnumerable<UserAccount> userAccounts;
        private IUserAccountRepository userAccountRepository;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            userAccountRepository = ServiceProvider.GetRequiredService<IUserAccountRepository>();

            userAccounts = await userAccountRepository.GetUserAccounts().ConfigureAwait(false);
            foreach (var account in userAccounts.ToList())
                await userAccountRepository.DeleteUserAccount(account.UserAccountId).ConfigureAwait(false);

            userAccounts = FakerData.UserAccountsValid;
            foreach (var account in userAccounts)
                await userAccountRepository.RegisterUserAccount(account).ConfigureAwait(false);
        }

        [Test]
        public async Task HttpStatusCodeNotFound()
        {
            var userAccount = FakerData.UserAccountValid;
            var response = await Client.GetAsync($"api/user-account/{userAccount.UserAccountId}").ConfigureAwait(false);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            userAccountRepository = null;
        }
    }

    internal static class FakerData
    {
        private const string LOCALE = "pt_BR";

        public static List<UserAccount> UserAccountsValid
        {
            get
            {
                List<UserAccount> userAccounts = new List<UserAccount>();

                for (int i = 1; i < 100; i++)
                {
                    var userAccount = new Faker<UserAccount>(LOCALE)
                                            .RuleFor(x => x.Name, f => f.Person.FullName)
                                            .RuleFor(x => x.DocumentNumber, f => f.Person.Cpf())
                                            .RuleFor(x => x.Email, f => f.Person.Email)
                                            .RuleFor(x => x.CellPhoneNumber, f => f.Person.Phone);

                    userAccounts.Add(userAccount);
                }

                return userAccounts;
            }
        }

        public static UserAccount UserAccountValid
        {
            get
            {
                return new Faker<UserAccount>(LOCALE)
                    .RuleFor(x => x.UserAccountId, f => f.Random.Int(1, 100))
                    .RuleFor(x => x.Name, f => f.Person.FullName)
                    .RuleFor(x => x.DocumentNumber, f => f.Person.Cpf())
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.CellPhoneNumber, f => f.Person.Phone);
            }
        }
    }

    internal static class UserAccountScenarios
    {
        private const string BASE_URL = "api/user-account";

        public static class Get
        {
            public static string GetUserAccountById(int userAccountId) 
                => $"{BASE_URL}/{userAccountId.ToString()}";
        }
    }
}