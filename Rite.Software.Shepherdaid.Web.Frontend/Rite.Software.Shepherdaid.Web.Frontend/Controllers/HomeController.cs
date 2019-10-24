using System.Web.Mvc;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string version = typeof(Controller).Assembly.GetName().Version.ToString() + "</h2>";
            //return Content(version);
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
    }
}