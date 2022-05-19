using System.Collections.Generic;
using System.Threading.Tasks;
using MyCourse.Models.InputModels;
using MyCourse.Models.InputModels.Courses;
//using MyCourse.Controllers;
using MyCourse.Models.ViewModels;
namespace MyCourse.Models.Services.Application.Course
{
    public interface ICourseService
    {
        //model.Search, model.Page, model.OrderBy, model.Ascending
        //Task<List<CourseViewModel>> GetCoursesAsync(string serch, int page, string orderby, bool ascending);
        Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model);
        Task<CourseDetailViewModel> GetCourseAsync(long id);
        Task<List<CourseViewModel>> GetMostRecentCoursesAsync();
        Task<List<CourseViewModel>> GetBestRatingCoursesAsync();
        Task<CourseEditInputModel> GetCourseForEditingAsync(long id);
        Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel inputModel);
        Task<CourseDetailViewModel> CreateCurseAsync(CourseCreateInputModel inputModel);
        Task<bool> IsTitleAviableAsync(string title, long id);
    }
}