using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Services;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.UnitTest.Application.Services
{
    [TestFixture]
    public class UserAccountServiceTest
    {
        private IUserAccountRepository userAccountRepository;

        [SetUp]
        public void SetUp()
        {
            userAccountRepository = Substitute.For<IUserAccountRepository>();
        }

        [Test]
        public async Task Deve_Retornar_Sucesso_Quando_Repositorio_Registrar_UserAccount()
        {
            //Arragne
            var userAccount = FakeData.UserAccountValid;

            userAccountRepository.GetUserAccountByEmail(Arg.Any<string>())
                .Returns(_ => Task.FromResult(userAccount));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var response = await sut.RegisterUserAccount(userAccount);

            //Assert
            await userAccountRepository.Received(1).RegisterUserAccount(Arg.Any<UserAccount>());

            response.IsSuccess.Should().BeTrue();
            response.IsFailure.Should().BeFalse();
            response.Value.Should().NotBeNull();
            response.Value.UserAccountId.Should().Be(userAccount.UserAccountId);
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_UserAccount_Nao_Localizado()
        {
            //Arragne
            var userAccount = FakeData.UserAccountValid;

            userAccountRepository.GetUserAccountByEmail(Arg.Any<string>())
                .Returns(_ => Task.FromResult<UserAccount>(null));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var response = await sut.RegisterUserAccount(userAccount);

            //Assert
            await userAccountRepository.Received(1).RegisterUserAccount(Arg.Any<UserAccount>());

            response.IsFailure.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
            response.Value.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_Repositorio_Lancar_Excessao_Ao_Consultar()
        {
            //Arragne
            var userAccount = FakeData.UserAccountValid;
            userAccountRepository.GetUserAccountByEmail(Arg.Any<string>())
                .Returns(_ => Task.FromException(new Exception()));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var response = await sut.RegisterUserAccount(userAccount);

            //Assert
            await userAccountRepository.Received(1).RegisterUserAccount(Arg.Any<UserAccount>());

            response.IsFailure.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
            response.Value.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_Repositorio_Lancar_Excessao_Ao_Registrar()
        {
            //Arragne
            var userAccount = FakeData.UserAccountValid;
            userAccountRepository.RegisterUserAccount(Arg.Any<UserAccount>())
                .Returns(Task.FromException<Exception>(new Exception()));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var response = await sut.RegisterUserAccount(userAccount);

            //Assert
            response.IsFailure.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
            response.Value.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_Registrar_UserAccount_For_Nulo()
        {
            //Arragne
            var sut = new UserAccountService(userAccountRepository);

            //Act
            var response = await sut.RegisterUserAccount(null);

            //Assert
            response.IsFailure.Should().BeTrue();
            response.IsSuccess.Should().BeFalse();
            response.Value.Should().BeNull();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_Repositorio_Lancar_Excessao()
        {
            //Arrange
            const int USERACCOUNTID = 1;

            userAccountRepository.GetUserAccountById(Arg.Any<int>())
                .Returns(_ => Task.FromException(new Exception()));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var result = await sut.GetUserAccountById(USERACCOUNTID);

            //Assert
            Assert.IsNull(result.Value);
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
        }

        [Test]
        public async Task Deve_Retornar_Sucesso_Quando_Repositorio_Retornar_Usuario_Valido()
        {
            //Arrange
            const int USERACCOUNTID = 1;

            userAccountRepository.GetUserAccountById(Arg.Any<int>())
                .Returns(_ => Task.FromResult(new UserAccount { UserAccountId = USERACCOUNTID }));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            var result = await sut.GetUserAccountById(USERACCOUNTID);

            //Assert
            Assert.IsNotNull(result.Value);
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.UserAccountId.Should().Be(USERACCOUNTID);
        }

        [Test]
        public async Task Deve_Retornar_Sucesso_Quando_Repositorio_Retornar_Consulta_Nula()
        {
            //Arrange
            userAccountRepository.GetUserAccountById(Arg.Any<int>())
                .Returns(_ => Task.FromResult<UserAccount>(null));

            var sut = new UserAccountService(userAccountRepository);

            //Act
            const int USERACCOUNTID = 1;
            var result = await sut.GetUserAccountById(USERACCOUNTID);

            //Assert
            result.Value.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.UserAccountId.Should().BeLessOrEqualTo(0);
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_UserAccountId_For_Igual_Zero()
        {
            //Arrange
            var sut = new UserAccountService(userAccountRepository);

            //Act
            var result = await sut.GetUserAccountById(0);

            //Assert
            Assert.IsNull(result.Value);
            result.IsFailure.Should().BeTrue();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_Repositorio_Estiver_Estado_Invald()
        {
            //Arrange
            userAccountRepository = null;
            var sut = new UserAccountService(userAccountRepository);

            //Act
            var result = await sut.GetUserAccountById(1);

            //Assert
            Assert.IsNull(result.Value);
            result.IsFailure.Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            userAccountRepository = null;
        }
    }
}