﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Delineation.Models;

namespace Delineation.Controllers
{
    public class D_ActController : Controller
    {
        private readonly DelineationContext _context;

        public D_ActController(DelineationContext context)
        {
            _context = context;
        }

        // GET: D_Act
        public async Task<IActionResult> Index()
        {
            var delineationContext = _context.d_Acts.Include(d => d.Tc);
            return View(await delineationContext.ToListAsync());
        }

        // GET: D_Act/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }

            return View(d_Act);
        }

        // GET: D_Act/Create
        public IActionResult Create()
        {
            ViewData["TcId"] = new SelectList(_context.D_Tces, "Id", "Num");
            return View();
        }

        // POST: D_Act/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Num,Date,TcId,IsEntity,EntityDoc,ConsBalance,DevBalabce,ConsExpl,DevExpl,IsTransit,FIOtrans,Validity")] D_Act d_Act)
        {
            if (ModelState.IsValid)
            {
                _context.Add(d_Act);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TcId"] = new SelectList(_context.D_Tces, "Id", "Id", d_Act.TcId);
            return View(d_Act);
        }

        // GET: D_Act/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts.FindAsync(id);
            if (d_Act == null)
            {
                return NotFound();
            }
            ViewData["TcId"] = new SelectList(_context.D_Tces, "Id", "Id", d_Act.TcId);
            return View(d_Act);
        }

        // POST: D_Act/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Num,Date,TcId,IsEntity,EntityDoc,ConsBalance,DevBalabce,ConsExpl,DevExpl,IsTransit,FIOtrans,Validity")] D_Act d_Act)
        {
            if (id != d_Act.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(d_Act);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_ActExists(d_Act.Id))
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
            ViewData["TcId"] = new SelectList(_context.D_Tces, "Id", "Id", d_Act.TcId);
            return View(d_Act);
        }

        // GET: D_Act/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Act = await _context.d_Acts
                .Include(d => d.Tc)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Act == null)
            {
                return NotFound();
            }

            return View(d_Act);
        }

        // POST: D_Act/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d_Act = await _context.d_Acts.FindAsync(id);
            _context.d_Acts.Remove(d_Act);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool D_ActExists(int id)
        {
            return _context.d_Acts.Any(e => e.Id == id);
        }
    }
}
