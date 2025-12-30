using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; } 
        public CustomValidationException(IDictionary<string, string[]> errors)
            : base("Validation failed.")
        {
            Errors = errors;
        }
    }
}
