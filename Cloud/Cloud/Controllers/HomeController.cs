using Cloud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Cloud.Controllers
{
    public class HomeController : Controller
    {
        readonly string connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DefaultConnection"];

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Company companyParam)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();  
            string query = "select * from tbl_company where comp_email = @email and comp_password = @password";
            List<SqlParameter> listpara = new List<SqlParameter>()
            {
                new SqlParameter("@email", companyParam.comp_email),
                new SqlParameter("@password", companyParam.comp_password),
            };
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddRange(listpara.ToArray());
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                return RedirectToAction("Index", "Truck", companyParam);
            }
            conn.Close();
            return View();
        }

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