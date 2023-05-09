using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Cloud.Models;
using NuGet.DependencyResolver;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Update;

namespace Cloud.Controllers
{
    public class CompanyController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public CompanyController(Group08ElectricmtContext context)
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

        // ----------------- SIGN UP ---------------------

        //GET
        public IActionResult SignUp()
        {
            return View();
        }

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

        // ----------------- LOG IN ---------------------

        public IActionResult Login()
        {
            byte[] value = null;
            if (HttpContext.Session.TryGetValue("email", out value))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string CompEmail, string CompPassword)
        {
            TblCompany result = (from comp in context.TblCompanies
                                 where comp.CompEmail == CompEmail
                                 && comp.Status == 1 select comp).SingleOrDefault();
            if (result == null)
                ModelState.AddModelError("CompEmail", "Email không đúng");

            if (ModelState.IsValid)
            {

                if (result.CompPassword.Equals(HashPassword(CompPassword)))
                {
                    HttpContext.Session.SetString("email", CompEmail);
                    HttpContext.Session.SetInt32("companyID", result.CompId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("CompPassword", HashPassword(CompPassword));
                }
            }
            return View();
        }
    }
}
