using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class OpenTicketPing : IRequest<OpenTicketResult>
    {
        public OpenTicketPing(NewTicket newTicket)
        {
            NewTicket = newTicket;
        }

        public NewTicket NewTicket { get; }
    }
}