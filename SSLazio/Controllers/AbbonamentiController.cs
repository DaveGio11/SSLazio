using SSLazio.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProvaBiglietti.Controllers
{
    public class AbbonamentiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Abbonamenti
        public ActionResult Index()
        {
            // Verifica se l'utente corrente è un amministratore
            if (User.IsInRole("Admin"))
            {
                // Se è un amministratore, ottieni tutti i biglietti
                var abbonamenti = db.Abbonamenti

                                .Include(b => b.SettoriAbb)
                                .Include(b => b.Utenti);

                return View(abbonamenti.ToList());
            }
            else
            {
                // Altrimenti, l'utente non è un amministratore, quindi ottieni solo i biglietti associati al suo ID
                string userId = User.Identity.Name;
                var abbonamenti = db.Abbonamenti
                                .Where(b => b.Utenti.Email == userId)
                                .Include(b => b.SettoriAbb)
                                .Include(b => b.Utenti);

                return View(abbonamenti.ToList());
            }
        }

        // GET: Abbonamenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abbonamenti abbonamenti = db.Abbonamenti.Find(id);
            if (abbonamenti == null)
            {
                return HttpNotFound();
            }
            return View(abbonamenti);
        }

        // GET: Abbonamenti/Create
        public ActionResult Create(int? idSettoreAbb)
        {

            DateTime dataInizio = new DateTime(2024, 03, 28); // Imposta la data di inizio del periodo consentito
            DateTime dataFine = dataInizio.AddMonths(1); // Imposta la data di fine del periodo consentito (un mese dopo la data di inizio)


            if (DateTime.Today < dataInizio || DateTime.Today > dataFine)
            {
                // Se la data attuale è al di fuori del periodo consentito, reindirizza l'utente o mostra un messaggio di errore
                // Ad esempio:
                TempData["ErrorMessage"] = "L'acquisto degli abbonamenti è disponibile solo dal " + dataInizio.ToShortDateString() + " al " + dataFine.ToShortDateString() + ".";
                return RedirectToAction("Index"); // Reindirizza l'utente alla pagina Index o ad un'altra pagina appropriata
            }

            if (idSettoreAbb == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Ottieni il nome del settore
            var settoreAbb = db.SettoriAbb
                            .Where(s => s.IdSettoreAbb == idSettoreAbb)
                            .Select(s => s.NomeSettore)
                            .FirstOrDefault();

            if (settoreAbb == null)
            {
                return HttpNotFound();
            }

            ViewBag.NomeSettore = settoreAbb;

            // Assicurati che l'utente sia loggato e ottieni i suoi dati
            string userId = User.Identity.Name;
            Utenti utente = db.Utenti.FirstOrDefault(u => u.Email == userId);

            if (utente == null)
            {
                // L'utente non è stato trovato, gestisci l'errore di conseguenza
                ModelState.AddModelError("", "L'utente non è stato trovato.");
                return View();
            }

            // Precompila i campi del modello Abbonamenti con i dati dell'utente loggato
            Abbonamenti abbonamenti = new Abbonamenti
            {
                Nome = utente.Nome,
                Cognome = utente.Cognome,
                LuogoNascita = utente.LuogoNascita,
                CodiceFiscale = utente.CodiceFiscale,
                Residenza = utente.Residenza,
                DataPagamento = DateTime.Today
            };

            return View(abbonamenti);
        }


        // POST: Abbonamenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdAbbonamento,IdUtente,IdSettoreAbb, Nome, Cognome,ImgAbbonato,DataNascita, Residenza, CodiceFiscale, LuogoNascita, TipoDocumento,NumeroDocumento,Riduzione,NumeroCarta,Intestatario,Scadenza,Cvc,DataPagamento, EmailDestinatario")] Abbonamenti abbonamenti)
        {
            if (ModelState.IsValid)
            {
                // Calcolo del prezzo corretto in base alla riduzione selezionata
                var settoreAbb = db.SettoriAbb.Find(abbonamenti.IdSettoreAbb);
                if (abbonamenti.Riduzione == "Intero")
                    abbonamenti.Prezzo = settoreAbb.Intero;
                else if (abbonamenti.Riduzione == "Under16")
                    abbonamenti.Prezzo = settoreAbb.Under16;
                else if (abbonamenti.Riduzione == "Aquilotto")
                    abbonamenti.Prezzo = settoreAbb.Aquilotto;

                // Assicurati che l'utente sia loggato e ottieni il suo ID
                string userId = User.Identity.Name;
                Utenti utente = db.Utenti.FirstOrDefault(u => u.Email == userId);

                if (utente == null)
                {
                    // L'utente non è stato trovato, gestisci l'errore di conseguenza
                    ModelState.AddModelError("", "L'utente non è stato trovato.");
                    return View(abbonamenti);
                }

                // Assegna l'ID dell'utente al biglietto
                abbonamenti.IdUtente = utente.IdUtente;

                db.Abbonamenti.Add(abbonamenti);
                db.SaveChanges();

                // Aggiornamento dei posti disponibili nel settore dello stadio selezionato
                settoreAbb.PostiDisponibili--;
                db.Entry(settoreAbb).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }


            ViewBag.IdSettoreAbb = new SelectList(db.SettoriAbb, "IdSettoriAbb", "NomeSettore", abbonamenti.IdSettoreAbb);
            ViewBag.IdUtente = User.Identity.Name; // Assegnare l'utente loggato
            return View(abbonamenti);
        }

        // GET: Abbonamenti/Edit/5
        public ActionResult Edit(int? id)
        {

            DateTime dataInizio = new DateTime(2024, 04, 11); // Imposta la data di inizio del periodo consentito
            DateTime dataFine = dataInizio.AddMonths(1); // Imposta la data di fine del periodo consentito (un mese dopo la data di inizio)

            if (DateTime.Today < dataInizio || DateTime.Today > dataFine)
            {
                // Se la data attuale è al di fuori del periodo consentito, reindirizza l'utente o mostra un messaggio di errore
                // Ad esempio:
                TempData["ErrorMessage"] = "La cessione degli abbonamenti è disponibile solo dal " + dataInizio.ToShortDateString() + " al " + dataFine.ToShortDateString() + ".";
                return RedirectToAction("Index"); // Reindirizza l'utente alla pagina Index o ad un'altra pagina appropriata
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abbonamenti abbonamenti = db.Abbonamenti.Find(id);
            if (abbonamenti == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSettoreAbb = new SelectList(db.SettoriAbb, "IdSettoreAbb", "NomeSettore", abbonamenti.IdSettoreAbb);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Email", abbonamenti.IdUtente);
            return View(abbonamenti);
        }

        // POST: Abbonamenti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdAbbonamento,IdUtente,IdSettoreAbb,Nome, Cognome,ImgAbbonato,DataNascita, CodiceFiscale,Residenza, LuogoNascita,TipoDocumento,NumeroDocumento,Riduzione,Prezzo,NumeroCarta,Intestatario,Scadenza,Cvc,DataPagamento, EmailDestinatario")] Abbonamenti abbonamenti, HttpPostedFileBase img)
        {
            if (img != null && img.ContentLength > 0)
            {
                string nomeFile = Path.GetFileName(img.FileName);
                string pathToSave = Path.Combine(Server.MapPath("~/Content/Profili/"), nomeFile);
                img.SaveAs(pathToSave);
                abbonamenti.ImgAbbonato = "/Content/Profili/" + nomeFile; // Assicurati che ImgCalciatore sia il campo corretto per l'URL dell'immagine nel tuo modello Giocator
            }
            if (ModelState.IsValid)
            {
                db.Entry(abbonamenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSettoreAbb = new SelectList(db.SettoriAbb, "IdSettoreAbb", "NomeSettore", abbonamenti.IdSettoreAbb);
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Email", abbonamenti.IdUtente);
            return View(abbonamenti);
        }

        // GET: Abbonamenti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Abbonamenti abbonamenti = db.Abbonamenti.Find(id);
            if (abbonamenti == null)
            {
                return HttpNotFound();
            }
            return View(abbonamenti);
        }

        // POST: Abbonamenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Abbonamenti abbonamenti = db.Abbonamenti.Find(id);
            db.Abbonamenti.Remove(abbonamenti);
            db.SaveChanges();

            // Ripristino dei posti disponibili nel settore dello stadio selezionato
            var settoreAbb = db.SettoriAbb.Find(abbonamenti.IdSettoreAbb);
            settoreAbb.PostiDisponibili++;
            db.Entry(settoreAbb).State = EntityState.Modified;
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
