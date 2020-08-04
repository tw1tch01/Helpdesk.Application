using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class CloseTicketPing : IRequest<CloseTicketResult>
    {
        public CloseTicketPing(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketId { get; }
    }
}