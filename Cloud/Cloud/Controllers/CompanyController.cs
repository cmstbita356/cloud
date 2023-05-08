using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Cloud.Models;
using NuGet.DependencyResolver;
using System.Text;
using System.Security.Cryptography;

namespace Cloud.Controllers
{
    public class CompanyController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public CompanyController(Group08ElectricmtContext context)
        {
            this.context = context;
        }

        public IActionResult SignUp()
        {
            byte[] value = null;
            if (HttpContext.Session.TryGetValue("username", out value))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }


        private string HashPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        //GET account info from login page
        [HttpGet]
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
