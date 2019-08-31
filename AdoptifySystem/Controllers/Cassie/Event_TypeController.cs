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

namespace AdoptifySystem.Controllers
{
    public class Event_TypeController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Event_Type
        public ActionResult Index()
        {
            return View(db.Event_Type.ToList());
        }

        // GET: Event_Type/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Event_Type.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }

        // GET: Event_Type/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event_Type/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_Type_ID,Event_Type_Name")] Event_Type event_Type)
        {
            if (ModelState.IsValid)
            {
                db.Event_Type.Add(event_Type);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(event_Type);
        }

        // GET: Event_Type/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Event_Type.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }

        // POST: Event_Type/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_Type_ID,Event_Type_Name")] Event_Type event_Type)
        {
            if (ModelState.IsValid)
            {
                db.Entry(event_Type).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(event_Type);
        }

        // GET: Event_Type/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Event_Type.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            return View(event_Type);
        }

        // POST: Event_Type/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event_Type event_Type = db.Event_Type.Find(id);
            db.Event_Type.Remove(event_Type);
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
