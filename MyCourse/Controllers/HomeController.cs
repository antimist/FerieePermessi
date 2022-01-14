using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class HomeController :Controller
    {
        // [ResponseCache(Duration = 60) Location = ResponseCache.Client] //espresso in secondi
        [ResponseCache(CacheProfile ="Home")]
        public IActionResult Index()
        {
            //return Content("Sono la Index della Home");
             return View();
        }
        
    }
}