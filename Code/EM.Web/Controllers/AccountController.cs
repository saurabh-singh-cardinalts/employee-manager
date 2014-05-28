using System.Web.Mvc;
using EM.Web.ViewModels;

namespace EM.Web.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

    }
}