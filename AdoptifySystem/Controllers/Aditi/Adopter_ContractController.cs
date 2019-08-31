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
    public class Adopter_ContractController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Adopter_Contract
        public ActionResult Index()
        {
            var adopter_Contract = db.Adopter_Contract.Include(a => a.Adopter);
            return View(adopter_Contract.ToList());
        }

        // GET: Adopter_Contract/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter_Contract adopter_Contract = db.Adopter_Contract.Find(id);
            if (adopter_Contract == null)
            {
                return HttpNotFound();
            }
            return View(adopter_Contract);
        }

        // GET: Adopter_Contract/Create
        public ActionResult Create()
        {
            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name");
            return View();
        }

        // POST: Adopter_Contract/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Contract_ID,Electronic_Contract,Adopter_ID")] Adopter_Contract adopter_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Adopter_Contract.Add(adopter_Contract);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name", adopter_Contract.Adopter_ID);
            return View(adopter_Contract);
        }

        // GET: Adopter_Contract/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter_Contract adopter_Contract = db.Adopter_Contract.Find(id);
            if (adopter_Contract == null)
            {
                return HttpNotFound();
            }
            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name", adopter_Contract.Adopter_ID);
            return View(adopter_Contract);
        }

        // POST: Adopter_Contract/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Contract_ID,Electronic_Contract,Adopter_ID")] Adopter_Contract adopter_Contract)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adopter_Contract).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name", adopter_Contract.Adopter_ID);
            return View(adopter_Contract);
        }

        // GET: Adopter_Contract/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter_Contract adopter_Contract = db.Adopter_Contract.Find(id);
            if (adopter_Contract == null)
            {
                return HttpNotFound();
            }
            return View(adopter_Contract);
        }

        // POST: Adopter_Contract/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adopter_Contract adopter_Contract = db.Adopter_Contract.Find(id);
            db.Adopter_Contract.Remove(adopter_Contract);
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
