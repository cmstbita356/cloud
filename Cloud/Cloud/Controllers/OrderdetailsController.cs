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
    public class OrderdetailsController : Controller
    {
        private readonly Group08ElectricmtContext _context;

        public OrderdetailsController(Group08ElectricmtContext context)
        {
            _context = context;
        }

        // GET: Orderdetails
        public async Task<IActionResult> Index()
        {
              return _context.TblOrderdetails != null ? 
                          View(await _context.TblOrderdetails.ToListAsync()) :
                          Problem("Entity set 'Group08ElectricmtContext.TblOrderdetails'  is null.");
        }

        // GET: Orderdetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblOrderdetails == null)
            {
                return NotFound();
            }

            var tblOrderdetail = await _context.TblOrderdetails
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (tblOrderdetail == null)
            {
                return NotFound();
            }

            return View(tblOrderdetail);
        }

        // GET: Orderdetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orderdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblOrderdetail neworder)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetInt32("orderID") != null)
                {
                    int ordnum = (from ord in _context.TblOrderdetails
                                  select ord).Count();

                    neworder.EntryId = ordnum + 1;
                    neworder.Status = 1;
                    neworder.OrderId = HttpContext.Session.GetInt32("orderID");
                    neworder.CompId = HttpContext.Session.GetInt32("companyID");

                    _context.Add(neworder); // Orderdetail
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("Create", "Orders");
                }
            }
            return View(neworder);
        }

        // GET: Orderdetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblOrderdetails == null)
            {
                return NotFound();
            }

            var tblOrderdetail = await _context.TblOrderdetails.FindAsync(id);
            if (tblOrderdetail == null)
            {
                return NotFound();
            }
            return View(tblOrderdetail);
        }

        // POST: Orderdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EntryId,OrderId,ItemId,ItemQuantity,CompId,Status")] TblOrderdetail tblOrderdetail)
        {
            if (id != tblOrderdetail.EntryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrderdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrderdetailExists(tblOrderdetail.EntryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblOrderdetail);
        }

        // GET: Orderdetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblOrderdetails == null)
            {
                return NotFound();
            }

            var tblOrderdetail = await _context.TblOrderdetails
                .FirstOrDefaultAsync(m => m.EntryId == id);
            if (tblOrderdetail == null)
            {
                return NotFound();
            }

            return View(tblOrderdetail);
        }

        // POST: Orderdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblOrderdetails == null)
            {
                return Problem("Entity set 'Group08ElectricmtContext.TblOrderdetails'  is null.");
            }
            var tblOrderdetail = await _context.TblOrderdetails.FindAsync(id);
            if (tblOrderdetail != null)
            {
                _context.TblOrderdetails.Remove(tblOrderdetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrderdetailExists(int id)
        {
          return (_context.TblOrderdetails?.Any(e => e.EntryId == id)).GetValueOrDefault();
        }
    }
}
