using System.Net;
using System.Threading.Tasks;
using Helpdesk.Application.Authentication.Pings;
using Helpdesk.WebAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Authentication.Controllers
{
    [ApiController]
    [ApiVersion(ApiConfig.CurrentVersion)]
    [ApiExplorerSettings(GroupName = AreaNames.Tickets)]
    [Area(AreaNames.Security)]
    [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [Route("api/[area]")]
    public class TokenController : Controller
    {
        private readonly IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Generate a JWT token
        /// </summary>
        /// <remarks>
        /// Genereates an autherized JWT token
        /// </remarks>
        /// <returns>Action result</returns>
        /// <response code="200"></response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("generate")]
        //[ProducesResponseType(typeof(CloseTicketResult), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(CloseTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Generate()
        {
            var result = await _mediator.Send(new AuthenticatePing());
            return Ok(result);
        }
    }
}