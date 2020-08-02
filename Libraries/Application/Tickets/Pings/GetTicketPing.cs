using Helpdesk.DomainModels.Tickets;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class GetTicketPing : IRequest<TicketDetails>
    {
        public GetTicketPing(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketId { get; }
    }
}