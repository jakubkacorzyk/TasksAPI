using System;
using System.Collections.Generic;
using System.Text;

namespace TasksCORE.Helpers
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;

        public ServiceResponse(T data)
        {
            this.Data = data;
        }

        public ServiceResponse(string message)
        {
            this.Success = false;
            this.Message = message;
        }
    }
}
