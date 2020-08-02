using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.OpenTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class OpenTicketHandler : IRequestHandler<OpenTicketPing, OpenTicketResult>
    {
        private readonly IOpenTicketService _openTicketService;

        public OpenTicketHandler(IOpenTicketService openTicketService)
        {
            _openTicketService = openTicketService;
        }

        public async Task<OpenTicketResult> Handle(OpenTicketPing request, CancellationToken cancellationToken)
        {
            return await _openTicketService.Open(request.NewTicket);
        }
    }
}