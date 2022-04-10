using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Exceptions.Infrastructure
{
    [Serializable]
    public class ImagePersistenceException : Exception
    {
        private Exception exc;

        public ImagePersistenceException(Exception innerException) : base("Couldn't persist the image", innerException)
        {
           // this.exc = exc;
        }

/*        public ImagePersistenceException(Exception exc)
        {
            this.exc = exc;
        }
*/
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