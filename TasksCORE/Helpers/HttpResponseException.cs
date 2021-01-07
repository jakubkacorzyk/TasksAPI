using System;

namespace TasksCORE.Helpers
{
    public class HttpResponseException : Exception
    {
        public int Status { get; set; }
        public string Value { get; set; }

        public HttpResponseException(int status, string message) : base(message)
        {
            Status = status;
        }
    }
}