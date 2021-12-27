using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.Services.Infrastructure;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MyCourseDbContext dbContext;

        public EfCoreCourseService(MyCourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel viewModel = await dbContext.Courses
                .AsNoTracking()
                .Where(course => course.Id == id)
                .Select(course => new CourseDetailViewModel{
                    Id          = course.Id,
                    Title       = course.Title,
                    Description = course.Description,
                    Author      = course.Author,
                    ImagePath   = course.ImagePath,
                    Rating      = course.Rating,
                    CurrentPrice= course.CurrentPrice,
                    FullPrice   = course.FullPrice, 
                    Lessons     = course.Lessons.Select(lesson => new LessonViewModel {
                        Id          = lesson.Id,
                        Title       = lesson.Title,
                        Description = lesson.Description,
                        Duration    = lesson.Duration
                    }).ToList()
                })
                .SingleAsync();                ;

            return viewModel;
        }

       





        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel> queryLinq = dbContext.Courses
            .AsNoTracking()
            .Select(course =>
            new CourseViewModel{
                Id          = course.Id,
                Title       = course.Title,
                ImagePath   = course.ImagePath,
                Author      = course.Author,
                Rating      = course.Rating,
                CurrentPrice= course.CurrentPrice,
                FullPrice   = course.FullPrice
            });
            

            List<CourseViewModel> courses = await queryLinq.ToListAsync();
            return courses;
        }
    }
}