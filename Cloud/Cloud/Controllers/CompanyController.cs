using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Cloud.Models;
using NuGet.DependencyResolver;
<<<<<<< Updated upstream
using System.Text;
using System.Security.Cryptography;
=======
using System.Security.Cryptography;
using System.Text;
>>>>>>> Stashed changes

namespace Cloud.Controllers
{
    public class CompanyController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public CompanyController(Group08ElectricmtContext context)
        {
            this.context = context;
        }

        public static string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        //GET
        public IActionResult SignUp()
        {
            return View();
        }

<<<<<<< Updated upstream

        private string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
=======
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(TblCompany newcomp)
        {
            if (ModelState.IsValid)
            {
                int compnum = (from comp in context.TblCompanies
                               select comp).Count();
                newcomp.CompId = compnum + 1;
                newcomp.Status = 1;
                newcomp.CompPassword = HashPassword(newcomp.CompPassword);

                context.Add(newcomp);
                context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(newcomp);
        }

>>>>>>> Stashed changes
        //GET account info from login page
        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Login(TblCompany company)
        {
            TblCompany result = (from comp in context.TblCompanies
                                 where company.CompEmail == comp.CompEmail
                                 select comp).SingleOrDefault();
            if(result == null)
                ModelState.AddModelError("CompEmail", "Email không đúng");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Verify(TblCompany company)
        {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            TblCompany result = (from comp in context.TblCompanies
                               where company.CompEmail == comp.CompEmail
                                     && company.CompPassword == comp.CompPassword
                               select comp).SingleOrDefault();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            if (result == null)
            {
                return View("Error");
            }
            else
            {
                return Index(company.CompId);
            }
        }

        public IActionResult Index(int CompId)
        {
            return View(CompId);
        }
    }
}
