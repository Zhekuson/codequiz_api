using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Repository.Repository.Exceptions
{
    public class QuizAttemptsNotFound : Exception
    {
        public QuizAttemptsNotFound()
        {
        }

        public QuizAttemptsNotFound(string message) : base(message)
        {
        }

        public QuizAttemptsNotFound(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QuizAttemptsNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
