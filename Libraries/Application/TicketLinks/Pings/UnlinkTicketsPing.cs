using Helpdesk.DomainModels.TicketLinks;
using Helpdesk.Services.TicketLinks.Results;
using MediatR;

namespace Helpdesk.Application.TicketLinks.Pings
{
    public class UnlinkTicketsPing : IRequest<UnlinkTicketsResult>
    {
        public UnlinkTicketsPing(UnlinkTicket unlinkTickets)
        {
            UnlinkTickets = unlinkTickets;
        }

        public UnlinkTicket UnlinkTickets { get; }
    }
}