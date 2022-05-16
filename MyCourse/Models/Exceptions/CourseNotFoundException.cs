using System;

namespace MyCourse.Models.Exceptions
{
    public class CourseNotFoundException : Exception
    {
        //public CourseNotFoundException(long courseId) : base($"Course {courseId} not found!")
        public CourseNotFoundException(long courseId) : base($"Course {courseId} not found!")
        {
        }
    }
}