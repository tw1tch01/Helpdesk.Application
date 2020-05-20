using System.Net;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Results;
using Helpdesk.Services.Tickets.Results.Enums;
using Helpdesk.WebAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Tickets.Controllers
{
    [Area(AreaNames.Tickets)]
    [ApiVersion(ApiConfig.CurrentVersion)]
    public class TicketsController : AbstractController
    {
        private readonly TicketManagementService _managementService;
        private readonly TicketQueryService _queryService;

        public TicketsController(
            TicketManagementService managementService,
            TicketQueryService queryService)
        {
            _managementService = managementService;
            _queryService = queryService;
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
            var result = await _managementService.Open(newTicket);

            return result.Result switch
            {
                TicketOpenResult.Opened => Created($"/tickets/{result.TicketId.Value}", result),
                _ => BadRequest(result),
            };
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
            var result = await _managementService.Open(newTicket);

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
        public async Task<IActionResult> DeleteTicket([FromRoute]int ticketId)
        {
            var result = await _managementService.Delete(ticketId);

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
        public async Task<IActionResult> UpdateTicket([FromRoute]int ticketId, [FromBody] UpdateTicketDto updateTicket)
        {
            var result = await _managementService.Update(ticketId, updateTicket);

            return result.Result switch
            {
                TicketUpdateResult.Updated => Ok(result),
                _ => BadRequest(result)
            };
        }
    }
}