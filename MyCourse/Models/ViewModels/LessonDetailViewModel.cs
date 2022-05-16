using System;
using System.Data;
using MyCourse.Models.Entities;

namespace MyCourse.Models.ViewModels.Lessons
{
    public class LessonDetailViewModel
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }

        public static LessonDetailViewModel FromDataRow(DataRow dataRow)
        {
/*            LessonDetailViewModel lessonViewModel = new LessonDetailViewModel {
               Id = Convert.ToInt64(dataRow["Id"]),
                CourseId = Convert.ToInt64(dataRow["CourseId"]),
                Title = Convert.ToString(dataRow["Title"]),
                Duration = TimeSpan.Parse(Convert.ToString(dataRow["Duration"])),
                Description = Convert.ToString(dataRow["Description"])
            };
*/          LessonDetailViewModel lessonViewModel = new LessonDetailViewModel(); 
                lessonViewModel.Id = Convert.ToInt64(dataRow["Id"]);
                lessonViewModel.CourseId = Convert.ToInt64(dataRow["CourseId"]);
                lessonViewModel.Title = Convert.ToString(dataRow["Title"]);
                lessonViewModel.Duration = TimeSpan.Parse(Convert.ToString(dataRow["Duration"]));
                lessonViewModel.Description = Convert.ToString(dataRow["Description"]);
  
            return lessonViewModel;
        }

        public static LessonDetailViewModel FromEntity(Lesson lesson)
        {
            return new LessonDetailViewModel
            {
                Id = lesson.Id,
                CourseId = lesson.CourseId,
                Title = lesson.Title,
                Duration = lesson.Duration,
                Description = lesson.Description
            };
        }
    }
}