using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.CloseTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class CloseTicketHandler : IRequestHandler<CloseTicketPing, CloseTicketResult>
    {
        private readonly ICloseTicketService _closeTicketService;

        public CloseTicketHandler(ICloseTicketService closeTicketService)
        {
            _closeTicketService = closeTicketService;
        }

        public async Task<CloseTicketResult> Handle(CloseTicketPing request, CancellationToken cancellationToken)
        {
            return await _closeTicketService.Close(request.TicketId, request.UserGuid);
        }
    }
}