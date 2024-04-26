using SSLazio.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class SettoriAbbController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: SettoriAbb
        public ActionResult Index()
        {
            DateTime dataInizio = new DateTime(2024, 2, 11);
            string dataFormattata = dataInizio.ToString("dd/MM/yyyy"); // Imposta la data di inizio del periodo consentito
            DateTime dataFine = dataInizio.AddMonths(1);
            string dataFormattataFine = dataFine.ToString("dd/MM/yyyy"); // Imposta la data di inizio del periodo consentito

            // Imposta la data di fine del periodo consentito (un mese dopo la data di inizio)
            ViewBag.DataInizio = dataFormattata;
            ViewBag.DataFine = dataFormattataFine;
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
