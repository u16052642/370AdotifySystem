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
    public class Animal_TreatmentController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Animal_Treatment
        public ActionResult Index()
        {
            var animal_Treatment = db.Animal_Treatment.Include(a => a.Animal).Include(a => a.Mecidal_Card);
            return View(animal_Treatment.ToList());
        }

        // GET: Animal_Treatment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Treatment animal_Treatment = db.Animal_Treatment.Find(id);
            if (animal_Treatment == null)
            {
                return HttpNotFound();
            }
            return View(animal_Treatment);
        }

        // GET: Animal_Treatment/Create
        public ActionResult Create()
        {
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Image");
            ViewBag.Id = new  SelectList(db.Mecidal_Card, "Id", "Diagnosis_Details");
            return View();
        }

        // POST: Animal_Treatment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Treat_ID,Animal_ID,Treat_StartDate,Treat_EndDate,Treat_Quantity,Treat_Regularity,Treat_Name,Treat_cost,Comment,Id")] Animal_Treatment animal_Treatment)
        {
            if (ModelState.IsValid)
            {
                db.Animal_Treatment.Add(animal_Treatment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Image", animal_Treatment.Animal_ID);
            ViewBag.Id = new SelectList(db.Mecidal_Card, "Id", "Diagnosis_Details", animal_Treatment.Id);
            return View(animal_Treatment);
        }

        // GET: Animal_Treatment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Treatment animal_Treatment = db.Animal_Treatment.Find(id);
            if (animal_Treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Image", animal_Treatment.Animal_ID);
            ViewBag.Id = new SelectList(db.Mecidal_Card, "Id", "Diagnosis_Details", animal_Treatment.Id);
            return View(animal_Treatment);
        }

        // POST: Animal_Treatment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Treat_ID,Animal_ID,Treat_StartDate,Treat_EndDate,Treat_Quantity,Treat_Regularity,Treat_Name,Treat_cost,Comment,Id")] Animal_Treatment animal_Treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animal_Treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Image", animal_Treatment.Animal_ID);
            ViewBag.Id = new SelectList(db.Mecidal_Card, "Id", "Diagnosis_Details", animal_Treatment.Id);
            return View(animal_Treatment);
        }

        // GET: Animal_Treatment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Treatment animal_Treatment = db.Animal_Treatment.Find(id);
            if (animal_Treatment == null)
            {
                return HttpNotFound();
            }
            return View(animal_Treatment);
        }

        // POST: Animal_Treatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animal_Treatment animal_Treatment = db.Animal_Treatment.Find(id);
            db.Animal_Treatment.Remove(animal_Treatment);
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
