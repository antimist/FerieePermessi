using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Services.Infrastructure
{
    [Serializable]
    internal class ImagePersistenceException : Exception
    {
        private Exception exc;

        public ImagePersistenceException()
        {
        }

        public ImagePersistenceException(Exception exc)
        {
            this.exc = exc;
        }

        public ImagePersistenceException(string message) : base(message)
        {
        }

        public ImagePersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ImagePersistenceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}