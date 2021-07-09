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
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var book = _context.Carti.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            var imprumut = new ImprumuturiCarti { IdCarte = book.IdCarte, DataImprumut = DateTime.Now, Carti = book };
            ViewBag.IdClient = new SelectList(_context.Clienti, "IdClient", "Nume");
            return View(imprumut);
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

            ViewBag.IdClient = new SelectList(_context.Clienti, "IdClient", "Nume", imprumuturiCarti.IdClient);
            imprumuturiCarti.Carti = _context.Carti.Find(imprumuturiCarti.IdCarte);
            return View(imprumuturiCarti);
        }

        // GET: ImprumuturiCarti/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            ImprumuturiCarti imprumut = _context.ImprumuturiCarti
                .Include(b => b.Carti)
                .Include(c => c.Clienti)
                .Where(b => b.IdCarte == id && b.DataReturnare == null)
                .FirstOrDefault();
            if (imprumut == null)
            {
                return NotFound();
            }
            return View(imprumut);
        }

        // POST: ImprumuturiCarti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit([Bind("IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumuturiCarti)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = _context.ImprumuturiCarti.Include(i => i.Carti)
                    .FirstOrDefault(i => i.IdImprumut == imprumuturiCarti.IdImprumut);
                if (borrowHistoryItem == null)
                {
                    return BadRequest();
                }

                borrowHistoryItem.DataReturnare = DateTime.Now;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Carti");
            }
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
