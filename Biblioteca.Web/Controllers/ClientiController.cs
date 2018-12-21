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
    public class ClientiController : Controller
    {
        private BibliotecaDBEntities db = new BibliotecaDBEntities();

        // GET: Clienti
        ////[Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Clientis.ToList());
        }

        ////[Authorize(Roles = "Admin")]
        // GET: Clienti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clienti/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "IdClient,Nume,Adresa,Contact")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Clientis.Add(clienti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clienti);
        }

        ////[Authorize(Roles = "Admin")]
        // GET: Clienti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clientis.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        // POST: Clienti/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        ////[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdClient,Nume,Adresa,Contact")] Clienti clienti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienti);
        }

        ////[Authorize(Roles = "Admin")]
        // GET: Clienti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clienti clienti = db.Clientis.Find(id);
            if (clienti == null)
            {
                return HttpNotFound();
            }
            return View(clienti);
        }

        ////[Authorize(Roles = "Admin")]
        // POST: Clienti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clienti clienti = db.Clientis.Find(id);
            db.Clientis.Remove(clienti);
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
