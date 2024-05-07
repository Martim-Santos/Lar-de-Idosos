using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lar_de_Idosos.Data;
using Lar_de_Idosos.Models;

namespace Lar_de_Idosos.Controllers
{
    public class GuardiaosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GuardiaosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Guardiaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Guardiao.ToListAsync());
        }

        // GET: Guardiaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardiao = await _context.Guardiao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardiao == null)
            {
                return NotFound();
            }

            return View(guardiao);
        }

        // GET: Guardiaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guardiaos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email,NumTelemovel")] Guardiao guardiao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardiao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(guardiao);
        }

        // GET: Guardiaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardiao = await _context.Guardiao.FindAsync(id);
            if (guardiao == null)
            {
                return NotFound();
            }
            return View(guardiao);
        }

        // POST: Guardiaos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Nome,Email,NumTelemovel")] Guardiao guardiao)
        {
            if (id != guardiao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardiao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardiaoExists(guardiao.Id))
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
            return View(guardiao);
        }

        // GET: Guardiaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardiao = await _context.Guardiao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardiao == null)
            {
                return NotFound();
            }

            return View(guardiao);
        }

        // POST: Guardiaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guardiao = await _context.Guardiao.FindAsync(id);
            if (guardiao != null)
            {
                _context.Guardiao.Remove(guardiao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GuardiaoExists(int id)
        {
            return _context.Guardiao.Any(e => e.Id == id);
        }
    }
}
