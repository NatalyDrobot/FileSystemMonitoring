using FileSystemWatcher1.Models.ViewModels;
using FileSystemWatcherDAL.Entities;
using FileSystemWatcherDAL.Repositories;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace FileSystemWatcher.Controllers
{

    public class AccountController : Controller
    {
        private readonly IRepository<User> db = new Repository<User>();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.GetWithFilter(p => p.Login == model.Login && p.Password == model.Password).FirstOrDefault();
                Session["User"] = user;

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    return RedirectToAction("Register", "Account");
                }
            }

            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            User user = null;

            if (ModelState.IsValid)
            {
                user = db.GetWithFilter(p => p.Login == model.Login).FirstOrDefault();

                if (user == null)
                {
                    // создаем нового пользователя
                    db.Add(new User { Email = model.Email, Password = model.Password, Login = model.Login });

                    user = db.GetWithFilter(p => p.Login == model.Login && p.Password == model.Password).FirstOrDefault();
                    Session["User"] = user;

                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("Index", "Home");
                    }
                    else { return RedirectToAction("Register", "Account"); }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                    return RedirectToAction("Register", "Account");
                }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            Session["User"] = null;
            return RedirectToAction("Index", "Home");
        }
       
    }
}
