using SSLazio.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class ProdottiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Prodotti
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }

        // GET: Prodotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: Prodotti/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prodotti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProdotto,NomeProdotto,Immagine,Immagine2,Descrizione,Prezzo,Quantita")] Prodotti prodotti, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Negozio/"), nomeFile);
                img.SaveAs(pathToSave);
                prodotti.Immagine = "/Content/Negozio/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocatori
                prodotti.Immagine2 = "/Content/Negozio/" + nomeFile;
            }


            if (ModelState.IsValid)
            {
                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProdotto,NomeProdotto,Immagine,Immagine2, Descrizione,Prezzo, Quantita")] Prodotti prodotti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prodotti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prodotti);
        }

        // GET: Prodotti/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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

        public ActionResult AddToCart(int id, int Quantita)
        {

            //if (!User.Identity.IsAuthenticated)
            //{
            //    TempData["AddCartFaild"] = "You must be logged in to add a product to the cart";
            //    return RedirectToAction("Index", "Home");

            //}

            var prodotto = db.Prodotti.Find(id);

            if (prodotto != null)
            {
                var cart = Session["cart"] as List<Prodotti> ?? new List<Prodotti>();
                prodotto.Quantita = Quantita;
                if (cart.Any(p => p.IdProdotto == id))
                {
                    var prodottoInCart = cart.First(p => p.IdProdotto == id);
                    prodottoInCart.Quantita += Quantita;
                }
                else
                {
                    cart.Add(prodotto);
                    Session["cart"] = cart;
                    TempData["AddCart"] = "Product added to the cart";
                }
            }
            return RedirectToAction("Index");
        }

        private int GetAvailableQuantity(int productId)
        {
            using (var db = new ModelDbContext())
            {
                var product = db.Prodotti.Find(productId);
                return product != null ? product.Quantita ?? 0 : 0;
            }
        }



    }
}


