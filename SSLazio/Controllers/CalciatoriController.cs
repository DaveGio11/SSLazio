using SSLazio.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class CalciatoriController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Calciatori
        public ActionResult Index()
        {
            return View(db.Calciatori.ToList());
        }

        // GET: Calciatori/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatori calciatori = db.Calciatori.Find(id);
            if (calciatori == null)
            {
                return HttpNotFound();
            }
            return View(calciatori);
        }

        // GET: Calciatori/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calciatori/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCalciatore,ImgCalciatore,Nome,Cognome,Ruolo,NumeroMaglia,Nazionalita,DataNascita,Descrizione,Presenze,MinutiGiocati,Gol,Assist,CartelliniGialli,CartelliniRossi,ImgRuolo,ImgCalciatore2")] Calciatori calciatori, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Team/"), nomeFile);
                img.SaveAs(pathToSave);
                calciatori.ImgCalciatore = "/Content/Team/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                calciatori.ImgRuolo = "/Content/Team/" + nomeFile;
                calciatori.ImgCalciatore2 = "/Content/Team/" + nomeFile;
            }

            if (ModelState.IsValid)
            {
                db.Calciatori.Add(calciatori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calciatori);
        }

        // GET: Calciatori/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatori calciatori = db.Calciatori.Find(id);
            if (calciatori == null)
            {
                return HttpNotFound();
            }
            return View(calciatori);
        }

        // POST: Calciatori/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCalciatore,ImgCalciatore,Nome,Cognome,Ruolo,NumeroMaglia,Nazionalita,DataNascita,Descrizione,Presenze,MinutiGiocati,Gol,Assist,CartelliniGialli,CartelliniRossi,ImgRuolo,ImgCalciatore2")] Calciatori calciatori, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Team/"), nomeFile);
                img.SaveAs(pathToSave);
                calciatori.ImgCalciatore = "/Content/Team/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                calciatori.ImgRuolo = "/Content/Team/" + nomeFile;
                calciatori.ImgCalciatore2 = "/Content/Team/" + nomeFile;
            }

            if (ModelState.IsValid)
            {
                db.Calciatori.Add(calciatori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calciatori);
        }

        // GET: Calciatori/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatori calciatori = db.Calciatori.Find(id);
            if (calciatori == null)
            {
                return HttpNotFound();
            }
            return View(calciatori);
        }

        // POST: Calciatori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calciatori calciatori = db.Calciatori.Find(id);
            db.Calciatori.Remove(calciatori);
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
