using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem;

namespace AdoptifySystem.Controllers.Cassie
{
    public class KennelsController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Kennels
        public ActionResult Index()
        {
            return View(db.Kennels.ToList());
        }

        // GET: Kennels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kennel kennel = db.Kennels.Find(id);
            if (kennel == null)
            {
                return HttpNotFound();
            }
            return View(kennel);
        }

        // GET: Kennels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kennels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Kennel_ID,Kennel_Name,Kennel_Number,Kennel_Capacity")] Kennel kennel)
        {
            if (ModelState.IsValid)
            {
                db.Kennels.Add(kennel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kennel);
        }

        // GET: Kennels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kennel kennel = db.Kennels.Find(id);
            if (kennel == null)
            {
                return HttpNotFound();
            }
            return View(kennel);
        }

        // POST: Kennels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Kennel_ID,Kennel_Name,Kennel_Number,Kennel_Capacity")] Kennel kennel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kennel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kennel);
        }

        // GET: Kennels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kennel kennel = db.Kennels.Find(id);
            if (kennel == null)
            {
                return HttpNotFound();
            }
            return View(kennel);
        }

        // POST: Kennels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kennel kennel = db.Kennels.Find(id);
            db.Kennels.Remove(kennel);
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
