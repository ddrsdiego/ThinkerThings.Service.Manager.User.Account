using MediatR;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Events
{
    public class NewUserAccountRegisteredEvent : INotification
    {
        public NewUserAccountRegisteredEvent(int userAccountId)
        {
            UserAccountId = userAccountId;
        }

        public int UserAccountId { get; }
    }
}