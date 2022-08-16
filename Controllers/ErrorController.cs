using Microsoft.AspNetCore.Mvc;

namespace RecyclingProject.Client.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NoAvailableCollectingPoints(int recyclerId)
        {
            ViewBag.Id = recyclerId;
            return View();
        }
        public IActionResult NoValueToCash(int recyclerId)
        {
            ViewBag.Id = recyclerId;
            return View();
        }

        public IActionResult FailedCreatingUser() => View();
    }
}
