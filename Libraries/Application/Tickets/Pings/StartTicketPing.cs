using System;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class StartTicketPing : IRequest<StartTicketResult>
    {
        public StartTicketPing(int ticketId, Guid userGuid)
        {
            TicketId = ticketId;
            UserGuid = userGuid;
        }

        public int TicketId { get; }
        public Guid UserGuid { get; }
    }
}