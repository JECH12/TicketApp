namespace Ticket.Contracts.DTOs
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string User { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
