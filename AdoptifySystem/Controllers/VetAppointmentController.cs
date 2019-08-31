using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem;

namespace AdoptifySystem.Controllers
{
    public class VetAppointmentController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: VetAppointment
        public ActionResult Index()
        {
            var vet_Appointment_Master = db.Vet_Appointment_Master.Include(v => v.Animal).Include(v => v.Veterinarian).Include(v => v.VetAppReason);
            return View(vet_Appointment_Master.ToList());
        }

        // GET: VetAppointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vet_Appointment_Master vet_Appointment_Master = db.Vet_Appointment_Master.Find(id);
            if (vet_Appointment_Master == null)
            {
                return HttpNotFound();
            }
            return View(vet_Appointment_Master);
        }

        // GET: VetAppointment/Create
        public ActionResult Create(DateTime? date)
        {
            List<Animal> animals = db.Animals.ToList();
            List<Animal> animals2 = new List<Animal>();
            foreach (Animal item in animals)
            {
                if (item.Animal_Status_ID == 2)
                    animals2.Add(item);
            }
            ViewBag.Animal_ID = new SelectList(animals2, "Animal_Name", "Animal_Name");
            ViewBag.Vet_ID = new SelectList(db.Veterinarians, "Vet_ID", "Vet_Name");
            ViewBag.VetAppReasonsID = new SelectList(db.VetAppReasons, "VetAppReasonsID", "Reason");
            return View();
        }

        // POST: VetAppointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vet_Appoint_Line_ID,Vet_ID,AppointmentDate,Description,Animal_ID,VetAppReasonsID")] Vet_Appointment_Master vet_Appointment_Master, DateTime? date)
        {
            if (ModelState.IsValid)
            {



                vet_Appointment_Master.AppointmentDate = date.Value;
                db.Vet_Appointment_Master.Add(vet_Appointment_Master);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Animal> animals = db.Animals.ToList();
            List<Animal> animals2 = new List<Animal>();
            foreach (Animal item in animals)
            {
                if(item.Animal_Status_ID==2)
                animals2.Add(item);
            }
            ViewBag.Animal_ID = new SelectList(animals2, "Animal_Name", "Animal_Name", vet_Appointment_Master.Animal_ID);
            ViewBag.Vet_ID = new SelectList(db.Veterinarians, "Vet_ID", "Vet_Name", vet_Appointment_Master.Vet_ID);
            ViewBag.VetAppReasonsID = new SelectList(db.VetAppReasons, "VetAppReasonsID", "Reason", vet_Appointment_Master.VetAppReasonsID);
            return View(vet_Appointment_Master);
        }

        // GET: VetAppointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vet_Appointment_Master vet_Appointment_Master = db.Vet_Appointment_Master.Find(id);
            if (vet_Appointment_Master == null)
            {
                return HttpNotFound();
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name", vet_Appointment_Master.Animal_ID);
            ViewBag.Vet_ID = new SelectList(db.Veterinarians, "Vet_ID", "Vet_Name", vet_Appointment_Master.Vet_ID);
            ViewBag.VetAppReasonsID = new SelectList(db.VetAppReasons, "VetAppReasonsID", "Reason", vet_Appointment_Master.VetAppReasonsID);
            return View(vet_Appointment_Master);
        }

        // POST: VetAppointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vet_Appoint_Line_ID,Vet_ID,AppointmentDate,Description,Animal_ID,VetAppReasonsID")] Vet_Appointment_Master vet_Appointment_Master)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vet_Appointment_Master).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name", vet_Appointment_Master.Animal_ID);
            ViewBag.Vet_ID = new SelectList(db.Veterinarians, "Vet_ID", "Vet_Name", vet_Appointment_Master.Vet_ID);
            ViewBag.VetAppReasonsID = new SelectList(db.VetAppReasons, "VetAppReasonsID", "Reason", vet_Appointment_Master.VetAppReasonsID);
            return View(vet_Appointment_Master);
        }

        // GET: VetAppointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vet_Appointment_Master vet_Appointment_Master = db.Vet_Appointment_Master.Find(id);
            if (vet_Appointment_Master == null)
            {
                return HttpNotFound();
            }
            return View(vet_Appointment_Master);
        }

        // POST: VetAppointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vet_Appointment_Master vet_Appointment_Master = db.Vet_Appointment_Master.Find(id);
            db.Vet_Appointment_Master.Remove(vet_Appointment_Master);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
