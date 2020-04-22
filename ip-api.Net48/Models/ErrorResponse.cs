using System;
using System.Collections.Generic;
using System.Text;

namespace ip_api.Net48.Models
{
    public class ErrorResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Query { get; set; }
    }
}
