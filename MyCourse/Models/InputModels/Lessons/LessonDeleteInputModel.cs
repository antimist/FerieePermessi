using System.ComponentModel.DataAnnotations;

namespace MyCourse.Models.InputModels.Lessons
{
    public class LessonDeleteInputModel
    {
        [Required]
        public long id { get; set; }
        public long CourseId { get; set; }
    }
}