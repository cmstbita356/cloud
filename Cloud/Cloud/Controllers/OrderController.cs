using Cloud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cloud.Controllers
{
    public class OrderController : Controller
    {
        private readonly Group08ElectricmtContext context;

        public OrderController(Group08ElectricmtContext context)
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

                List<TblOrder> order = context.TblOrders.Where(o => o.CompId == companyID).Select(c => c).ToList();

                return View(order);
            }
        }

        public IActionResult Create()
        {
            int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));
            List<TblItem> item = context.TblItems.Where(i => i.CompId == companyID).Select(c => c).ToList();
            return RedirectToAction("Edit", "Item", item);
        }
        [HttpPost]
        public IActionResult Create(IEnumerable<TblItem> itemList)
        {
            int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));
            foreach (TblItem item in itemList)
            {
                var order = context.TblItems.Where(o => o.ItemId == item.ItemId && o.CompId == companyID).SingleOrDefault();
                if (order != null)
                {
                    order.ItemInstock += item.ItemInstock;
                    context.TblItems.Update(order);
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
