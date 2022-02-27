using System;

namespace MyCourse.Models.Exceptions
{
    public class CourseTitleUnaviableException : Exception
    {
        public CourseTitleUnaviableException(string title, Exception innerException) : base($"Course Title '{title} existed'", innerException)
        {

        }
    }
}