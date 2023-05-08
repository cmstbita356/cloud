using AutoMapper;
using Cloud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public EmployeeController(Group08ElectricmtContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Company");
            }
            else
            { 

                int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));

                List<TblEmployee> listEmp = context.TblEmployees.Where(emp => emp.Status == 1 && emp.CompId == companyID).Select(emp => emp).ToList();

                return View(listEmp);
            }
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Details()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}
