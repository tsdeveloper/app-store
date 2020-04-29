using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}