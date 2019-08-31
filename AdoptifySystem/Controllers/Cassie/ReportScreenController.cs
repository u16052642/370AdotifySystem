using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdoptifySystem.Controllers
{
    public class ReportScreenController : Controller
    {
        [HttpGet]
        // GET: ReportScreen
        public ActionResult Index()
        {
            return View();
        }
    }
}