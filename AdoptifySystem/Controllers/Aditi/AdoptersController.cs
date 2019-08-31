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
    public class AdoptersController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();
        public IEnumerable<SelectListItem> Titles { get; set; }

        
        //public static Flexible myclass = new Flexible();
        

        //public ActionResult OnGet()
        //{
       
        //    return Page();
        //}
        // GET: Adopters

        public AdoptersController()
        {
            Titles = db.Titles.Select(t => new SelectListItem
            {
                Text = t.Title_Description,
                Value = t.Title_ID.ToString()
            });
            ViewData["Titles"] = Titles;
        }

        public ActionResult Index()
        {
           var adopters = db.Adopters.Include(a => a.Title).Include(a => a.Adopter_Status);
            return View(adopters.ToList());
        }

        // GET: Adopters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter adopter = db.Adopters.Find(id);
            if (adopter == null)
            {
                return HttpNotFound();
            }
            return View(adopter);

        }

        // GET: Adopters/Create
        public ActionResult Create()
        {
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "Title_Description");
            ViewBag.Adopter_Status_ID = new SelectList(db.Adopter_Status, "Adopter_Status_ID", "Adopter_Status_Name");
            return View();
        }

        // POST: Adopters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Adopter_ID,Adopter_Name,Adopter_Surname,Adopter_Email,Title_ID,Adopter_Address,Adopter_PostalAddress,Adopter_HomeNumber,Adopter_WorkNumber,Adopter_CellNumber,Adopter_CarRegistartion_Number,Adopter_Employer,Adopter_Status_ID,Amount_of_Family_Memebers,No_of_Children,Age_of_Children,Property_Securely_Closed,Properyty_Include_Pool,Pool_Secured,Animal_Shelter_Available,Sick_Animal,Sick_Animal_Diagnosis,Animal_Sleep_Location,Given_Animal_Away,HomeCheck_Suburb,Type_of_House,Adopted_Before,Complex_or_Flat,Animal_Allowed,Animal_Captivity,Animal_Vaccines_Updated,Adopter_Occupation")] Adopter adopter)
        {

            try
            {
              if (ModelState.IsValid)
                {
                    db.Adopters.Add(adopter);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "Title_Description", adopter.Title_ID);
                ViewBag.Adopter_Status_ID = new SelectList(db.Adopter_Status, "Adopter_Status_ID", "Adopter_Status_Name", adopter.Adopter_Status_ID);
                return View(adopter);
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                throw;
            }
            
        }

        // GET: Adopters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter adopter = db.Adopters.Find(id);
            if (adopter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "Title_Description", adopter.Title_ID);
            ViewBag.Adopter_Status_ID = new SelectList(db.Adopter_Status, "Adopter_Status_ID", "Adopter_Status_Name", adopter.Adopter_Status_ID);
            return View(adopter);
        }

        // POST: Adopters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Adopter_ID,Adopter_Name,Adopter_Surname,Adopter_Email,Title_ID,Adopter_Address,Adopter_PostalAddress,Adopter_HomeNumber,Adopter_WorkNumber,Adopter_CellNumber,Adopter_CarRegistartion_Number,Adopter_Employer,Adopter_Status_ID,Amount_of_Family_Memebers,No_of_Children,Age_of_Children,Property_Securely_Closed,Properyty_Include_Pool,Pool_Secured,Animal_Shelter_Available,Sick_Animal,Sick_Animal_Diagnosis,Animal_Sleep_Location,Given_Animal_Away,HomeCheck_Suburb,Type_of_House,Adopted_Before,Complex_or_Flat,Animal_Allowed,Animal_Captivity,Animal_Vaccines_Updated, Adopter_Occupation")] Adopter adopter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adopter).State = EntityState.Modified;
                db.SaveChanges();

                Audit_Log audit = new Audit_Log();
                audit.Auditlog_DateTime = DateTime.Now;

                db.Audit_Log.Add(audit);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Title_ID = new SelectList(db.Titles, "Title_ID", "Title_Description", adopter.Title_ID);
            ViewBag.Adopter_Status_ID = new SelectList(db.Adopter_Status, "Adopter_Status_ID", "Adopter_Status_Name", adopter.Adopter_Status_ID);
            return View(adopter);
        }

        // GET: Adopters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adopter adopter = db.Adopters.Find(id);
            if (adopter == null)
            {
                return HttpNotFound();
            }
            return View(adopter);
        }

        // POST: Adopters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adopter adopter = db.Adopters.Find(id);
            db.Adopters.Remove(adopter);
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

        /*public ActionResult AdopterRelative(int? id)
        {
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                //stpre in flexible class
                Adopter adopter = db.Adopters.Find(id);
                if (adopter == null)
                {
                    return HttpNotFound();
                }
                myclass.ARelative = adopter;
                return View(myclass);
            }

        }*/

        public ActionResult Search(string searchBy, string search)
        {
            if (searchBy == "Adopter_Name")
            {
                return View(db.Adopters.Where(c => c.Adopter_Name.Contains(search) || search == null).ToList());
            }
            else
            {
                return View(db.Adopters.Where(c => c.Adopter_Surname.Contains(search) || search == null).ToList());
            }

        }

    }
}
