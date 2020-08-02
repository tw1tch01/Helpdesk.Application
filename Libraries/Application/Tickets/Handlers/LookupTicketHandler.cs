using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Queries.LookupTickets;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class LookupTicketHandler : IRequestHandler<LookupTicketsPing, IList<TicketLookup>>
    {
        private readonly ILookupTicketsService _lookupTicketsService;

        public LookupTicketHandler(ILookupTicketsService lookupTicketsService)
        {
            _lookupTicketsService = lookupTicketsService;
        }

        public async Task<IList<TicketLookup>> Handle(LookupTicketsPing request, CancellationToken cancellationToken)
        {
            return await _lookupTicketsService.Lookup(request.Parameters);
        }
    }
}