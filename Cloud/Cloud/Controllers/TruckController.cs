using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class TruckController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
