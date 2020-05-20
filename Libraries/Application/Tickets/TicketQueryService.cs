using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Common;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Queries.GetTicket;
using Helpdesk.Services.Tickets.Queries.LookupTickets;

namespace Helpdesk.Application.Tickets
{
    public class TicketQueryService
    {
        private readonly IGetTicketService _getTicketService;
        private readonly ILookupTicketsService _lookupTicketsService;

        public TicketQueryService(
            IGetTicketService getTicketService,
            ILookupTicketsService lookupTicketsService)
        {
            _getTicketService = getTicketService;
            _lookupTicketsService = lookupTicketsService;
        }

        public Task<FullTicketDetails> GetTicketDetails(int ticketId) => _getTicketService.Get(ticketId);

        public Task<IList<TicketLookup>> LookupTickets(TicketLookupParams @params) => _lookupTicketsService.Lookup(@params);

        public Task<PagedCollection<TicketLookup>> PagedCollection(int page, int pageSize, TicketLookupParams @params) => _lookupTicketsService.PagedLookup(page, pageSize, @params);
    }
}