using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;

namespace Biblioteca.Controllers
{
    public class ImprumuturiCartiController : Controller
    {
        private readonly BibliotecaDBContext _context;

        public ImprumuturiCartiController(BibliotecaDBContext context)
        {
            _context = context;
        }

        // GET: ImprumuturiCarti
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Index()
        {
            var bibliotecaDBContext = _context.ImprumuturiCarti.Include(i => i.Carti).Include(i => i.Clienti);
            return View(await bibliotecaDBContext.ToListAsync());
        }

        // GET: ImprumuturiCarti/Create
        [Authorize(Roles = "Admin,Client")]
        public IActionResult Create()
        {
            ViewData["IdCarte"] = new SelectList(_context.Carti, "IdCarte", "Cod");
            ViewData["IdClient"] = new SelectList(_context.Clienti, "IdClient", "Adresa");
            return View();
        }

        // POST: ImprumuturiCarti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client")]
        public async Task<IActionResult> Create([Bind("IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumuturiCarti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imprumuturiCarti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCarte"] = new SelectList(_context.Carti, "IdCarte", "Cod", imprumuturiCarti.IdCarte);
            ViewData["IdClient"] = new SelectList(_context.Clienti, "IdClient", "Adresa", imprumuturiCarti.IdClient);
            return View(imprumuturiCarti);
        }

        // GET: ImprumuturiCarti/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imprumuturiCarti = await _context.ImprumuturiCarti.FindAsync(id);
            if (imprumuturiCarti == null)
            {
                return NotFound();
            }
            ViewData["IdCarte"] = new SelectList(_context.Carti, "IdCarte", "Cod", imprumuturiCarti.IdCarte);
            ViewData["IdClient"] = new SelectList(_context.Clienti, "IdClient", "Adresa", imprumuturiCarti.IdClient);
            return View(imprumuturiCarti);
        }

        // POST: ImprumuturiCarti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumuturiCarti)
        {
            if (id != imprumuturiCarti.IdImprumut)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imprumuturiCarti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImprumuturiCartiExists(imprumuturiCarti.IdImprumut))
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
            ViewData["IdCarte"] = new SelectList(_context.Carti, "IdCarte", "Cod", imprumuturiCarti.IdCarte);
            ViewData["IdClient"] = new SelectList(_context.Clienti, "IdClient", "Adresa", imprumuturiCarti.IdClient);
            return View(imprumuturiCarti);
        }

        // GET: ImprumuturiCarti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imprumuturiCarti = await _context.ImprumuturiCarti
                .Include(i => i.Carti)
                .Include(i => i.Clienti)
                .FirstOrDefaultAsync(m => m.IdImprumut == id);
            if (imprumuturiCarti == null)
            {
                return NotFound();
            }

            return View(imprumuturiCarti);
        }

        // POST: ImprumuturiCarti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imprumuturiCarti = await _context.ImprumuturiCarti.FindAsync(id);
            _context.ImprumuturiCarti.Remove(imprumuturiCarti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImprumuturiCartiExists(int id)
        {
            return _context.ImprumuturiCarti.Any(e => e.IdImprumut == id);
        }
    }
}
