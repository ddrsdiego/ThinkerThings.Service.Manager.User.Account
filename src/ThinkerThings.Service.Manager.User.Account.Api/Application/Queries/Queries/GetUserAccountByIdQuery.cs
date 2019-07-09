using MediatR;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Responses;
using ThinkerThings.Service.Manager.User.Account.Domain.SeedWorks;

namespace ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Queries
{
    public class GetUserAccountByIdQuery : IRequest<Result<GetUserAccountByIdResponse>>
    {
        public GetUserAccountByIdQuery(int userAccountId)
        {
            UserAccountId = userAccountId;
        }

        public int UserAccountId { get; }
    }
}