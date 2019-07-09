using FluentValidation;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Queries;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Validators
{
    public class GetUserAccountByIdQueryValidator : AbstractValidator<GetUserAccountByIdQuery>
    {
        public GetUserAccountByIdQueryValidator()
        {
            RuleFor(query => query.UserAccountId).GreaterThan(0);
        }
    }
}