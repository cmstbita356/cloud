using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
