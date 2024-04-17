using SSLazio.Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class CalciatriciController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Calciatrici
        public ActionResult Index()
        {
            return View(db.Calciatrici.ToList());
        }

        // GET: Calciatrici/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatrici calciatrici = db.Calciatrici.Find(id);
            if (calciatrici == null)
            {
                return HttpNotFound();
            }
            return View(calciatrici);
        }

        // GET: Calciatrici/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calciatrici/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDCalciatrice,ImgCalciatrice,Nome,Cognome,Ruolo,NumeroMaglia,Nazionalita,DataNascita,Descrizione,Presenze,MinutiGiocati,Gol,Assist,CartelliniGialli,CartelliniRossi,ImgRuolo,ImgCalciatrice2")] Calciatrici calciatrici, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Girls/"), nomeFile);
                img.SaveAs(pathToSave);
                calciatrici.ImgCalciatrice = "/Content/Girls/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                calciatrici.ImgRuolo = "/Content/Team/" + nomeFile;
                calciatrici.ImgCalciatrice2 = "/Content/Team/" + nomeFile;

            }

            if (ModelState.IsValid)
            {
                db.Calciatrici.Add(calciatrici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(calciatrici);
        }

        // GET: Calciatrici/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatrici calciatrici = db.Calciatrici.Find(id);
            if (calciatrici == null)
            {
                return HttpNotFound();
            }
            return View(calciatrici);
        }

        // POST: Calciatrici/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCalciatrice,ImgCalciatrice,Nome,Cognome,Ruolo,NumeroMaglia,Nazionalita,DataNascita,Descrizione,Presenze,MinutiGiocati,Gol,Assist,CartelliniGialli,CartelliniRossi,ImgRuolo,ImgCalciatrice2")] Calciatrici calciatrici, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Girls/"), nomeFile);
                img.SaveAs(pathToSave);
                calciatrici.ImgCalciatrice = "/Content/Girls/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                calciatrici.ImgRuolo = "/Content/Team/" + nomeFile;
                calciatrici.ImgCalciatrice2 = "/Content/Team/" + nomeFile;

            }

            if (ModelState.IsValid)
            {
                db.Calciatrici.Add(calciatrici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calciatrici);
        }

        // GET: Calciatrici/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calciatrici calciatrici = db.Calciatrici.Find(id);
            if (calciatrici == null)
            {
                return HttpNotFound();
            }
            return View(calciatrici);
        }

        // POST: Calciatrici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Calciatrici calciatrici = db.Calciatrici.Find(id);
            db.Calciatrici.Remove(calciatrici);
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
