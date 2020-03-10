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
    public class D_PersonController : Controller
    {
        private readonly DelineationContext _context;

        public D_PersonController(DelineationContext context)
        {
            _context = context;
        }

        // GET: D_Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.D_Persons.ToListAsync());
        }

        // GET: D_Person/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Person = await _context.D_Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Person == null)
            {
                return NotFound();
            }

            return View(d_Person);
        }

        // GET: D_Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: D_Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic")] D_Person d_Person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(d_Person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(d_Person);
        }

        // GET: D_Person/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Person = await _context.D_Persons.FindAsync(id);
            if (d_Person == null)
            {
                return NotFound();
            }
            return View(d_Person);
        }

        // POST: D_Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic")] D_Person d_Person)
        {
            if (id != d_Person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(d_Person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!D_PersonExists(d_Person.Id))
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
            return View(d_Person);
        }

        // GET: D_Person/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var d_Person = await _context.D_Persons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (d_Person == null)
            {
                return NotFound();
            }

            return View(d_Person);
        }

        // POST: D_Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var d_Person = await _context.D_Persons.FindAsync(id);
            _context.D_Persons.Remove(d_Person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool D_PersonExists(int id)
        {
            return _context.D_Persons.Any(e => e.Id == id);
        }
    }
}
