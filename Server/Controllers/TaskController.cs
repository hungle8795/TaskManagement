using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class TaskController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
