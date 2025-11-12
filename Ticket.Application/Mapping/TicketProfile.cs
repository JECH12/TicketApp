using AutoMapper;
using Ticket.Contracts.DTOs;

namespace Ticket.Application.Mapping
{
    public class TicketProfile:Profile
    {
        public TicketProfile()
        {
            CreateMap< Data.Entities.Ticket, TicketDto>();

            CreateMap<TicketInputDto, Data.Entities.Ticket>();
        }
    }
}
