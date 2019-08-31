using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem;
using AdoptifySystem.Models;

namespace AdoptifySystem.Controllers.Aditi
{
    public class Volunteer_HoursController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Volunteer_Hours
        public ActionResult Index()
        {
            var volunteer_Hours = db.Volunteer_Hours.Include(v => v.Volunteer).Include(v => v.Volunteer_Work_Type);
            return View(volunteer_Hours.ToList());
        }

        // GET: Volunteer_Hours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Hours volunteer_Hours = db.Volunteer_Hours.Find(id);
            if (volunteer_Hours == null)
            {
                return HttpNotFound();
            }
            return View(volunteer_Hours);
        }

        // GET: Volunteer_Hours/Create
        public ActionResult Create()
        {
            ViewBag.Vol_ID = new SelectList(db.Volunteers, "Vol_ID", "Vol_Name");
            ViewBag.Vol_WorkType_ID = new SelectList(db.Volunteer_Work_Type, "Vol_WorkType_ID", "Vol_WorkType_Name");
            return View();
        }

        // POST: Volunteer_Hours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vol_Hours_ID,Vol_workDate,Vol_Start_Time,Vol_End_Time,Vol_ID,Vol_WorkType_ID")] Volunteer_Hours volunteer_Hours)
        {
            if (ModelState.IsValid)
            {
                db.Volunteer_Hours.Add(volunteer_Hours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vol_ID = new SelectList(db.Volunteers, "Vol_ID", "Vol_Name", volunteer_Hours.Vol_ID);
            ViewBag.Vol_WorkType_ID = new SelectList(db.Volunteer_Work_Type, "Vol_WorkType_ID", "Vol_WorkType_Name", volunteer_Hours.Vol_WorkType_ID);
            return View(volunteer_Hours);
        }

        // GET: Volunteer_Hours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Hours volunteer_Hours = db.Volunteer_Hours.Find(id);
            if (volunteer_Hours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vol_ID = new SelectList(db.Volunteers, "Vol_ID", "Vol_Name", volunteer_Hours.Vol_ID);
            ViewBag.Vol_WorkType_ID = new SelectList(db.Volunteer_Work_Type, "Vol_WorkType_ID", "Vol_WorkType_Name", volunteer_Hours.Vol_WorkType_ID);
            return View(volunteer_Hours);
        }

        // POST: Volunteer_Hours/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vol_Hours_ID,Vol_workDate,Vol_Start_Time,Vol_End_Time,Vol_ID,Vol_WorkType_ID")] Volunteer_Hours volunteer_Hours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer_Hours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vol_ID = new SelectList(db.Volunteers, "Vol_ID", "Vol_Name", volunteer_Hours.Vol_ID);
            ViewBag.Vol_WorkType_ID = new SelectList(db.Volunteer_Work_Type, "Vol_WorkType_ID", "Vol_WorkType_Name", volunteer_Hours.Vol_WorkType_ID);
            return View(volunteer_Hours);
        }

        // GET: Volunteer_Hours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Hours volunteer_Hours = db.Volunteer_Hours.Find(id);
            if (volunteer_Hours == null)
            {
                return HttpNotFound();
            }
            return View(volunteer_Hours);
        }

        // POST: Volunteer_Hours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer_Hours volunteer_Hours = db.Volunteer_Hours.Find(id);
            db.Volunteer_Hours.Remove(volunteer_Hours);
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
