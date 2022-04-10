using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Services.Application
{
    [Serializable]
    internal class ConstraintViolationException : Exception
    {
        public ConstraintViolationException()
        {
        }

        public ConstraintViolationException(Exception exc)
        {
        }
        public ConstraintViolationException(string message) : base(message)
        {
        }

        public ConstraintViolationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConstraintViolationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}