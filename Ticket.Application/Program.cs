using Microsoft.EntityFrameworkCore;
using Ticket.Application.GraphQL;
using Ticket.Application.GraphQL.Types;
using Ticket.Application.Mapping;
using Ticket.Data.Context;
using Ticket.Services.Interfaces;
using Ticket.Services.Services;

namespace Ticket.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TicketDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddScoped<ITicketService, TicketService>();

            builder.Services.AddAutoMapper(typeof(TicketProfile));

            builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddType<TicketType>()
            .AddType<ResultType>()
            .AddFiltering()
            .AddSorting();

            var app = builder.Build();

            app.MapGraphQL("/graphql");

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TicketDbContext>();
                db.Database.Migrate();
            }

            app.Run();
        }
    }
}
