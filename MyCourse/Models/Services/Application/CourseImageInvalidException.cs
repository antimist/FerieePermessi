using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Services.Application
{
    [Serializable]
    internal class CourseImageInvalidException : Exception
    {
        private int id;
        private Exception exc;

        public CourseImageInvalidException()
        {
        }

        public CourseImageInvalidException(string message) : base(message)
        {
        }

        public CourseImageInvalidException(int id, Exception exc)
        {
            this.id = id;
            this.exc = exc;
        }

        public CourseImageInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CourseImageInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}