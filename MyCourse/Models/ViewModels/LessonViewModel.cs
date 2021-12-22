using System;
using System.Data;

namespace MyCourse.Models.ViewModels
{
    public class LessonViewModel
    {
        public int Id {get; set;}
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
    }

}

