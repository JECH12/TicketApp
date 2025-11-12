using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Contracts.Results
{
    public class Results
    {
        public class Result<T>
        {
            public bool Success { get; set; }
            public string? Message { get; set; }
            public T? Data { get; set; }
            public List<string>? Errors { get; set; }

            public static Result<T> Ok(T data, string? message = null)
                => new()
                {
                    Success = true,
                    Message = message ?? "Operation completed successfully.",
                    Data = data
                };

            public static Result<T> Fail(string message, List<string>? errors = null)
                => new()
                {
                    Success = false,
                    Message = message,
                    Errors = errors
                };
        }
    }
}
