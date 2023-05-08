using Cloud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Cloud.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Group08ElectricmtContext group08ElectricmtContext;

        public HomeController(ILogger<HomeController> logger, Group08ElectricmtContext context)
        {
            _logger = logger;
            this.group08ElectricmtContext = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Login(TblCompany companyParam)
        //{
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    conn.Open();  
        //    string query = "select * from tbl_company where comp_email = @email and comp_password = @password";
        //    List<SqlParameter> listpara = new List<SqlParameter>()
        //    {
        //        new SqlParameter("@email", companyParam.CompEmail),
        //        new SqlParameter("@password", companyParam.CompPassword),
        //    };
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    cmd.Parameters.AddRange(listpara.ToArray());
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    if(reader.HasRows)
        //    {
        //        return RedirectToAction("Index", "Truck", companyParam);
        //    }
        //    conn.Close();
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}