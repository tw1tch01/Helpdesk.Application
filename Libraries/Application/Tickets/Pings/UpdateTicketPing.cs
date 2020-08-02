using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class UpdateTicketPing : IRequest<UpdateTicketResult>
    {
        public UpdateTicketPing(int ticketId, EditTicket editTicket)
        {
            TicketId = ticketId;
            EditTicket = editTicket;
        }

        public int TicketId { get; }
        public EditTicket EditTicket { get; }
    }
}