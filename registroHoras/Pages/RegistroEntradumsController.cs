using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using registroHoras.Models;

namespace registroHoras.Pages
{
    public class RegistroEntradumsController : Controller
    {
        private readonly pruebaContext _context;

        public RegistroEntradumsController(pruebaContext context)
        {
            _context = context;
        }

        // GET: RegistroEntradums
        public async Task<IActionResult> Index()
        {
              return View(await _context.RegistroEntrada.ToListAsync());
        }

        // GET: RegistroEntradums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.RegistroEntrada == null)
            {
                return NotFound();
            }

            var registroEntradum = await _context.RegistroEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroEntradum == null)
            {
                return NotFound();
            }

            return View(registroEntradum);
        }

        // GET: RegistroEntradums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistroEntradums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,Usuario,Estado")] RegistroEntradum registroEntradum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroEntradum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registroEntradum);
        }

        // GET: RegistroEntradums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.RegistroEntrada == null)
            {
                return NotFound();
            }

            var registroEntradum = await _context.RegistroEntrada.FindAsync(id);
            if (registroEntradum == null)
            {
                return NotFound();
            }
            return View(registroEntradum);
        }

        // POST: RegistroEntradums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Fecha,Usuario,Estado")] RegistroEntradum registroEntradum)
        {
            if (id != registroEntradum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroEntradum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroEntradumExists(registroEntradum.Id))
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
            return View(registroEntradum);
        }

        // GET: RegistroEntradums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.RegistroEntrada == null)
            {
                return NotFound();
            }

            var registroEntradum = await _context.RegistroEntrada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroEntradum == null)
            {
                return NotFound();
            }

            return View(registroEntradum);
        }

        // POST: RegistroEntradums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.RegistroEntrada == null)
            {
                return Problem("Entity set 'pruebaContext.RegistroEntrada'  is null.");
            }
            var registroEntradum = await _context.RegistroEntrada.FindAsync(id);
            if (registroEntradum != null)
            {
                _context.RegistroEntrada.Remove(registroEntradum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroEntradumExists(long id)
        {
          return _context.RegistroEntrada.Any(e => e.Id == id);
        }
    }
}
