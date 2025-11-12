using Ticket.Contracts.DTOs;

namespace Ticket.Application.GraphQL.Types
{
    public class TicketType: ObjectType<TicketDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TicketDto> descriptor)
        {
            descriptor.Field(t => t.Id);

            descriptor.Field(t => t.User);

            descriptor.Field(t => t.Status);

            descriptor.Field(t => t.CreationDate);

            descriptor.Field(t => t.UpdateDate);
        }
    }
}
