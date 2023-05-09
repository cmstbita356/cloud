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

        public IActionResult Create()
        {
            TblEmployee emp = new TblEmployee();
            var employees = context.TblEmployees.Where(c => c.CompId == HttpContext.Session.GetInt32("companyID")).ToList();

            return View(employees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblTruck newtruck)
        {
            if (ModelState.IsValid)
            {
                int trucknum = (from truck in context.TblEmployees
                              select truck).Count();

                newtruck.TruckId = trucknum + 1;
                newtruck.Status = 1;
                newtruck.CompId = HttpContext.Session.GetInt32("companyID");

                context.Add(newtruck);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newtruck);
        }

        public IActionResult Delete(int id)
        {
            TblTruck truck = context.TblTrucks.Where(t => t.TruckId == id).SingleOrDefault();
            if (truck == null)
            {
                return NotFound();
            }
            return View(truck);
        }

        [HttpPost]
        public IActionResult Delete(TblTruck truck)
        {
            var v = context.TblTrucks.FirstOrDefault(e => e.TruckId == truck.TruckId);

            if (v == null)
            {
                return NotFound();
            }

            v.Status = 0;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
