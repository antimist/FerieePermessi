using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CourserController : Controller
    {
        public IActionResult Index()
        {
            return Content("Sono Index");
        }

        public IActionResult Detail(string id)
        {
                return Content($"Sono Detail, ho ricevuto l'id {id}");
        }

        public IActionResult Search(string title)
        {
            return Content($"Hai cercato {title}");
        }
    }
}