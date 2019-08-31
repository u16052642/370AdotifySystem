using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem;

namespace AdoptifySystem.Controllers.Cassie
{
    public class AnimalsDetailsReportController : Controller
    {
        Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: AnimalsDetailsReport
        public ActionResult Animal()
        {
            return View(db.Animals.ToList());
        }


    }

       
}
