using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdoptifySystem.Controllers.Cassie
{
    public class ReportController : Controller
    {
        Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Report
        public ActionResult EmployeeTimesheet()
        {
            return View(db.TimeSheets.ToList());
        }
    }
}