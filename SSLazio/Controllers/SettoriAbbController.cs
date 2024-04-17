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
    public class SettoriAbbController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: SettoriAbb
        public ActionResult Index()
        {
            return View(db.SettoriAbb.ToList());
        }

        // GET: SettoriAbb/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriAbb settoriAbb = db.SettoriAbb.Find(id);
            if (settoriAbb == null)
            {
                return HttpNotFound();
            }
            return View(settoriAbb);
        }

        // GET: SettoriAbb/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SettoriAbb/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSettoreAbb,NomeSettore,Intero,Under16,Aquilotto,PostiDisponibili")] SettoriAbb settoriAbb)
        {
            if (ModelState.IsValid)
            {
                db.SettoriAbb.Add(settoriAbb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(settoriAbb);
        }

        // GET: SettoriAbb/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriAbb settoriAbb = db.SettoriAbb.Find(id);
            if (settoriAbb == null)
            {
                return HttpNotFound();
            }
            return View(settoriAbb);
        }

        // POST: SettoriAbb/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSettoreAbb,NomeSettore,Intero,Under16,Aquilotto,PostiDisponibili")] SettoriAbb settoriAbb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(settoriAbb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(settoriAbb);
        }

        // GET: SettoriAbb/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SettoriAbb settoriAbb = db.SettoriAbb.Find(id);
            if (settoriAbb == null)
            {
                return HttpNotFound();
            }
            return View(settoriAbb);
        }

        // POST: SettoriAbb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SettoriAbb settoriAbb = db.SettoriAbb.Find(id);
            db.SettoriAbb.Remove(settoriAbb);
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
