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
        //private BibliotecaDBEntities db = new BibliotecaDBEntities();

        //// GET: ImprumuturiCarti
        //public ActionResult Index()
        //{
        //    var imprumuturiCartis = db.ImprumuturiCartis.Include(i => i.Carti).Include(i => i.Clienti);
        //    return View(imprumuturiCartis.ToList());
        //}


        //// GET: ImprumuturiCarti/Create
        //public ActionResult Create()
        //{
        //    ViewBag.IdCarte = new SelectList(db.Cartis, "IdCarte", "Titlu");
        //    ViewBag.IdClient = new SelectList(db.Clientis, "IdClient", "Nume");
        //    return View();
        //}

        //// POST: ImprumuturiCarti/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumuturiCarti)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ImprumuturiCartis.Add(imprumuturiCarti);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.IdCarte = new SelectList(db.Cartis, "IdCarte", "Titlu", imprumuturiCarti.IdCarte);
        //    ViewBag.IdClient = new SelectList(db.Clientis, "IdClient", "Nume", imprumuturiCarti.IdClient);
        //    return View(imprumuturiCarti);
        //}

        //// GET: ImprumuturiCarti/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ImprumuturiCarti imprumuturiCarti = db.ImprumuturiCartis.Find(id);
        //    if (imprumuturiCarti == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.IdCarte = new SelectList(db.Cartis, "IdCarte", "Titlu", imprumuturiCarti.IdCarte);
        //    ViewBag.IdClient = new SelectList(db.Clientis, "IdClient", "Nume", imprumuturiCarti.IdClient);
        //    return View(imprumuturiCarti);
        //}

        //// POST: ImprumuturiCarti/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti imprumuturiCarti)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(imprumuturiCarti).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdCarte = new SelectList(db.Cartis, "IdCarte", "Titlu", imprumuturiCarti.IdCarte);
        //    ViewBag.IdClient = new SelectList(db.Clientis, "IdClient", "Nume", imprumuturiCarti.IdClient);
        //    return View(imprumuturiCarti);
        //}

        //// GET: ImprumuturiCarti/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ImprumuturiCarti imprumuturiCarti = db.ImprumuturiCartis.Find(id);
        //    if (imprumuturiCarti == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(imprumuturiCarti);
        //}

        //// POST: ImprumuturiCarti/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ImprumuturiCarti imprumuturiCarti = db.ImprumuturiCartis.Find(id);
        //    db.ImprumuturiCartis.Remove(imprumuturiCarti);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        private BibliotecaDBEntities db = new BibliotecaDBEntities();

        // GET: ImprumuturiCarti
        [Authorize(Roles = "Admin,Client")]
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

        // POST: BorrowHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client")]
        public ActionResult Create([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.ImprumuturiCartis.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index", "Carti");
            }

            ViewBag.CustomerId = new SelectList(db.Clientis, "IdClient", "Nume", borrowHistory.IdClient);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ImprumuturiCarti borrowHistory = db.ImprumuturiCartis
                .Include(b => b.Carti)
                .Include(c => c.Clienti)
                .Where(b => b.IdCarte == id && b.DataReturnare == null)
                .FirstOrDefault();
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IdImprumut,IdCarte,IdClient,DataImprumut,DataReturnare")] ImprumuturiCarti borrowHistory)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = db.ImprumuturiCartis.Find(borrowHistory.IdImprumut);
                if (borrowHistoryItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                borrowHistoryItem.DataReturnare = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Carti");
            }
            return View(borrowHistory);
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
