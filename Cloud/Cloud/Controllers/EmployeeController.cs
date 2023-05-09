using AutoMapper;
using Cloud.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Cryptography;
using System.Text;

namespace Cloud.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public EmployeeController(Group08ElectricmtContext context)
        {
            this.context = context;
        }

        public static string HashPassword(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                // Convert the byte array to a hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
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

            TblTruck truck = context.TblTrucks.Where(t => t.EmplId == id).SingleOrDefault();
            List<TruckItemView> ListItem = new List<TruckItemView>();

            int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));
            var result = (from i in context.TblItems
                          join t in context.TblTruckItems
            on i.ItemId equals t.ItemId
                          where i.CompId == companyID && t.TruckId == truck.TruckId
                          select new
                          {
                              Item_Name = i.ItemName,
                              Item_Quantity = t.ItemQuantity
                          });

            foreach (var item in result)
            {
                TruckItemView truckitemview = new TruckItemView(item.Item_Name, item.Item_Quantity ?? 0);
                ListItem.Add(truckitemview);
            }


            EmployeeTruckItem v = new EmployeeTruckItem(employee, truck, ListItem);
            return View(v);
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
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            TblEmployee target = (from emp in context.TblEmployees
                                  where emp.Status == 1
                                  select emp).SingleOrDefault();

            var maxCompId = context.TblCompanies.Max(c => c.CompId);
            var minCompId = context.TblCompanies.Min(c => c.CompId);

            ViewBag.MaxCompId = maxCompId;
            ViewBag.MinCompId = minCompId;


            return View(target);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblEmployee newemp)
        {
            if (ModelState.IsValid)
            {
                int empnum = (from emp in context.TblEmployees
                               select emp).Count();
                newemp.EmplId = empnum + 1;
                newemp.Status = 1;
                newemp.EmplPassword = HashPassword("password");

                context.Add(newemp);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newemp);
        }
    }
}
