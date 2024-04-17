using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SSLazio.Models;

namespace SSLazio.Controllers
{
    public class SettoriStadioController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: SettoriStadio
        public ActionResult Index()
        {
            var settoriStadio = db.SettoriStadio.Include(s => s.Partite);
            return View(settoriStadio.ToList());
        }

        // GET: SettoriStadio/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriStadio settoriStadio = db.SettoriStadio.Find(id);
            if (settoriStadio == null)
            {
                return HttpNotFound();
            }
            return View(settoriStadio);
        }

        // GET: SettoriStadio/Create
        public ActionResult Create()
        {
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita");
            return View();
        }

        // POST: SettoriStadio/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSettore,NomeSettore,Intero,Ridotto,PostiDisponibili,IdPartita")] SettoriStadio settoriStadio)
        {
            if (ModelState.IsValid)
            {
                db.SettoriStadio.Add(settoriStadio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", settoriStadio.IdPartita);
            return View(settoriStadio);
        }

        // GET: SettoriStadio/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriStadio settoriStadio = db.SettoriStadio.Find(id);
            if (settoriStadio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", settoriStadio.IdPartita);
            return View(settoriStadio);
        }

        // POST: SettoriStadio/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSettore,NomeSettore,Intero,Ridotto,PostiDisponibili,IdPartita")] SettoriStadio settoriStadio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settoriStadio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", settoriStadio.IdPartita);
            return View(settoriStadio);
        }

        // GET: SettoriStadio/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriStadio settoriStadio = db.SettoriStadio.Find(id);
            if (settoriStadio == null)
            {
                return HttpNotFound();
            }
            return View(settoriStadio);
        }

        // POST: SettoriStadio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SettoriStadio settoriStadio = db.SettoriStadio.Find(id);
            db.SettoriStadio.Remove(settoriStadio);
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
