using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Services.Exceptions
{
    public class UserUnauthorizedException : Exception
    {
        public UserUnauthorizedException()
        {
        }

        public UserUnauthorizedException(string message) : base(message)
        {
        }

        public UserUnauthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserUnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
