using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Contracts.Constants
{
    public static class ResultMessages
    {
        //Generic
        public const string Success = "Operation completed successfully.";
        public const string Error = "An unexpected error occurred.";

        //Tickets
        public const string TicketRetrieved = "Ticket retrieved successfully.";
        public const string TicketsRetrieved = "Tickets retrieved successfully.";
        public const string TicketCreated = "Ticket created successfully.";
        public const string TicketUpdated = "Ticket updated successfully.";
        public const string TicketDeleted = "Ticket deleted successfully.";
        public const string TicketNotFound = "Ticket not found.";

        // Database / System
        public const string DatabaseCreateError = "Database error while creating ticket.";
        public const string DatabaseUpdateError = "Database error while updating ticket.";
        public const string DatabaseDeleteError = "Database error while deleting ticket.";
        public const string DatabaseRetrieveError = "Error retrieving tickets.";
        public const string DatabaseRetrieveSingleError = "Error retrieving ticket.";
    }
}
