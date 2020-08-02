using System;
using System.Net;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Results;
using Helpdesk.Services.Tickets.Results.Enums;
using Helpdesk.WebAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Tickets.Controllers
{
    [Area(AreaNames.Tickets)]
    [ApiVersion(ApiConfig.CurrentVersion)]
    [ApiExplorerSettings(GroupName = AreaNames.Tickets)]
    public class TicketActionsController : AbstractController
    {
        private readonly IMediator _mediator;

        public TicketActionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Close a ticket
        /// </summary>
        /// <remarks>
        /// Close a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully closed.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("close/{ticketId:int}")]
        [ProducesResponseType(typeof(CloseTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CloseTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CloseTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new CloseTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketCloseResult.Closed => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Pause a ticket
        /// </summary>
        /// <remarks>
        /// Pause a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully paused.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("pause/{ticketId:int}")]
        [ProducesResponseType(typeof(PauseTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(PauseTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PauseTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new PauseTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketPauseResult.Paused => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Reopen a ticket
        /// </summary>
        /// <remarks>
        /// Reopen a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully reopend.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("reopen/{ticketId:int}")]
        [ProducesResponseType(typeof(ReopenTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ReopenTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ReopenTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new ReopenTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketReopenResult.Reopened => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Resolve a ticket
        /// </summary>
        /// <remarks>
        /// Resolve a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully resolved.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("resolve/{ticketId:int}")]
        [ProducesResponseType(typeof(ResolveTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResolveTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResolveTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new ResolveTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketResolveResult.Resolved => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Start a ticket
        /// </summary>
        /// <remarks>
        /// Start a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully startd.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("start/{ticketId:int}")]
        [ProducesResponseType(typeof(StartTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(StartTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> StartTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new StartTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketStartResult.Started => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}