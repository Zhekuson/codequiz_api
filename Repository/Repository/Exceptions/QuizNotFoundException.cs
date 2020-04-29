using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Repository.Repository.Exceptions
{
    public class QuizNotFoundException : Exception
    {
        public QuizNotFoundException()
        {
        }

        public QuizNotFoundException(string message) : base(message)
        {
        }

        public QuizNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QuizNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
