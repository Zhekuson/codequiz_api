using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Repository.Repository.Exceptions
{
    class QuestionNotFoundException : Exception
    {
        public QuestionNotFoundException()
        {
        }

        public QuestionNotFoundException(string message) : base(message)
        {
        }

        public QuestionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected QuestionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
