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
    public class Event_Controller : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Event_
        public ActionResult Index()
        {
            var event_ = db.Event_.Include(e => e.Event_Type);
            return View(event_.ToList());
        }

        // GET: Event_/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_ event_ = db.Event_.Find(id);
            if (event_ == null)
            {
                return HttpNotFound();
            }
            return View(event_);
        }

        // GET: Event_/Create
        public ActionResult Create(DateTime? date)
        {
            ViewBag.Event_Type_ID = new SelectList(db.Event_Type, "Event_Type_ID", "Event_Type_Name");
            return View();
        }

        // POST: Event_/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Event_ID,Event_Name,Event_StartTime,Event_EndTime,Event_Guest_Number,Event_Ticket_Price,Event_Location,Event_Description,Event_Type_ID")] Event_ event_, DateTime? date, string time="")
        {
            if (ModelState.IsValid)
            {

                int hour = Convert.ToInt32(time);

                DateTime dt = DateTime.Now;

                
                event_.Event_StartTime = date.Value;
                db.Event_.Add(event_);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event_Type_ID = new SelectList(db.Event_Type, "Event_Type_ID", "Event_Type_Name", event_.Event_Type_ID);
            return View(event_);
        }

        // GET: Event_/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_ event_ = db.Event_.Find(id);
            if (event_ == null)
            {
                return HttpNotFound();
            }
            ViewBag.Event_Type_ID = new SelectList(db.Event_Type, "Event_Type_ID", "Event_Type_Name", event_.Event_Type_ID);
            return View(event_);
        }

        // POST: Event_/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Event_ID,Event_Name,Event_StartTime,Event_EndTime,Event_Guest_Number,Event_Ticket_Price,Event_Location,Event_Description,Event_Type_ID")] Event_ event_)
        {
            if (ModelState.IsValid)
            {
                db.Entry(event_).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Event_Type_ID = new SelectList(db.Event_Type, "Event_Type_ID", "Event_Type_Name", event_.Event_Type_ID);
            return View(event_);
        }

        // GET: Event_/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_ event_ = db.Event_.Find(id);
            if (event_ == null)
            {
                return HttpNotFound();
            }
            return View(event_);
        }

        // POST: Event_/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event_ event_ = db.Event_.Find(id);
            db.Event_.Remove(event_);
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
