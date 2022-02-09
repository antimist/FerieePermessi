using System;
using System.Data;
using MyCourse.Models.Entities;

namespace MyCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public long Id {get; set;}
        public string Title {get; set;}
        public string Description {get; set;}
        public TimeSpan Duration { get; set; }

        internal static LessonViewModel FromDataRow(DataRow lessonsRow)
        {

            LessonViewModel lessonViewModel = new LessonViewModel
            {
                Id = Convert.ToInt32(lessonsRow["Id"]) , 
                Title = Convert.ToString(lessonsRow["Title"]), 
                Description = Convert.ToString(lessonsRow["Description"]),
                Duration = TimeSpan.Parse(Convert.ToString(lessonsRow["Duration"]))  
            };            
            
            return lessonViewModel;
        }

        public static LessonViewModel FromEntity(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Duration = lesson.Duration
            };
        }
    }

}

