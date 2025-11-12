using Ticket.Contracts.DTOs;
using static Ticket.Contracts.Results.Results;

namespace Ticket.Services.Interfaces
{
    public interface ITicketService
    {
        Result<IQueryable<TicketDto>> GetAll();
        Task<Result<TicketDto?>> GetByIdAsync(int id);
        Task<Result<TicketDto>> CreateAsync(TicketInputDto input);
        Task<Result<TicketDto?>> UpdateAsync(int id, TicketInputDto input);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
