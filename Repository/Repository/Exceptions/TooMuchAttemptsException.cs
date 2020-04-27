using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Repository.Repository.Exceptions
{
    public class TooMuchAttemptsException : Exception
    {
        public TooMuchAttemptsException()
        {
        }

        public TooMuchAttemptsException(string message) : base(message)
        {
        }

        public TooMuchAttemptsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TooMuchAttemptsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
