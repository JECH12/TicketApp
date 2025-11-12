using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ticket.Contracts.Constants;
using Ticket.Contracts.DTOs;
using Ticket.Data.Context;
using Ticket.Services.Interfaces;
using static Ticket.Contracts.Results.Results;

namespace Ticket.Services.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketDbContext _context;
        private readonly IMapper _mapper;

        public TicketService(TicketDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Result<IQueryable<TicketDto>> GetAll()
        {
            try
            {
                var query = _context.Tickets
                    .AsNoTracking()
                    .ProjectTo<TicketDto>(_mapper.ConfigurationProvider);

                return Result<IQueryable<TicketDto>>.Ok(query, ResultMessages.TicketRetrieved);
            }
            catch (Exception ex)
            {
                return Result<IQueryable<TicketDto>>.Fail(ResultMessages.DatabaseRetrieveError, new List<string> { ex.Message });
            }
        }

        public async Task<Result<TicketDto?>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.Tickets.FindAsync(id);

                if (entity == null)
                    return Result<TicketDto?>.Fail(ResultMessages.TicketNotFound);

                var dto = _mapper.Map<TicketDto>(entity);
                return Result<TicketDto?>.Ok(dto, ResultMessages.TicketRetrieved);
            }
            catch (Exception ex)
            {
                return Result<TicketDto?>.Fail(ResultMessages.DatabaseRetrieveSingleError, new List<string> { ex.Message });
            }
        }

        public async Task<Result<TicketDto>> CreateAsync(TicketInputDto input)
        {
            try
            {
                var entity = _mapper.Map<Data.Entities.Ticket>(input);
                entity.CreationDate = DateTime.UtcNow;
                entity.UpdateDate = DateTime.UtcNow;

                _context.Tickets.Add(entity);
                await _context.SaveChangesAsync();

                var dto = _mapper.Map<TicketDto>(entity);
                return Result<TicketDto>.Ok(dto, ResultMessages.TicketCreated);
            }
            catch (DbUpdateException dbEx)
            {
                return Result<TicketDto>.Fail(ResultMessages.DatabaseCreateError, new List<string> { dbEx.Message });
            }
            catch (Exception ex)
            {
                return Result<TicketDto>.Fail(ResultMessages.Error, new List<string> { ex.Message });
            }
        }

        public async Task<Result<TicketDto?>> UpdateAsync(int id, TicketInputDto input)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                    return Result<TicketDto?>.Fail(ResultMessages.TicketNotFound);

                ticket.User = input.User ?? ticket.User;
                ticket.Status = input.Status ?? ticket.Status;
                ticket.UpdateDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var dto = _mapper.Map<TicketDto>(ticket);
                return Result<TicketDto?>.Ok(dto, ResultMessages.TicketUpdated);
            }
            catch (DbUpdateException dbEx)
            {
                return Result<TicketDto?>.Fail(ResultMessages.DatabaseUpdateError, new List<string> { dbEx.Message });
            }
            catch (Exception ex)
            {
                return Result<TicketDto?>.Fail(ResultMessages.Error, new List<string> { ex.Message });
            }
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            try
            {
                var entity = await _context.Tickets.FindAsync(id);
                if (entity == null)
                    return Result<bool>.Fail(ResultMessages.TicketNotFound);

                _context.Tickets.Remove(entity);
                await _context.SaveChangesAsync();

                return Result<bool>.Ok(true, ResultMessages.TicketDeleted);
            }
            catch (DbUpdateException dbEx)
            {
                return Result<bool>.Fail(ResultMessages.DatabaseDeleteError, new List<string> { dbEx.Message });
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ResultMessages.Error, new List<string> { ex.Message });
            }
        }
    }
}
