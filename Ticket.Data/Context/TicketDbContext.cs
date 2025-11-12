using Microsoft.EntityFrameworkCore;

namespace Ticket.Data.Context
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options): base(options)
        {
        }

        public DbSet<Entities.Ticket> Tickets => Set<Entities.Ticket>();
    }
}
