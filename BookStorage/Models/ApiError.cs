using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStorage.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public DateTime Date => DateTime.UtcNow;
        public ApiError(string message)
        {
            Message = message;
        }
    }
}