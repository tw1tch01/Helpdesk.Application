using System.Net;
using System.Threading.Tasks;
using Helpdesk.Application.TicketLinks.Pings;
using Helpdesk.DomainModels.TicketLinks;
using Helpdesk.Services.TicketLinks.Results;
using Helpdesk.Services.TicketLinks.Results.Enums;
using Helpdesk.WebAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.TicketLinks.Controllers
{
    [Area(AreaNames.TicketLinks)]
    [ApiVersion(ApiConfig.CurrentVersion)]
    [ApiExplorerSettings(GroupName = AreaNames.TicketLinks)]
    public class TicketLinksController : AbstractController
    {
        private readonly IMediator _mediator;

        public TicketLinksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Link tickets
        /// </summary>
        /// <remarks>
        /// Link a ticket to another ticket
        /// </remarks>
        /// <returns>Action result</returns>
        /// <response code="200">Tickets were succesfully linked.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("link")]
        [ProducesResponseType(typeof(LinkTicketsResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LinkTicketsResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LinkTicket([FromBody] LinkTicket linkTickets)
        {
            var result = await _mediator.Send(new LinkTicketsPing(linkTickets));

            return result.Result switch
            {
                TicketsLinkResult.Linked => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Unlink tickets
        /// </summary>
        /// <remarks>
        /// Unlink a ticket from another ticket
        /// </remarks>
        /// <returns>Action result</returns>
        /// <response code="200">Tickets were succesfully unlinked.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("unlink")]
        [ProducesResponseType(typeof(LinkTicketsResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(LinkTicketsResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UnlinkTicket([FromBody] UnlinkTicket unlinkTickets)
        {
            var result = await _mediator.Send(new UnlinkTicketsPing(unlinkTickets));

            return result.Result switch
            {
                TicketsUnlinkResult.Unlinked => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}