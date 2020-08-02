using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.ReopenTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class ReopenTicketHandler : IRequestHandler<ReopenTicketPing, ReopenTicketResult>
    {
        private readonly IReopenTicketService _reopenTicketService;

        public ReopenTicketHandler(IReopenTicketService reopenTicketService)
        {
            _reopenTicketService = reopenTicketService;
        }

        public async Task<ReopenTicketResult> Handle(ReopenTicketPing request, CancellationToken cancellationToken)
        {
            return await _reopenTicketService.Reopen(request.TicketId, request.UserGuid);
        }
    }
}