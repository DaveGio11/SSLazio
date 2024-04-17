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
    public class PartiteController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Partite
        public ActionResult Index()
        {
            return View(db.Partite.ToList());
        }

        // GET: Partite/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partite partite = db.Partite.Find(id);
            if (partite == null)
            {
                return HttpNotFound();
            }
            return View(partite);
        }

        // GET: Partite/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partite/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPartita,NomePartita,DataOra,LogoLazio,LogoAvversario,LogoCompetizione,Luogo")] Partite partite)
        {
            if (ModelState.IsValid)
            {
                db.Partite.Add(partite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partite);
        }

        // GET: Partite/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partite partite = db.Partite.Find(id);
            if (partite == null)
            {
                return HttpNotFound();
            }
            return View(partite);
        }

        // POST: Partite/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPartita,NomePartita,DataOra,LogoLazio,LogoAvversario,LogoCompetizione,Luogo")] Partite partite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partite);
        }

        // GET: Partite/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partite partite = db.Partite.Find(id);
            if (partite == null)
            {
                return HttpNotFound();
            }
            return View(partite);
        }

        // POST: Partite/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partite partite = db.Partite.Find(id);
            db.Partite.Remove(partite);
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
