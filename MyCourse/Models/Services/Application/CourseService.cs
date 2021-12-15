using System;
using System.Collections.Generic;
using MyCourse.Controllers;
using MyCourse.Models.Enums;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class CourseService
    {
        public List<CourseViewModel> GetCourses()
        {
            //throw new NotImplementedException();
            var courseList = new List<CourseViewModel>();
            var rand = new Random();

            for (int i = 1; i <= 20; i++)
            {
                var price = Convert.ToDecimal(rand.NextDouble()*10+10);
                var Fullprice = Convert.ToDecimal(rand.NextDouble()*10+10);
                var course = new CourseViewModel
                {
                    Id=i,
                    Title = $"Corso {i}",
                    CurrentPrice = new Money(Currency.EUR, price),
                    FullPrice = new Money(Currency.EUR, Fullprice),
                    Author ="Nome Cognome",
                    Rating = rand.NextDouble() * 5.0,
                    ImagePath = "~/logo.svg"
                };
                courseList.Add(course);
            }
            return courseList;
        }

        public CourseDetailViewModel GetCourse(int id)
        {
            var rand = new Random();
            var price = Convert.ToDecimal(rand.NextDouble()*10+10 );
            var course = new CourseDetailViewModel
            {
                Id=id,
                Title=$"Corso {id}",
                CurrentPrice= new Money(Currency.EUR, price),
                FullPrice =new Money(Currency.EUR, price),
                Author="Nome Cognome",
                Rating = rand.Next(10,50) / 10.0,
                ImagePath = "/logo.svg",
                Description= $"Descrizione {id}",
                Lessons = new List<LessonViewModel>()
            };

            for (var i = 1; i <=5 ; i++)
            {
                var lesson = new LessonViewModel {
                    Title = $"Lezione {i}",
                    Duration = TimeSpan.FromSeconds(rand.Next(40,90))
                };
                course.Lessons.Add(lesson);
            }

            return course;
        }
    }
}