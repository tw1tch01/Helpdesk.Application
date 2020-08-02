using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Application.Tickets.Pings;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Queries.GetTicket;
using MediatR;

namespace Helpdesk.Application.Tickets.Handlers
{
    public class GetTicketHandler : IRequestHandler<GetTicketPing, TicketDetails>
    {
        private readonly IGetTicketService _getTicketService;

        public GetTicketHandler(IGetTicketService getTicketService)
        {
            _getTicketService = getTicketService;
        }

        public async Task<TicketDetails> Handle(GetTicketPing request, CancellationToken cancellationToken)
        {
            return await _getTicketService.Get(request.TicketId);
        }
    }
}