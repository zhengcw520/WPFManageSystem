using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MySqlSugar.Shared
{
    public class ApiResponse
    {
        public string? Message { get; set; }

        public bool Status { get; set; }

        public object? Result { get; set; }

        public int Total { get; set; }

        public ApiResponse(string message, bool Status = false)
        {
            this.Message = message;
            this.Status = Status;
        }

        public ApiResponse(bool status, object result, int Total = 0)
        {
            this.Status = status;
            this.Result = result;
            this.Total = Total;
        }

        public ApiResponse() 
        {
        }    
    }

    public class ApiResponse<T>
    {
        public string? Message { get; set; }

        public bool Status { get; set; }
        public int Total { get; set; }

        public T Result { get; set; }
    }
}
