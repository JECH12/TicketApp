using HotChocolate.Types;
using Ticket.Contracts.DTOs;

namespace Ticket.Application.GraphQL.Types
{
    public class ResultType : ObjectType<Contracts.Results.Results.Result<TicketDto>>
    {
        protected override void Configure(IObjectTypeDescriptor<Contracts.Results.Results.Result<TicketDto>> descriptor)
        {
            descriptor.Field(f => f.Success);
            descriptor.Field(f => f.Message);
            descriptor.Field(f => f.Errors);
            descriptor.Field(f => f.Data).Type<TicketType>();
        }
    }
}
