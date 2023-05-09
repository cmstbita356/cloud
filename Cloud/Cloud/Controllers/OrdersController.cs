using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cloud.Models;

namespace Cloud.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Group08ElectricmtContext _context;

        public OrdersController(Group08ElectricmtContext context)
        {
            _context = context;
        }

        // GET: Orders
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Login", "Company");
            }
            else
            {

                int companyID = Convert.ToInt32(HttpContext.Session.GetInt32("companyID"));

                List<TblOrder> listOrd = _context.TblOrders.Where(emp => emp.Status == 1 && emp.CompId == companyID).Select(emp => emp).ToList();

                return View(listOrd);
            }
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblOrder neworder)
        {
            if (ModelState.IsValid)
            {
                int ordnum = (from ord in _context.TblOrders
                              select ord).Count();

                neworder.ReportId = ordnum + 1;
                neworder.CompId = HttpContext.Session.GetInt32("companyID");
                neworder.OrderDelivered = 1;
                neworder.OrderDate = DateTime.Now;
                neworder.Status = 1;

                _context.Add(neworder);
                _context.SaveChangesAsync();

                int ordnumnew = (from ord in _context.TblOrders
                                 select ord).Count();

                if ((ordnumnew = ordnum) != 1)
                {
                    HttpContext.Session.SetInt32("orderID", ordnum);
                }
                else return RedirectToAction(nameof(Index));
            }

            return View(neworder);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOrders == null)
            {
                return NotFound();
            }

            var tblOrder = await _context.TblOrders
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (tblOrder == null)
            {
                return NotFound();
            }

            return View(tblOrder);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOrders == null)
            {
                return Problem("Entity set 'Group08ElectricmtContext.TblOrders'  is null.");
            }
            var tblOrder = await _context.TblOrders.FindAsync(id);
            if (tblOrder != null)
            {
                _context.TblOrders.Remove(tblOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderExists(int id)
        {
          return (_context.TblOrders?.Any(e => e.ReportId == id)).GetValueOrDefault();
        }
    }
}
