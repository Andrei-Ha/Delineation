using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;

namespace Delineation.Controllers
{
    public class D_TcController : Controller
    {
        private readonly DelineationContext _context;

        public D_TcController(DelineationContext context)
        {
            _context = context;
        }

        // GET: D_Tc
        public async Task<IActionResult> Index()
        {
            var delineationContext = _context.D_Tces.Include(d => d.Res);
            return View(await delineationContext.ToListAsync());
        }

        // GET: D_Tc/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Tc = await _context.D_Tces
                .Include(d => d.Res)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Tc == null)
            {
                return NotFound();
            }

            return View(d_Tc);
        }

        // GET: D_Tc/Create
        public IActionResult Create()
        {
            ViewData["ResId"] = new SelectList(_context.D_Reses, "ID", "ID");
            return View();
        }

        // POST: D_Tc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Num,Date,FIO,ResId,Address,Pow,Category,Point,InvNum")] D_Tc d_Tc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(d_Tc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResId"] = new SelectList(_context.D_Reses, "ID", "ID", d_Tc.ResId);
            return View(d_Tc);
        }

        // GET: D_Tc/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Tc = await _context.D_Tces.FindAsync(id);
            if (d_Tc == null)
            {
                return NotFound();
            }
            ViewData["ResId"] = new SelectList(_context.D_Reses, "ID", "ID", d_Tc.ResId);
            return View(d_Tc);
        }

        // POST: D_Tc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Num,Date,FIO,ResId,Address,Pow,Category,Point,InvNum")] D_Tc d_Tc)
        {
            if (id != d_Tc.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(d_Tc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_TcExists(d_Tc.Id))
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
            ViewData["ResId"] = new SelectList(_context.D_Reses, "ID", "ID", d_Tc.ResId);
            return View(d_Tc);
        }

        // GET: D_Tc/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Tc = await _context.D_Tces
                .Include(d => d.Res)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Tc == null)
            {
                return NotFound();
            }

            return View(d_Tc);
        }

        // POST: D_Tc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d_Tc = await _context.D_Tces.FindAsync(id);
            _context.D_Tces.Remove(d_Tc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool D_TcExists(int id)
        {
            return _context.D_Tces.Any(e => e.Id == id);
        }
    }
}
