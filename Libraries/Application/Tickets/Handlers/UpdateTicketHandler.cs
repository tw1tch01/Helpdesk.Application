using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.UpdateTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class UpdateTicketHandler : IRequestHandler<UpdateTicketPing, UpdateTicketResult>
    {
        private readonly IUpdateTicketService _updateTicketService;

        public UpdateTicketHandler(IUpdateTicketService updateTicketService)
        {
            _updateTicketService = updateTicketService;
        }

        public async Task<UpdateTicketResult> Handle(UpdateTicketPing request, CancellationToken cancellationToken)
        {
            return await _updateTicketService.Update(request.TicketId, request.EditTicket);
        }
    }
}