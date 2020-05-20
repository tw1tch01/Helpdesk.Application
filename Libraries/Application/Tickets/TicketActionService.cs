using System.Threading.Tasks;
using Helpdesk.Services.Tickets.Commands.CloseTicket;
using Helpdesk.Services.Tickets.Commands.PauseTicket;
using Helpdesk.Services.Tickets.Commands.ReopenTicket;
using Helpdesk.Services.Tickets.Commands.ResolveTicket;
using Helpdesk.Services.Tickets.Commands.StartTicket;
using Helpdesk.Services.Tickets.Results;

namespace Helpdesk.Application.Tickets
{
    public class TicketActionService
    {
        private readonly ICloseTicketService _closeTicketService;
        private readonly IPauseTicketService _pauseTicketService;
        private readonly IReopenTicketService _reopenTicketService;
        private readonly IResolveTicketService _resolveTicketService;
        private readonly IStartTicketService _startTicketService;

        public TicketActionService(
            ICloseTicketService closeTicketService,
            IPauseTicketService pauseTicketService,
            IReopenTicketService reopenTicketService,
            IResolveTicketService resolveTicketService,
            IStartTicketService startTicketService)
        {
            _closeTicketService = closeTicketService;
            _pauseTicketService = pauseTicketService;
            _reopenTicketService = reopenTicketService;
            _resolveTicketService = resolveTicketService;
            _startTicketService = startTicketService;
        }

        public Task<CloseTicketResult> CloseTicket(int ticketId, int userId) => _closeTicketService.Close(ticketId, userId);

        public Task<PauseTicketResult> PauseTicket(int ticketId, int userId) => _pauseTicketService.Pause(ticketId, userId);

        public Task<ReopenTicketResult> ReopenTicket(int ticketId, int userId) => _reopenTicketService.Reopen(ticketId, userId);

        public Task<ResolveTicketResult> ResolveTicket(int ticketId, int userId) => _resolveTicketService.Resolve(ticketId, userId);

        public Task<StartTicketResult> StartTicket(int ticketId, int userId) => _startTicketService.Start(ticketId, userId);
    }
}