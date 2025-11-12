using Ticket.Contracts.DTOs;
using Ticket.Services.Interfaces;

namespace Ticket.Application.GraphQL
{
    public class Mutation
    {
        public async Task<Contracts.Results.Results.Result<TicketDto>> CreateTicketAsync(TicketInputDto input, [Service] ITicketService service)
            => await service.CreateAsync(input);

        public async Task<Contracts.Results.Results.Result<TicketDto?>> UpdateTicketAsync(int id, TicketInputDto input, [Service] ITicketService service)
            => await service.UpdateAsync(id, input);

        public async Task<Contracts.Results.Results.Result<bool>> DeleteTicketAsync(int id, [Service] ITicketService service)
            => await service.DeleteAsync(id);
    }
}
