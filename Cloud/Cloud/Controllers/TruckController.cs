using Cloud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class TruckController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public TruckController(Group08ElectricmtContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Company");
            }
            else
            {

                int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));

                List<TblTruck> listEmp = context.TblTrucks.Where(tr => tr.Status == 1 && tr.CompId == companyID).Select(tr => tr).ToList();

                return View(listEmp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblTruck newtruck)
        {
            if (ModelState.IsValid)
            {
                int trucknum = (from truck in context.TblEmployees
                              select truck).Count();

                newtruck.EmplId = trucknum + 1;
                newtruck.Status = 1;

                context.Add(newtruck);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newtruck);
        }
    }
}
