using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Web.Models;
using Biblioteca.Web.ViewModels;

namespace Biblioteca.Web.Controllers
{
    public class CartiController : Controller
    {
        private BibliotecaDBEntities db = new BibliotecaDBEntities();

        // GET: Carti
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            //var carti = db.Cartis.Include(h => h.ImprumuturiCartis)
            //   .Select(b => new CarteViewModel
            //   {
            //       IdCarte = b.IdCarte,
            //       Autor = b.Autor,
            //       Editura = b.Editura,
            //       Cod = b.Cod,
            //       Titlu = b.Titlu,
            //       Disponibil = !b.ImprumuturiCartis.Any(h => h.DataReturnare == null)
            //   }).ToList();
            //return View(carti);

            return View(db.Cartis.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: Carti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "IdCarte,Titlu,Cod,Autor,Editura")] Carti carti)
        {
            if (ModelState.IsValid)
            {
                db.Cartis.Add(carti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carti);
        }

        [Authorize(Roles = "Admin")]
        // GET: Carti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carti carti = db.Cartis.Find(id);
            if (carti == null)
            {
                return HttpNotFound();
            }
            return View(carti);
        }

        // POST: Carti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "IdCarte,Titlu,Cod,Autor,Editura")] Carti carti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carti);
        }

        // GET: Carti/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carti carti = db.Cartis.Find(id);
            if (carti == null)
            {
                return HttpNotFound();
            }
            return View(carti);
        }

        // POST: Carti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Carti carti = db.Cartis.Find(id);
            db.Cartis.Remove(carti);
            db.SaveChanges();
            return RedirectToAction("Index");
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
