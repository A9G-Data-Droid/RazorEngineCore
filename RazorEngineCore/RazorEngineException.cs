using System;

namespace RazorEngineCore
{
    public class RazorEngineException : ApplicationException
    {
        public RazorEngineException()
        {
        }

        public RazorEngineException(string? message) : base(message)
        {
        }

        public RazorEngineException(string? message, Exception innerException) : base(message, innerException)
        {
        }
    }
}