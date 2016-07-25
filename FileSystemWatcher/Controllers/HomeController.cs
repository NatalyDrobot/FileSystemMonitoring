using System.Web.Mvc;

namespace FileSystemWatcher.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }


    }
}
