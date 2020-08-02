using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.ResolveTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class ResolveTicketHandler : IRequestHandler<ResolveTicketPing, ResolveTicketResult>
    {
        private readonly IResolveTicketService _resolveTicketService;

        public ResolveTicketHandler(IResolveTicketService resolveTicketService)
        {
            _resolveTicketService = resolveTicketService;
        }

        public async Task<ResolveTicketResult> Handle(ResolveTicketPing request, CancellationToken cancellationToken)
        {
            return await _resolveTicketService.Resolve(request.TicketId, request.UserGuid);
        }
    }
}