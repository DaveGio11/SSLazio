using SSLazio.Models;
using System.Linq;
using System.Web.Mvc;

namespace SSLazio.Controllers
{
    public class HomeController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(db.Partite.ToList());
        }
    }
}
