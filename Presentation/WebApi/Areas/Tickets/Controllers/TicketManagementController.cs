using System;
using System.Net;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Results;
using Helpdesk.Services.Tickets.Results.Enums;
using Helpdesk.WebAPI.Common;
using Helpdesk.WebAPI.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;

namespace Helpdesk.WebAPI.Areas.Tickets.Controllers
{
    [Area(AreaNames.Tickets)]
    [ApiVersion(ApiConfig.CurrentVersion)]
    [ApiExplorerSettings(GroupName = AreaNames.Tickets)]
    [Authorize(PolicyNames.Management)]
    public class TicketManagementController : AbstractController
    {
        private readonly IMediator _mediator;

        public TicketManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Open a ticket
        /// </summary>
        /// <remarks>
        /// Open a new Ticket
        /// </remarks>
        /// <param name="newTicket">Details of the new ticket.</param>
        /// <returns>Action result</returns>
        /// <response code="201">Ticket was succesfully opened.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPost("open")]
        [ProducesResponseType(typeof(OpenTicketResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(OpenTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> OpenTicket([FromBody] NewTicket newTicket)
        {
            var result = await _mediator.Send(new OpenTicketPing(newTicket));

            return result.Result switch
            {
                TicketOpenResult.Opened => Created($"/tickets/{result.TicketId.Value}", result),
                _ => BadRequest(result),
            };
        }

        /// <summary>
        /// Delete a ticket
        /// </summary>
        /// <remarks>
        /// Delete a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully deleted.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpDelete("delete/{ticketId:int}")]
        [ProducesResponseType(typeof(DeleteTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(DeleteTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteTicket([FromRoute] int ticketId)
        {
            var result = await _mediator.Send(new DeleteTicketPing(ticketId, Guid.Empty));

            return result.Result switch
            {
                TicketDeleteResult.Deleted => Ok(result),
                _ => BadRequest(result)
            };
        }

        /// <summary>
        /// Update a ticket
        /// </summary>
        /// <remarks>
        /// Update a Ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket</param>
        /// <param name="updateTicket">Updated details for the ticket</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully updated.</response>
        /// <response code="400">Request was not valid.</response>
        [HttpPatch("update/{ticketId:int}")]
        [ProducesResponseType(typeof(UpdateTicketResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UpdateTicketResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTicket([FromRoute] int ticketId, [FromBody] EditTicket updateTicket)
        {
            var result = await _mediator.Send(new UpdateTicketPing(ticketId, updateTicket));

            return result.Result switch
            {
                TicketUpdateResult.Updated => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}