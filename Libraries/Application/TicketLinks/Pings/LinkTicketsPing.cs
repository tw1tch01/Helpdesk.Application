using Helpdesk.DomainModels.TicketLinks;
using Helpdesk.Services.TicketLinks.Results;
using MediatR;

namespace Helpdesk.Application.TicketLinks.Pings
{
    public class LinkTicketsPing : IRequest<LinkTicketsResult>
    {
        public LinkTicketsPing(LinkTicket linkTickets)
        {
            LinkTickets = linkTickets;
        }

        public LinkTicket LinkTickets { get; }
    }
}