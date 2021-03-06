﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using Microsoft.AspNetCore.Authorization;
using Biblioteca.ViewModels;

namespace Biblioteca.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CartiController : Controller
    {
        private readonly BibliotecaDBContext _context;

        public CartiController(BibliotecaDBContext context)
        {
            _context = context;
        }

        // GET: Carti
        public IActionResult Index()
        {
            var carti = _context.Carti.Include(h => h.ImprumuturiCarti)
                .Select(b => new CarteVM
                {
                    IdCarte = b.IdCarte,
                    Autor = b.Autor,
                    Editura = b.Editura,
                    Cod = b.Cod,
                    Titlu = b.Titlu,
                    Disponibil = !b.ImprumuturiCarti.Any(h => h.DataReturnare == null)
                }).ToList();

            return View(carti);
        }

        // GET: Carti/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCarte,Titlu,Cod,Autor,Editura")] Carti carti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carti);
        }

        // GET: Carti/Edit/5
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carti = await _context.Carti.FindAsync(id);
            if (carti == null)
            {
                return NotFound();
            }
            return View(carti);
        }

        // POST: Carti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCarte,Titlu,Cod,Autor,Editura")] Carti carti)
        {
            if (id != carti.IdCarte)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartiExists(carti.IdCarte))
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
            return View(carti);
        }

        // GET: Carti/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carti = await _context.Carti
                .FirstOrDefaultAsync(m => m.IdCarte == id);
            if (carti == null)
            {
                return NotFound();
            }

            return View(carti);
        }

        // POST: Carti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carti = await _context.Carti.FindAsync(id);
            _context.Carti.Remove(carti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartiExists(int id)
        {
            return _context.Carti.Any(e => e.IdCarte == id);
        }
    }
}
