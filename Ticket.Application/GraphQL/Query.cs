using Ticket.Contracts.DTOs;
using Ticket.Services.Interfaces;

namespace Ticket.Application.GraphQL
{
    public class Query
    {
        [UsePaging(IncludeTotalCount = true)]
        [UseFiltering]
        [UseSorting]
        public IQueryable<TicketDto> GetTickets([Service] ITicketService service)
         => service.GetAll().Data!;

        public async Task<Contracts.Results.Results.Result<TicketDto?>> GetTicketById(int id, [Service] ITicketService service)
            => await service.GetByIdAsync(id);
    }
}
