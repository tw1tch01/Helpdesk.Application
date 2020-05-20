﻿using System.Threading.Tasks;
using Helpdesk.DomainModels.Tickets;
using Helpdesk.Services.Tickets.Commands.DeleteTicket;
using Helpdesk.Services.Tickets.Commands.OpenTicket;
using Helpdesk.Services.Tickets.Commands.UpdateTicket;
using Helpdesk.Services.Tickets.Results;

namespace Helpdesk.Application.Tickets
{
    public class TicketManagementService
    {
        private readonly IOpenTicketService _openTicketService;
        private readonly IDeleteTicketService _deleteTicketService;
        private readonly IUpdateTicketService _updateTicketService;

        public TicketManagementService(
            IOpenTicketService openTicketService,
            IDeleteTicketService deleteTicketService,
            IUpdateTicketService updateTicketService)
        {
            _openTicketService = openTicketService;
            _deleteTicketService = deleteTicketService;
            _updateTicketService = updateTicketService;
        }

        public Task<OpenTicketResult> Open(NewTicket newTicket) => _openTicketService.Open(newTicket);

        public Task<DeleteTicketResult> Delete(int ticketId) => _deleteTicketService.Delete(ticketId, 0);

        public Task<UpdateTicketResult> Update(int ticketId, UpdateTicketDto updateTicket) => _updateTicketService.Update(ticketId, updateTicket);
    }
}