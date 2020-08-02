using System.Collections.Generic;
using Helpdesk.DomainModels.Tickets;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class LookupTicketsPing : IRequest<IList<TicketLookup>>
    {
        public LookupTicketsPing(TicketLookupParams parameters)
        {
            Parameters = parameters;
        }

        public TicketLookupParams Parameters { get; }
    }
}