using System;

namespace MyCourse.Models.Exceptions.Application
{
    public class LessonNotFoundException : Exception
    {
        public LessonNotFoundException(long lessonId) : base($"Lesson {lessonId} not found")
        {
        }
    }
}