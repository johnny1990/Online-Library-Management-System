using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Web.Models;

namespace Biblioteca.Web.Controllers
{
    public class ImprumuturiCartiController : Controller
    {      
        private BibliotecaDBEntities db = new BibliotecaDBEntities();

        // GET: ImprumuturiCarti
        ////[Authorize(Roles = "Admin,Client")]
        public ActionResult Index()
        {
            var imprumuturiCartis = db.ImprumuturiCartis.Include(i => i.Carti).Include(i => i.Clienti);
            return View(imprumuturiCartis.ToList());
        }

        // GET: BorrowHistories/Create
        public ActionResult Create()
        {
            ViewBag.IdCarte = new SelectList(db.Cartis, "IdCarte", "Titlu");
            ViewBag.IdClient = new SelectList(db.Clientis, "IdClient", "Nume");
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Admin,Client")]
        public ActionResult Create([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumut)
        {
            if (ModelState.IsValid)
            {
                db.ImprumuturiCartis.Add(imprumut);
                db.SaveChanges();
                return RedirectToAction("Index", "Carti");
            }

            ViewBag.CustomerId = new SelectList(db.Clientis, "IdClient", "Nume", imprumut.IdClient);
            return View(imprumut);
        }

        // GET: BorrowHistories/Edit/5
        ////[Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImprumuturiCarti imprumut = db.ImprumuturiCartis
                .Include(b => b.Carti)
                .Include(c => c.Clienti)
                .Where(b => b.IdCarte == id && b.DataReturnare == null)
                .FirstOrDefault();
            if (imprumut == null)
            {
                return HttpNotFound();
            }
            return View(imprumut);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumut)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = db.ImprumuturiCartis.Find(imprumut.IdImprumut);
                if (borrowHistoryItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                borrowHistoryItem.DataReturnare = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Carti");
            }
            return View(imprumut);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
