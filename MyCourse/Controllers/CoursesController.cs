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
using MyCourse.Models.Exceptions;
using MyCourse.Models.Exceptions.Application;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application.Course;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CourserController : Controller
    {
        private readonly ICourseService courseService;
        //public CourserController(ICourseService courseService)
        public CourserController(ICachedCourseService courseService)
        {
            this.courseService = courseService;
        }
        //public async Task<IActionResult> Index(string search, int page, string orderby, bool ascending)
        public async Task<IActionResult> Index(CourseListInputModel input)
        {
            // var curseService = new CourseService();
            ViewData["Title"] = "Catalogo dei Corsi";
            ListViewModel<CourseViewModel> courses = await courseService.GetCoursesAsync(input);

            CourseListViewModel viewModel = new CourseListViewModel
            {
                Courses = courses,
                Input = input
            };

            return View(viewModel); //Content("Sono Index");
        }

        public async Task<IActionResult> Detail(long id)
        {
            CourseDetailViewModel viewModel = await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "Nuovo Corso";
            var inputModel = new CourseCreateInputModel();
            return View(inputModel);
        }

        //public IActionResult Edit(long id)
        //{
        //    ViewData["Title"] = "Modifica Corso";
        //    return View();
        //}

        public async Task<IActionResult> Edit(long id)
        {
            ViewData["Title"] = "Modifica corso";
            CourseEditInputModel inputModel = await courseService.GetCourseForEditingAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CourseDetailViewModel course = await courseService.EditCourseAsync(inputModel);
                    TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                    return RedirectToAction(nameof(Detail), new { id = inputModel.Id });
                }
                catch (CourseTitleUnaviableException)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), "Questo titolo già esiste");
                }
                catch (CourseImageInvalidException)
                {
                    ModelState.AddModelError(nameof(CourseEditInputModel.Image), "L'immagine selezionata non è valida");
                }
                catch (OptimisticConcurrencyException)
                {
                    ModelState.AddModelError("", "Spiacenti, il salvataggio non è andato a buon fine perché nel frattempo un altro utente ha aggiornato il corso. Ti preghiamo di aggiornare la pagina e ripetere le modifiche.");
                }
            }

            ViewData["Title"] = "Modifica corso";
            return View(inputModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInputModel inputModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CourseDetailViewModel curse = await courseService.CreateCurseAsync(inputModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (CourseTitleUnaviableException)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), "Questo titolo già esiste");
                }
            }

            ViewData["Title"] = "Nuovo Corso";
            return View(inputModel);
            //Convolgere un servizio applicativo in modo che il corso venga creato
            //CourseDetailViewModel course = await courseService.CreateCurseAsync(inputModel);
            //return RedirectToAction(nameof (Index));
        }

        public async Task<IActionResult> IsTitleAviable(string title, long id = 0)
        {
            bool result = await courseService.IsTitleAviableAsync(title, id);
            return Json(result);
        }
    }
}