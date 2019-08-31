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
    public class Volunteer_Work_TypeController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Volunteer_Work_Type
        public ActionResult Index()
        {
            return View(db.Volunteer_Work_Type.ToList());
        }

        // GET: Volunteer_Work_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Work_Type volunteer_Work_Type = db.Volunteer_Work_Type.Find(id);
            if (volunteer_Work_Type == null)
            {
                return HttpNotFound();
            }
            return View(volunteer_Work_Type);
        }

        // GET: Volunteer_Work_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Volunteer_Work_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vol_WorkType_ID,Vol_WorkType_Name,Vol_WorkType_Description")] Volunteer_Work_Type volunteer_Work_Type)
        {
            if (ModelState.IsValid)
            {
                db.Volunteer_Work_Type.Add(volunteer_Work_Type);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(volunteer_Work_Type);
        }

        // GET: Volunteer_Work_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Work_Type volunteer_Work_Type = db.Volunteer_Work_Type.Find(id);
            if (volunteer_Work_Type == null)
            {
                return HttpNotFound();
            }
            return View(volunteer_Work_Type);
        }

        // POST: Volunteer_Work_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vol_WorkType_ID,Vol_WorkType_Name,Vol_WorkType_Description")] Volunteer_Work_Type volunteer_Work_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(volunteer_Work_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(volunteer_Work_Type);
        }

        // GET: Volunteer_Work_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Volunteer_Work_Type volunteer_Work_Type = db.Volunteer_Work_Type.Find(id);
            if (volunteer_Work_Type == null)
            {
                return HttpNotFound();
            }
            return View(volunteer_Work_Type);
        }

        // POST: Volunteer_Work_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Volunteer_Work_Type volunteer_Work_Type = db.Volunteer_Work_Type.Find(id);
            db.Volunteer_Work_Type.Remove(volunteer_Work_Type);
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

            

                   
              
       
