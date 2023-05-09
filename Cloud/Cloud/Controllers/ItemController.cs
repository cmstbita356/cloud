using Cloud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(List<TblItem> list)
        {
            return View(list);
        }
    }
}
