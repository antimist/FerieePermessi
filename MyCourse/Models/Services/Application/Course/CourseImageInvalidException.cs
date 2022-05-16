using System;
using System.Runtime.Serialization;

namespace MyCourse.Models.Services.Application.Course
{
    [Serializable]
    internal class CourseImageInvalidException : Exception
    {
        private long id;
        private Exception exc;

        public CourseImageInvalidException()
        {
        }

        public CourseImageInvalidException(string message) : base(message)
        {
        }

        public CourseImageInvalidException(long id, Exception exc)
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