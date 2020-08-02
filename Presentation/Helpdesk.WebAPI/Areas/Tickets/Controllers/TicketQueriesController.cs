using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Data.Common;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Domain.Tickets.Enums;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.DomainModels.Tickets.Enums;
using Helpdesk.WebAPI.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Helpdesk.WebAPI.Areas.Tickets.Controllers
{
    [Area(AreaNames.Tickets)]
    [ApiVersion(ApiConfig.CurrentVersion)]
    [ApiExplorerSettings(GroupName = AreaNames.Tickets)]
    public class TicketQueriesController : AbstractController
    {
        private readonly IMediator _mediator;

        public TicketQueriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get a ticket
        /// </summary>
        /// <remarks>
        /// Get the full details for a ticket
        /// </remarks>
        /// <param name="ticketId">Unique identifier of the ticket</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket details.</response>
        [HttpGet("{ticketId:int}")]
        [ProducesResponseType(typeof(TicketDetails), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTicket([FromRoute] int ticketId)
        {
            var details = await _mediator.Send(new GetTicketPing(ticketId));

            return details switch
            {
                null => NotFound(),
                _ => Ok(details)
            };
        }

        /// <summary>
        /// Lookup tickets
        /// </summary>
        /// <remarks>
        /// Lookup a set of tickets
        /// </remarks>
        /// <param name="createdAfter">Filter to tickets created after specified date</param>
        /// <param name="createdBefore">Filter to tickets created before specified date</param>
        /// <param name="searchyBy">Filter to tickets whose Name contains the specified term</param>
        /// <param name="ticketIds">Filter to only the tickets whose TicketId is contained within specified ticketIds</param>
        /// <param name="filterByStatus">Filter to tickets whose status match the specified status</param>
        /// <param name="filterBySeverity">Filter to tickets whose severity match the specified severity</param>
        /// <param name="filterByPriority">Filter to tickets whose priority match the specified priority</param>
        /// <param name="sortBy">Sort the tickets by the specified value</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully opened.</response>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IList<TicketLookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LookupTickets(
            DateTimeOffset? createdAfter = null,
            DateTimeOffset? createdBefore = null,
            string searchyBy = null,
            string ticketIds = null,
            TicketStatus? filterByStatus = null,
            Severity? filterBySeverity = null,
            Priority? filterByPriority = null,
            SortTicketsBy? sortBy = null)
        {
            var @params = new TicketLookupParams
            {
                CreatedAfter = createdAfter,
                CreatedBefore = createdBefore,
                SearchBy = searchyBy,
                TicketIds = DecodeParameterList<int>(ticketIds),
                FilterByStatus = filterByStatus,
                FilterBySeverity = filterBySeverity,
                FilterByPriority = filterByPriority,
                SortBy = sortBy
            };

            return Ok(await _mediator.Send(new LookupTicketsPing(@params)));
        }

        /// <summary>
        /// Paged tickets lookup
        /// </summary>
        /// <remarks>
        /// Page through a set of tickets
        /// </remarks>
        /// <param name="createdAfter">Filter to tickets created after specified date</param>
        /// <param name="createdBefore">Filter to tickets created before specified date</param>
        /// <param name="searchyBy">Filter to tickets whose Name contains the specified term</param>
        /// <param name="ticketIds">Filter to only the tickets whose TicketId is contained within specified ticketIds</param>
        /// <param name="filterByStatus">Filter to tickets whose status match the specified status</param>
        /// <param name="filterBySeverity">Filter to tickets whose severity match the specified severity</param>
        /// <param name="filterByPriority">Filter to tickets whose priority match the specified priority</param>
        /// <param name="sortBy">Sort the tickets by the specified value</param>
        /// <returns>Action result</returns>
        /// <response code="200">Ticket was succesfully opened.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedCollection<TicketLookup>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PagedTicketLookup(
            DateTimeOffset? createdAfter = null,
            DateTimeOffset? createdBefore = null,
            string searchyBy = null,
            string ticketIds = null,
            TicketStatus? filterByStatus = null,
            Severity? filterBySeverity = null,
            Priority? filterByPriority = null,
            SortTicketsBy? sortBy = null)
        {
            (int page, int pageSize) = GetPagination();

            var @params = new TicketLookupParams
            {
                CreatedAfter = createdAfter,
                CreatedBefore = createdBefore,
                SearchBy = searchyBy,
                TicketIds = DecodeParameterList<int>(ticketIds),
                FilterByStatus = filterByStatus,
                FilterBySeverity = filterBySeverity,
                FilterByPriority = filterByPriority,
                SortBy = sortBy
            };

            return Ok(await _mediator.Send(new PagedTicketsPing(page, pageSize, @params)));
        }
    }
}