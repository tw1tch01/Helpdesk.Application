using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.TicketLinks.Pings;
using Helpdesk.Services.TicketLinks.Commands.UnlinkTickets;
using Helpdesk.Services.TicketLinks.Results;
using MediatR;

namespace Helpdesk.Application.TicketLinks.Handlers
{
    public class UnlinkTicketsHandler : IRequestHandler<UnlinkTicketsPing, UnlinkTicketsResult>
    {
        private readonly IUnlinkTicketService _unlinkTicketService;

        public UnlinkTicketsHandler(IUnlinkTicketService unlinkTicketService)
        {
            _unlinkTicketService = unlinkTicketService;
        }

        public async Task<UnlinkTicketsResult> Handle(UnlinkTicketsPing request, CancellationToken cancellationToken)
        {
            return await _unlinkTicketService.Unlink(request.UnlinkTickets);
        }
    }
}