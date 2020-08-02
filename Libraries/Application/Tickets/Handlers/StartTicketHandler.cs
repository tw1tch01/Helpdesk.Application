using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.StartTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class StartTicketHandler : IRequestHandler<StartTicketPing, StartTicketResult>
    {
        private readonly IStartTicketService _startTicketService;

        public StartTicketHandler(IStartTicketService startTicketService)
        {
            _startTicketService = startTicketService;
        }

        public async Task<StartTicketResult> Handle(StartTicketPing request, CancellationToken cancellationToken)
        {
            return await _startTicketService.Start(request.TicketId, request.UserGuid);
        }
    }
}