using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem.Models;

namespace AdoptifySystem.Controllers
{
    public class Customer_EventController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Customer_Event
        public ActionResult Index()
        {
            var customer_Event = db.Customer_Event.Include(c => c.Event_).Include(c => c.Payment_Type);
            return View(customer_Event.ToList());
        }

        // GET: Customer_Event/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Event customer_Event = db.Customer_Event.Find(id);
            if (customer_Event == null)
            {
                return HttpNotFound();
            }
            return View(customer_Event);
        }

        // GET: Customer_Event/Create
        public ActionResult Create()
        {
            ViewBag.Event_ID = new SelectList(db.Event_, "Event_ID", "Event_Name");
            ViewBag.Payment_Type_ID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Type_Name");
            return View();
        }

        // POST: Customer_Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Customer_Event_ID,Customer_Event_Name,Customer_Event_Surname,Customer_Event_Email,Number_of_tickects,TicketFee_Date,TicketFee_Total,Event_ID,Payment_Type_ID")] Customer_Event customer_Event)
        {
            if (ModelState.IsValid)
            {
                db.Customer_Event.Add(customer_Event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Event_ID = new SelectList(db.Event_, "Event_ID", "Event_Name", customer_Event.Event_ID);
            ViewBag.Payment_Type_ID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Type_Name", customer_Event.Payment_Type_ID);
            return View(customer_Event);
        }

        // GET: Customer_Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Event customer_Event = db.Customer_Event.Find(id);
            if (customer_Event == null)
            {
                return HttpNotFound();
            }
            ViewBag.Event_ID = new SelectList(db.Event_, "Event_ID", "Event_Name", customer_Event.Event_ID);
            ViewBag.Payment_Type_ID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Type_Name", customer_Event.Payment_Type_ID);
            return View(customer_Event);
        }

        // POST: Customer_Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Customer_Event_ID,Customer_Event_Name,Customer_Event_Surname,Customer_Event_Email,Number_of_tickects,TicketFee_Date,TicketFee_Total,Event_ID,Payment_Type_ID")] Customer_Event customer_Event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer_Event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Event_ID = new SelectList(db.Event_, "Event_ID", "Event_Name", customer_Event.Event_ID);
            ViewBag.Payment_Type_ID = new SelectList(db.Payment_Type, "Payment_Type_ID", "Payment_Type_Name", customer_Event.Payment_Type_ID);
            return View(customer_Event);
        }

        // GET: Customer_Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer_Event customer_Event = db.Customer_Event.Find(id);
            if (customer_Event == null)
            {
                return HttpNotFound();
            }
            return View(customer_Event);
        }

        // POST: Customer_Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer_Event customer_Event = db.Customer_Event.Find(id);
            db.Customer_Event.Remove(customer_Event);
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
