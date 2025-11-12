
namespace Ticket.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        public string User { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Open"; // or "Closed"
    }
}
