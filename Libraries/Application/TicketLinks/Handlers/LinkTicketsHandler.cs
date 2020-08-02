using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.TicketLinks.Pings;
using Helpdesk.Services.TicketLinks.Commands.LinkTickets;
using Helpdesk.Services.TicketLinks.Results;
using MediatR;

namespace Helpdesk.Application.TicketLinks.Handlers
{
    public class LinkTicketsHandler : IRequestHandler<LinkTicketsPing, LinkTicketsResult>
    {
        private readonly ILinkTicketService _linkTicketService;

        public LinkTicketsHandler(ILinkTicketService linkTicketService)
        {
            _linkTicketService = linkTicketService;
        }

        public async Task<LinkTicketsResult> Handle(LinkTicketsPing request, CancellationToken cancellationToken)
        {
            return await _linkTicketService.Link(request.LinkTickets);
        }
    }
}