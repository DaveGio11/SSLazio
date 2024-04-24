using SSLazio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class CarrelloController : Controller
    {
        // GET: Carrello
        public ActionResult Index()
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart == null || !cart.Any())
            {
                return RedirectToAction("Index", "Prodotti");
            }
            return View(cart);
        }



        // Action per la gestione quantita e push su DB DettagliVnedita
        [HttpPost]
        public ActionResult Order()
        {
            ModelDbContext db = new ModelDbContext();

            var userId = db.Utenti.FirstOrDefault(u => u.Email == User.Identity.Name).IdUtente;


            var cart = Session["cart"] as List<Prodotti>;

            if (cart != null && cart.Any())
            {
                Ordini ordini = new Ordini();

                ordini.IdUtente = userId;
                ordini.DataOrdine = DateTime.Now;
                db.Ordini.Add(ordini);
                db.SaveChanges();

                foreach (var product in cart)
                {
                    DettaglioOrdini dettaglioVendita = new DettaglioOrdini();

                    dettaglioVendita.IdProdotto = product.IdProdotto;
                    dettaglioVendita.IdOrdine = ordini.IdOrdine;
                    dettaglioVendita.Quantita = Convert.ToInt32(product.Quantita);
                    dettaglioVendita.Totale = product.Prezzo;

                    db.DettaglioOrdini.Add(dettaglioVendita);
                    db.SaveChanges();

                    // Trova il prodotto nel database e aggiorna la quantità disponibile
                    var dbProduct = db.Prodotti.Find(product.IdProdotto);
                    if (dbProduct != null)
                    {
                        dbProduct.Quantita -= dettaglioVendita.Quantita;
                        db.SaveChanges();
                    }
                }
                cart.Clear();
            }
            TempData["Order"] = "Order placed successfully!";

            return RedirectToAction("Index", "Prodotti");

        }


        // Action per la rimozione di un prodotto dal carrello

        public ActionResult Delete(int? id)
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                var productRemove = cart.FirstOrDefault(p => p.IdProdotto == id);
                if (productRemove != null)
                {
                    if (productRemove.Quantita > 1)
                    {
                        productRemove.Quantita--;
                    }
                    else
                    {
                        cart.Remove(productRemove);
                    }
                }
            }
            return RedirectToAction("Index");
        }



        public ActionResult CartClear()
        {
            var cart = Session["cart"] as List<Prodotti>;
            if (cart != null)
            {
                cart.Clear();
            }
            TempData["CreateMess"] = "The cart has been emptied";
            return RedirectToAction("Index", "Prodotti");
        }
    }
}