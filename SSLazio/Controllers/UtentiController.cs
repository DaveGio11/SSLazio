using SSLazio.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;



namespace SSLazio.Controllers
{
    public class UtentiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Utenti
        // GET: Utenti
        public ActionResult Index()
        {
            // Controlla se l'utente è autenticato
            if (User.Identity.IsAuthenticated)
            {
                // Se l'utente è un amministratore, mostra tutti gli utenti
                if (User.IsInRole("Admin"))
                {
                    return View(db.Utenti.ToList());
                }
                else
                {
                    // Se l'utente non è un amministratore, mostra solo il proprio profilo
                    string userEmail = User.Identity.Name; // Ottieni l'email dell'utente autenticato
                    var user = db.Utenti.FirstOrDefault(u => u.Email == userEmail);
                    if (user != null)
                    {
                        return View(new List<Utenti> { user });
                    }
                }
            }
            // Se l'utente non è autenticato, reindirizzalo alla pagina di login
            return RedirectToAction("Login", "Utenti");
        }


        // GET: Utenti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // GET: Utenti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utenti/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUtente,Email,Password,Ruolo,Nome,Cognome,DataNascita,LuogoNascita,CodiceFiscale,Residenza")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Utenti.Add(utenti);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(utenti);
        }

        //TIFOSO GET

        public ActionResult Tifoso(string email, string password)
        {
            Utenti utente = new Utenti { Email = email, Password = password };
            return View(utente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tifoso(UpdateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Trovo l'utente esistente
                var utente = db.Utenti.FirstOrDefault(u => u.Email == model.Email);

                if (utente != null)
                {
                    // Aggiorno i campi dell'utente
                    utente.Nome = model.Nome;
                    utente.Cognome = model.Cognome;
                    utente.CodiceFiscale = model.CodiceFiscale;
                    utente.DataNascita = model.DataNascita;
                    utente.LuogoNascita = model.LuogoNascita;
                    utente.Residenza = model.Residenza;


                    // Salvo i cambiamenti nel database
                    db.SaveChanges();

                    // Reindirizzo all'azione "Edit" del controller "Utenti" con l'ID dell'utente
                    return RedirectToAction("Edit", "Utenti", new { id = utente.IdUtente });
                }
                else
                {
                    ModelState.AddModelError("", "Utente non trovato");
                }
            }

            return View(model);
        }


        // Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                using (var context = new ModelDbContext())
                {
                    var user = context.Utenti.FirstOrDefault(e => e.Email == email && e.Password == password);

                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(email, false);
                        TempData["LoginMessage"] = "Benvenuto " + user.Email;

                        if (user.Ruolo == "Admin")
                        {
                            TempData["AdminMessage"] = "Hai effettuato l'accesso come amministratore.";
                        }

                        // Passa l'oggetto Utenti alla vista Tifoso con i dati dell'utente loggato
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email o Password errati");
                        return View();
                    }
                }
            }
            else
            {
                // Il modello non è valido, ritorna la vista con i dati dell'utente
                return View("Authorize", new Utenti { Email = email });
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            TempData["LogoutMessage"] = "Sei stato disconnesso correttamente.";
            return RedirectToAction("Index", "Home");
        }


        // GET: Utenti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUtente,Email,Password,Ruolo,Nome,Cognome,DataNascita,LuogoNascita,CodiceFiscale,Residenza")] Utenti utenti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utenti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Partite");
            }
            return View(utenti);
        }

        // GET: Utenti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utenti utenti = db.Utenti.Find(id);
            if (utenti == null)
            {
                return HttpNotFound();
            }
            return View(utenti);
        }

        // POST: Utenti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utenti utenti = db.Utenti.Find(id);
            db.Utenti.Remove(utenti);
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