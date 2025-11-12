using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Ticket.Contracts.Constants;
using Ticket.Contracts.DTOs;
using Ticket.Data.Context;
using Ticket.Services.Services;
using Xunit;

namespace Ticket.Tests
{
    public class TicketServiceTests
    {
        private readonly TicketDbContext _context;
        private readonly IMapper _mapper;

        public TicketServiceTests()
        {
            var options = new DbContextOptionsBuilder<TicketDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TicketDbContext(options);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Data.Entities.Ticket, TicketDto>().ReverseMap();
                cfg.CreateMap<TicketInputDto, Data.Entities.Ticket>().ReverseMap();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        private TicketService CreateService() => new TicketService(_context, _mapper);

        [Fact]
        public void GetAllTickets()
        {
            _context.Tickets.AddRange(
                new Data.Entities.Ticket { User = "Esteban", Status = TicketStatuses.Open, CreationDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow },
                new Data.Entities.Ticket { User = "Maria", Status = TicketStatuses.Closed, CreationDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow }
            );
            _context.SaveChanges();

            var service = CreateService();

            var result = service.GetAll();

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.Count().Should().Be(2);
            result.Message.Should().Be(ResultMessages.TicketRetrieved);
        }

        [Fact]
        public async Task GetByIdAsync_Exists()
        {
            var ticket = new Data.Entities.Ticket { User = "Esteban", Status = TicketStatuses.Open, CreationDate = DateTime.UtcNow, UpdateDate = DateTime.UtcNow };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var service = CreateService();

            var result = await service.GetByIdAsync(ticket.Id);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.User.Should().Be("Esteban");
        }

        [Fact]
        public async Task GetByIdAsync_NotExist()
        {
            var service = CreateService();

            var result = await service.GetByIdAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(ResultMessages.TicketNotFound);
        }

        [Fact]
        public async Task CreateAsync_Successfully()
        {
            var input = new TicketInputDto
            {
                User = "Carlos",
                Status = TicketStatuses.Open
            };

            var service = CreateService();

            var result = await service.CreateAsync(input);

            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.User.Should().Be("Carlos");
            _context.Tickets.Count().Should().Be(1);
        }

        [Fact]
        public async Task UpdateAsync_Successfully()
        {
            var ticket = new Data.Entities.Ticket
            {
                User = "Esteban",
                Status = TicketStatuses.Open,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var input = new TicketInputDto
            {
                User = "Esteban C.",
                Status = TicketStatuses.Closed
            };

            var service = CreateService();


            var result = await service.UpdateAsync(ticket.Id, input);


            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data!.User.Should().Be("Esteban C.");
            result.Data.Status.Should().Be(TicketStatuses.Closed);
        }

        [Fact]
        public async Task UpdateAsync_TicketNotExist()
        {
            var input = new TicketInputDto { User = "Pedro", Status = TicketStatuses.Closed };
            var service = CreateService();

            var result = await service.UpdateAsync(999, input);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(ResultMessages.TicketNotFound);
        }


        [Fact]
        public async Task DeleteAsync_Successfully()
        {
            var ticket = new Data.Entities.Ticket
            {
                User = "Maria",
                Status = TicketStatuses.Open,
                CreationDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            var service = CreateService();


            var result = await service.DeleteAsync(ticket.Id);

            result.Success.Should().BeTrue();
            result.Data.Should().BeTrue();
            _context.Tickets.Count().Should().Be(0);
        }

        [Fact]
        public async Task DeleteAsync_NotExist()
        {
            var service = CreateService();

            var result = await service.DeleteAsync(999);

            result.Success.Should().BeFalse();
            result.Message.Should().Be(ResultMessages.TicketNotFound);
        }
    }
}
