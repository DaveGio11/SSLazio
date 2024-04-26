using SSLazio.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class TrophiesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Trophies
        public ActionResult Index()
        {
            return View(db.Trophies.ToList());
        }

        // GET: Trophies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophies trophies = db.Trophies.Find(id);
            if (trophies == null)
            {
                return HttpNotFound();
            }
            return View(trophies);
        }

        // GET: Trophies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trophies/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDTrofeo,ImgTrofeo,ImgSquadra,NomeTrofeo,AnnoTrofeo,Descrizione")] Trophies trophies, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/News/"), nomeFile);
                img.SaveAs(pathToSave);
                trophies.ImgTrofeo = "/Content/News/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                trophies.ImgSquadra = "/Content/News/" + nomeFile;
            }

            if (ModelState.IsValid)
            {
                db.Trophies.Add(trophies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trophies);
        }

        // GET: Trophies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophies trophies = db.Trophies.Find(id);
            if (trophies == null)
            {
                return HttpNotFound();
            }
            return View(trophies);
        }

        // POST: Trophies/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDTrofeo,ImgTrofeo,ImgSquadra,NomeTrofeo,AnnoTrofeo,Descrizione")] Trophies trophies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trophies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trophies);
        }

        // GET: Trophies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trophies trophies = db.Trophies.Find(id);
            if (trophies == null)
            {
                return HttpNotFound();
            }
            return View(trophies);
        }

        // POST: Trophies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trophies trophies = db.Trophies.Find(id);
            db.Trophies.Remove(trophies);
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
