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
    public class VeterinariansController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Veterinarians
        public ActionResult Index(string searchBy, string search)
        {
            try {
                if (searchBy == "Vet_Name")
                    return View(db.Veterinarians.Where(x => x.Vet_Name == search || search == null).ToList());
                else
                    return View(db.Veterinarians.Where(x => x.Vet_Address == search || search == null).ToList());
            }
            catch (Exception err)
            {
                ViewBag.err = err.Message;
            }
            return View(db.Veterinarians.ToList());
        }

        // GET: Veterinarians/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarian veterinarian = db.Veterinarians.Find(id);
            if (veterinarian == null)
            {
                return HttpNotFound();
            }
            return View(veterinarian);
        }

        // GET: Veterinarians/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veterinarians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vet_ID,Vet_Name,Vet_Emial,Vet_Tel,Vet_Address")] Veterinarian veterinarian)
        {
            
            if (ModelState.IsValid)
            {
                if (db.Veterinarians.Any(p => p.Vet_Name == veterinarian.Vet_Name || p.Vet_Emial == veterinarian.Vet_Emial)) //duplicate data
                {
                    ViewBag.Message = "Vet already exists";
                    /*ModelState.AddModelError("txtName", "Vet Name already exists.")*/
                    
                }

                else {
                    db.Veterinarians.Add(veterinarian);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Saved New Veternarian Successfully";
                    return RedirectToAction("Index");
                }
            }
          
            return View(veterinarian);
        }

        // GET: Veterinarians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarian veterinarian = db.Veterinarians.Find(id);
            if (veterinarian == null)
            {
                return HttpNotFound();
            }
            return View(veterinarian);
        }

        // POST: Veterinarians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vet_ID,Vet_Name,Vet_Emial,Vet_Tel,Vet_Address")] Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                if (db.Veterinarians.Any(p => p.Vet_Name == veterinarian.Vet_Name)) //duplicate data
                {
                    ViewBag.Message = "Vet already exists";
                    /*ModelState.AddModelError("txtName", "Vet Name already exists.")*/

                }

                else {
                    db.Entry(veterinarian).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["EditMessage"] = "Updated Veternarian Details Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(veterinarian);
        }

        // GET: Veterinarians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veterinarian veterinarian = db.Veterinarians.Find(id);
            if (veterinarian == null)
            {
                return HttpNotFound();
            }
            return View(veterinarian);
        }

        // POST: Veterinarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Veterinarian veterinarian = db.Veterinarians.Find(id);
            db.Veterinarians.Remove(veterinarian);
            db.SaveChanges();
            TempData["DeleteMessage"] = "Deleted Veternarian Successfully";
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
