using FluentValidation;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Events;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Events.Validators
{
    public class NewUserAccountRegisteredEventValidator : AbstractValidator<NewUserAccountRegisteredEvent>
    {
    }
}