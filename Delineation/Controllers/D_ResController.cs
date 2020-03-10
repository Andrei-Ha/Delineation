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
    public class D_ResController : Controller
    {
        private readonly DelineationContext _context;

        public D_ResController(DelineationContext context)
        {
            _context = context;
        }

        // GET: D_Res
        public async Task<IActionResult> Index()
        {
            return View(await _context.D_Reses.ToListAsync());
        }

        // GET: D_Res/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Res = await _context.D_Reses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (d_Res == null)
            {
                return NotFound();
            }

            return View(d_Res);
        }

        // GET: D_Res/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: D_Res/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Code,Name")] D_Res d_Res)
        {
            if (ModelState.IsValid)
            {
                _context.Add(d_Res);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(d_Res);
        }

        // GET: D_Res/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Res = await _context.D_Reses.FindAsync(id);
            if (d_Res == null)
            {
                return NotFound();
            }
            return View(d_Res);
        }

        // POST: D_Res/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,Name")] D_Res d_Res)
        {
            if (id != d_Res.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(d_Res);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_ResExists(d_Res.ID))
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
            return View(d_Res);
        }

        // GET: D_Res/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Res = await _context.D_Reses
                .FirstOrDefaultAsync(m => m.ID == id);
            if (d_Res == null)
            {
                return NotFound();
            }

            return View(d_Res);
        }

        // POST: D_Res/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d_Res = await _context.D_Reses.FindAsync(id);
            _context.D_Reses.Remove(d_Res);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool D_ResExists(int id)
        {
            return _context.D_Reses.Any(e => e.ID == id);
        }
    }
}
