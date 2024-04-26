using SSLazio.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProvaBiglietti.Controllers
{
    public class BigliettiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Biglietti
        public ActionResult Index()
        {
            // Verifica se l'utente corrente è un amministratore
            if (User.IsInRole("Admin"))
            {
                // Se è un amministratore, ottieni tutti i biglietti
                var biglietti = db.Biglietti
                                .Include(b => b.Partite)
                                .Include(b => b.SettoriStadio)
                                .Include(b => b.Utenti);

                return View(biglietti.ToList());
            }
            else
            {
                // Altrimenti, l'utente non è un amministratore, quindi ottieni solo i biglietti associati al suo ID
                string userId = User.Identity.Name;
                var biglietti = db.Biglietti
                                .Where(b => b.Utenti.Email == userId)
                                .Include(b => b.Partite)
                                .Include(b => b.SettoriStadio)
                                .Include(b => b.Utenti);

                return View(biglietti.ToList());
            }
        }


        // GET: Biglietti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biglietti biglietti = db.Biglietti.Find(id);
            if (biglietti == null)
            {
                return HttpNotFound();
            }
            return View(biglietti);
        }

        // GET: Biglietti/Create
        public ActionResult Create(int? idPartita, int? idSettore)
        {
            if (idPartita == null || idSettore == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Ottieni il nome della partita
            var partita = db.Partite
                            .Where(p => p.IdPartita == idPartita)
                            .Select(p => p.NomePartita)
                            .FirstOrDefault();

            // Ottieni il nome del settore
            var settore = db.SettoriStadio
                            .Where(s => s.IdPartita == idPartita && s.IdSettore == idSettore)
                            .Select(s => s.NomeSettore)
                            .FirstOrDefault();

            if (partita == null || settore == null)
            {
                return HttpNotFound();
            }

            ViewBag.NomePartita = partita;
            ViewBag.NomeSettore = settore;

            // Assicurati che l'utente sia loggato e ottieni i suoi dati
            string userId = User.Identity.Name;
            Utenti utente = db.Utenti.FirstOrDefault(u => u.Email == userId);

            if (utente == null)
            {
                // L'utente non è stato trovato, gestisci l'errore di conseguenza
                ModelState.AddModelError("", "L'utente non è stato trovato.");
                return View();
            }

            // Precompila i campi del modello Biglietti con i dati dell'utente loggato
            Biglietti biglietti = new Biglietti
            {
                Nome = utente.Nome,
                Cognome = utente.Cognome,
                LuogoNascita = utente.LuogoNascita,
                CodiceFiscale = utente.CodiceFiscale,
                Residenza = utente.Residenza,
                DataPagamento = DateTime.Today
            };

            return View(biglietti);
        }





        // POST: Biglietti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdBiglietto,IdPartita,IdSettore,Nome, Cognome, DataNascita, LuogoNascita, CodiceFiscale, Residenza, Riduzione, NumeroCarta, Intestatario, Scadenza, Cvc, DataPagamento, EmailDestinatario")] Biglietti biglietti)
        {
            if (ModelState.IsValid)
            {
                // Calcolo del prezzo corretto in base alla riduzione selezionata
                var settore = db.SettoriStadio.Find(biglietti.IdSettore);
                if (biglietti.Riduzione == "Intero")
                    biglietti.Prezzo = settore.Intero;
                else if (biglietti.Riduzione == "Ridotto")
                    biglietti.Prezzo = settore.Ridotto;

                // Assicurati che l'utente sia loggato e ottieni il suo ID
                string userId = User.Identity.Name;
                Utenti utente = db.Utenti.FirstOrDefault(u => u.Email == userId);

                if (utente == null)
                {
                    // L'utente non è stato trovato, gestisci l'errore di conseguenza
                    ModelState.AddModelError("", "L'utente non è stato trovato.");
                    return View(biglietti);
                }

                // Assegna l'ID dell'utente al biglietto
                biglietti.IdUtente = utente.IdUtente;

                db.Biglietti.Add(biglietti);
                db.SaveChanges();

                // Aggiornamento dei posti disponibili nel settore dello stadio selezionato
                settore.PostiDisponibili--;
                db.Entry(settore).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", biglietti.IdPartita);
            ViewBag.IdSettore = new SelectList(db.SettoriStadio.Where(s => s.IdPartita == biglietti.IdPartita), "IdSettore", "NomeSettore", biglietti.IdSettore);
            ViewBag.IdUtente = User.Identity.Name; // Assegnare l'utente loggato
            return View(biglietti);
        }


        // GET: Biglietti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biglietti biglietti = db.Biglietti.Find(id);
            if (biglietti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", biglietti.IdPartita);
            ViewBag.IdSettore = new SelectList(db.SettoriStadio, "IdSettore", "NomeSettore", biglietti.IdSettore);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Email", biglietti.IdUtente);
            return View(biglietti);
        }

        // POST: Biglietti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdBiglietto,IdUtente,IdPartita,IdSettore,Nome,Cognome,DataNascita,LuogoNascita,CodiceFiscale,Residenza,Riduzione,Prezzo, NumeroCarta, Intestatario, Scadenza, Cvc, DataPagamento, EmailDestinatario")] Biglietti biglietti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(biglietti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPartita = new SelectList(db.Partite, "IdPartita", "NomePartita", biglietti.IdPartita);
            ViewBag.IdSettore = new SelectList(db.SettoriStadio, "IdSettore", "NomeSettore", biglietti.IdSettore);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Email", biglietti.IdUtente);
            return View(biglietti);
        }

        // GET: Biglietti/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Biglietti biglietti = db.Biglietti.Find(id);
            if (biglietti == null)
            {
                return HttpNotFound();
            }
            return View(biglietti);
        }

        // POST: Biglietti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Biglietti biglietti = db.Biglietti.Find(id);
            db.Biglietti.Remove(biglietti);
            db.SaveChanges();

            // Ripristino dei posti disponibili nel settore dello stadio selezionato
            var settore = db.SettoriStadio.Find(biglietti.IdSettore);
            settore.PostiDisponibili++;
            db.Entry(settore).State = EntityState.Modified;
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
