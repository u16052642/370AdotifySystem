using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdoptifySystem.Controllers.Zinhle
{
    public class KennelController : Controller
    {
        // GET: Kennel
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddKennel()
        {
            return View();
        }

        public ActionResult SearchKennel()
        {
            return View();
        }

        public ActionResult MaintainKennel()
        {
            return View();
        }

        public ActionResult MoveAnimalToKennel()
        {
            return View();
        }
    }
}