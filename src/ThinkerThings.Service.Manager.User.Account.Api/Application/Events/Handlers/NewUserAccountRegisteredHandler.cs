using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Events;
using ThinkerThings.Service.Manager.User.Account.Domain.AggregateModels.UserAccountModel;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Handlers
{
    public class NewUserAccountRegisteredHandler : INotificationHandler<NewUserAccountRegisteredEvent>
    {
        private readonly IUserAccountService _userAccountService;

        public NewUserAccountRegisteredHandler(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public async Task Handle(NewUserAccountRegisteredEvent notification, CancellationToken cancellationToken)
        {
            var getUserAccountByIdResult = await _userAccountService.GetUserAccountById(notification.UserAccountId);
            if (getUserAccountByIdResult.IsFailure)
            {
            }

            //Send Event
        }
    }
}