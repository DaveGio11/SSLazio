using SSLazio.Models;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class MemoriesController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Memories
        public ActionResult Index()
        {
            return View(db.Memories.ToList());
        }

        // GET: Memories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memories memories = db.Memories.Find(id);
            if (memories == null)
            {
                return HttpNotFound();
            }
            return View(memories);
        }

        // GET: Memories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Memories/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdStoria,ImgStoria,TitoloStoria,DataStoria,Descrizione")] Memories memories, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Storia/"), nomeFile);
                img.SaveAs(pathToSave);
                memories.ImgStoria = "/Content/Storia/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori

            }
            if (ModelState.IsValid)
            {
                db.Memories.Add(memories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memories);
        }

        // GET: Memories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memories memories = db.Memories.Find(id);
            if (memories == null)
            {
                return HttpNotFound();
            }
            return View(memories);
        }

        // POST: Memories/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdStoria,ImgStoria,TitoloStoria,DataStoria,Descrizione")] Memories memories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memories);
        }

        // GET: Memories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memories memories = db.Memories.Find(id);
            if (memories == null)
            {
                return HttpNotFound();
            }
            return View(memories);
        }

        // POST: Memories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Memories memories = db.Memories.Find(id);
            db.Memories.Remove(memories);
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
