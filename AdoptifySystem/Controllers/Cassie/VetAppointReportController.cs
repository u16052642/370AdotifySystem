using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem.Models;

namespace AdoptifySystem.Controllers
{
    public class VetAppointReportController : Controller
    {
        // GET: VetAppointReport
        public ActionResult Index()
        {
            Wollies_ShelterEntities db = new Wollies_ShelterEntities();

            List<Veterinarian> VetDetails = db.Veterinarians.ToList();
            List<Animal> AnimalDetails = db.Animals.ToList();
            List<Vet_Appointment_Master> VetAppointLineDetails = db.Vet_Appointment_Master.ToList();

            var combinedTable = from a in AnimalDetails
                                join v in VetAppointLineDetails on a.Animal_ID equals v.Animal_ID into table1
                                from v in table1.DefaultIfEmpty()
                                join s in VetDetails on v.Vet_ID equals s.Vet_ID into table2
                                from s in table2.DefaultIfEmpty()
                                select new VetAppointReport { AnimalList = a, AppointList = v, VetList = s };

            return View(combinedTable);
        }
    }
}