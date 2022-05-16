using System;
using System.Collections.Generic;
using MyCourse.Models.Enums;
using MyCourse.Controllers;
using MyCourse.Models.ValueTypes;
using MyCourse.Models.ViewModels;
using System.Threading.Tasks;
using MyCourse.Models.InputModels;

namespace MyCourse.Models.Services.Application.Course
{
    public class CourseService : ICourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            //throw new NotImplementedException();
            var courseList = new List<CourseViewModel>();
            var rand = new Random();

            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var Fullprice = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
                var course = new CourseViewModel
                {
                    Id = i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, Fullprice),
                    Author = "Nome Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~/logo.svg"
                };
                courseList.Add(course);
            }
            return courseList;
        }
   
        public CourseDetailViewModel GetCourseAsync(long id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble() * 10 + 10);
            var course = new CourseDetailViewModel
            {
                Id = id,
                Title = $"Corso {id}",
                CurrentPrice = new Money(Currency.EUR, price),
                FullPrice = new Money(Currency.EUR, price),
                Author = "Nome Cognome",
                Rating = rand.Next(10, 50) / 10.0,
                ImagePath = "/logo.svg",
                Description = $"Descrizione {id}",
                Lessons = new List<LessonViewModel>()
            };

            for (var i = 1; i <= 5; i++)
            {
                var lesson = new LessonViewModel
                {
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40, 90))
                };
                course.Lessons.Add(lesson);
            }

            return course;
        }

        public Task<ListViewModel<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            throw new NotImplementedException();
        }

        Task<CourseDetailViewModel> ICourseService.GetCourseAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetMostRecentCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetBestRatingCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CourseEditInputModel> GetCourseForEditingAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<CourseDetailViewModel> CreateCurseAsync(CourseCreateInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsTitleAviableAsync(string title, long id)
        {
            throw new NotImplementedException();
        }
    }
}