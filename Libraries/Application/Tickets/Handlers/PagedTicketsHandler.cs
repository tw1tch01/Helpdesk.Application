using System.Threading;
using System.Threading.Tasks;
using Data.Common;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Queries.LookupTickets;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class PagedTicketsHandler : IRequestHandler<PagedTicketsPing, PagedCollection<TicketLookup>>
    {
        private readonly ILookupTicketsService _lookupTicketsService;

        public PagedTicketsHandler(ILookupTicketsService lookupTicketsService)
        {
            _lookupTicketsService = lookupTicketsService;
        }

        public async Task<PagedCollection<TicketLookup>> Handle(PagedTicketsPing request, CancellationToken cancellationToken)
        {
            return await _lookupTicketsService.PagedLookup(request.Page, request.PageSize, request.Parameters);
        }
    }
}