using MvcBreadCrumbs;
using System.Web.Mvc;

namespace Rite.Software.Shepherdaid.Web.Frontend.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        [BreadCrumb(Clear =true, Label ="Security")]
        public ActionResult Index()
        {
            return View();
        }
    }
}