using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.PauseTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class PauseTicketHandler : IRequestHandler<PauseTicketPing, PauseTicketResult>
    {
        private readonly IPauseTicketService _pauseTicketService;

        public PauseTicketHandler(IPauseTicketService pauseTicketService)
        {
            _pauseTicketService = pauseTicketService;
        }

        public async Task<PauseTicketResult> Handle(PauseTicketPing request, CancellationToken cancellationToken)
        {
            return await _pauseTicketService.Pause(request.TicketId, request.UserGuid);
        }
    }
}