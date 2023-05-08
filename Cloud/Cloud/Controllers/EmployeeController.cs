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

        public IActionResult Edit(int id)
        {
            TblEmployee target = (from emp in context.TblEmployees
                                  where emp.EmplId == id &&
                                  emp.Status == 1
                                  select emp).SingleOrDefault();

            var maxCompId = context.TblCompanies.Max(c => c.CompId);
            var minCompId = context.TblCompanies.Min(c => c.CompId);

            ViewBag.MaxCompId = maxCompId;
            ViewBag.MinCompId = minCompId;


            return View(target);
        }
        [HttpPost]
        public IActionResult Edit(TblEmployee employee)
        {
            var existingEmployee = context.TblEmployees.FirstOrDefault(e => e.EmplId == employee.EmplId);

            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.EmplFirstname = employee.EmplFirstname;
            existingEmployee.EmplLastname = employee.EmplLastname;
            existingEmployee.CompId = employee.CompId;
            existingEmployee.EmplEmail = employee.EmplEmail;

            context.TblEmployees.Update(existingEmployee);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            TblEmployee employee = (from emp in context.TblEmployees
                                    where emp.EmplId == id
                                    && emp.Status == 1
                                    select emp).SingleOrDefault();
            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            TblEmployee employee = (from emp in context.TblEmployees
                                    where emp.EmplId == id
                                    && emp.Status == 1
                                    select emp).SingleOrDefault();
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        [HttpPost]
        public IActionResult Delete(TblEmployee employee)
        {
            var v = context.TblEmployees.FirstOrDefault(e => e.EmplId == employee.EmplId);

            if (employee == null)
            {
                return NotFound();
            }

            v.Status = 0;
            context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
