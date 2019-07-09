using FluentAssertions;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Handlers;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.UnitTest.Application.Commands.Handlers
{
    [TestFixture]
    public class RegisterNewUserAccountHandlerTest
    {
        private IMediator mediator;
        private IUserAccountService userAccountService;

        [SetUp]
        public void SetUp()
        {
            mediator = Substitute.For<IMediator>();
            userAccountService = Substitute.For<IUserAccountService>();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_UserAccount_For_Nulo()
        {
            //Arrange
            var sut = new RegisterNewUserAccountHandler(mediator, userAccountService);

            //Act
            var response = await sut.Handle(null, CancellationToken.None).ConfigureAwait(false);

            //Assert
            response.Value.Should().BeNull();
            response.IsFailure.Should().BeTrue();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_UserAccount_For_Invalido()
        {
            //Arrange
            var sut = new RegisterNewUserAccountHandler(mediator, userAccountService);
            var command = FakerData.RegisterNewUserAccountCommandInvalid;

            //Act
            var response = await sut.Handle(command, CancellationToken.None).ConfigureAwait(false);

            //Assert
            response.Value.Should().BeNull();
            response.IsFailure.Should().BeTrue();
        }

        [Test]
        public async Task Deve_Retornar_Falha_Quando_UserAccount_For_Valido()
        {
            //Arrange
            var sut = new RegisterNewUserAccountHandler(mediator, userAccountService);
            var command = FakerData.RegisterNewUserAccountCommandValid;

            //Act
            var response = await sut.Handle(command, CancellationToken.None).ConfigureAwait(false);

            //Assert
            response.Value.Should().BeNull();
            response.IsFailure.Should().BeTrue();
        }

        [TearDown]
        public void TearDown()
        {
            mediator = null;
            userAccountService = null;
        }
    }
}