using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rite.Software.Shepherdaid.Web.Frontend.Registration
{
    public class DocumentTypesController : Controller
    {
        // GET: DocumentTypes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}