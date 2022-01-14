/*
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;
*/
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCourse.Models.Enums;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CourserController : Controller
    {
        private readonly ICourseService courseService;
        public CourserController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            //var curseService = new CourseService();
            List<CourseViewModel> courses = await courseService.GetCoursesAsync();
            ViewData["Title"] = "Catalogo dei Corsi";
            return View(courses); //Content("Sono Index");
        }

        public async Task<IActionResult> Detail(int id)
        {
            //var curseService = new CourseService();
            CourseDetailViewModel viewModel = await  courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}