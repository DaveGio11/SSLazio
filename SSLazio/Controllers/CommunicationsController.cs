using SSLazio.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class CommunicationsController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Communications
        public ActionResult Index()
        {
            return View(db.Communications.ToList());
        }

        // GET: Communications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communications communications = db.Communications.Find(id);
            if (communications == null)
            {
                return HttpNotFound();
            }
            return View(communications);
        }

        // GET: Communications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Communications/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdComunicazione,TitoloCom,Contenuto,DataComunicazione, ImgComunicazione")] Communications communications, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/News/"), nomeFile);
                img.SaveAs(pathToSave);
                communications.ImgComunicazione = "/Content/News/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori

            }

            if (ModelState.IsValid)
            {
                db.Communications.Add(communications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(communications);
        }

        // GET: Communications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communications communications = db.Communications.Find(id);
            if (communications == null)
            {
                return HttpNotFound();
            }
            return View(communications);
        }

        // POST: Communications/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdComunicazione,TitoloCom,Contenuto,DataComunicazione, ImgComunicazione")] Communications communications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(communications);
        }

        // GET: Communications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Communications communications = db.Communications.Find(id);
            if (communications == null)
            {
                return HttpNotFound();
            }
            return View(communications);
        }

        // POST: Communications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Communications communications = db.Communications.Find(id);
            db.Communications.Remove(communications);
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
