using Data.Common;
using Helpdesk.DomainModels.Tickets;
using MediatR;

namespace Helpdesk.Application.Tickets.Pings
{
    public class PagedTicketsPing : IRequest<PagedCollection<TicketLookup>>
    {
        public PagedTicketsPing(int page, int pageSize, TicketLookupParams parameters)
        {
            Page = page;
            PageSize = pageSize;
            Parameters = parameters;
        }

        public int Page { get; }
        public int PageSize { get; }
        public TicketLookupParams Parameters { get; }
    }
}