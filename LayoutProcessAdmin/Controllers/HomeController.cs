using LayoutProcessAdmin.Models.Account;
using System.Web.Mvc;

namespace LayoutProcessAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["User"] == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                var user = (User) Session["User"];
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult NoPermission(string module)
        {
            ViewBag.Module = module;

            return View();
        }
    }
}