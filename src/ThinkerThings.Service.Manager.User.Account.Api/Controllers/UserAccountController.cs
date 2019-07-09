using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Commands.Commands;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Queries;
using ThinkerThings.Service.Manager.User.Account.Api.Application.Queries.Responses;

namespace ThinkerThings.Service.Manager.User.Account.Api.Controllers
{
    [ApiController]
    [Route("api/user-account")]
    public class UserAccountController : Controller
    {
        private readonly IMediator _mediator;

        public UserAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, Route("{userAccountId}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(GetUserAccountByIdResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAccountById([FromRoute] int userAccountId)
        {
            var response = await _mediator.Send(new GetUserAccountByIdQuery(userAccountId)).ConfigureAwait(false);
            if (response.IsFailure)
                return BadRequest(response);

            if (response.Value.UserAccountId <= 0)
                return NotFound();

            return Ok(response.Value);
        }

        [HttpPost, Route("")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(string[]), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> RegisterNewUserAccount([FromBody] RegisterNewUserAccountCommand command)
        {
            if (command == null)
                return BadRequest(nameof(command));

            var response = await _mediator.Send(command).ConfigureAwait(false);
            if (response.IsFailure)
                return BadRequest(response);

            return Created("", response.Value);
        }
    }
}