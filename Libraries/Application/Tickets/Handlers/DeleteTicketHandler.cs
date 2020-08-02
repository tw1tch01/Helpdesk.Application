using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.Services.Tickets.Commands.DeleteTicket;
using Helpdesk.Services.Tickets.Results;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class DeleteTicketHandler : IRequestHandler<DeleteTicketPing, DeleteTicketResult>
    {
        private readonly IDeleteTicketService _deleteTicketService;

        public DeleteTicketHandler(IDeleteTicketService deleteTicketService)
        {
            _deleteTicketService = deleteTicketService;
        }

        public async Task<DeleteTicketResult> Handle(DeleteTicketPing request, CancellationToken cancellationToken)
        {
            return await _deleteTicketService.Delete(request.TicketId, request.UserGuid);
        }
    }
}