using System.Collections.Generic;
// using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CourseDetailViewModel : CourseViewModel
    {
        public string Description {get; set;}
        public List<LessonViewModel> Lessons {get; set;}
    }
}