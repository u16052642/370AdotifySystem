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
    public class AdoptionsController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();
       static  List<Adoption> myList = new List<Adoption>();
       static List<Animal> animal2 = new List<Animal>();
       static List<Animal> animalsss = new List<Animal>();
        static Adoption adoption = new Adoption();
        static int Id = 0;
        // GET: Adoptions
        public ActionResult Index(string searchBy, string search)
        {
            //var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.Payment).Include(a => a.Adoption_Status);
           
            try
            {
                if (searchBy == "Animal_Name")
                    return View(db.Adoptions.Where(x => x.Animal.Animal_Name == search || search == null).ToList());
                else
                    return View(db.Adoptions.Where(x => x.Adopter.Adopter_Name == search || search == null).ToList());
            }
            catch (Exception err)
            {
                ViewBag.err = err.Message;
            }
            return View(db.Adoptions.ToList());
        }
      

        public ActionResult HomeCheckSchedule(int? id)
        {

            List<Adoption> adoption1 = db.Adoptions.ToList();

            Id = Convert.ToInt32(id);
            Adoption adoption = db.Adoptions.Find(id);//Display the animal details object
            if (adoption != null)
            {
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " "+ adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                   return View("HomeCheckSchedule");
            }
       
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            return View("HomeCheckSchedule");
        }
        public ActionResult Save(String NDate="")
        {
            DateTime dd = new DateTime();
            Adoption adoption = db.Adoptions.Find(Id);
            if (NDate != "")
            {
                String year = NDate.Substring(0, 4);
                String month = NDate.Substring(5, 2);
                String day = NDate.Substring(8, 2);
                 dd = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            }

           
            if (adoption == null)
            {
               
                return HttpNotFound();
            }

            
           
            adoption.Adopt_Status_ID = 2;

            HomeCheck obj = new HomeCheck();
            obj.Adoption_ID = Id;
            obj.HomeCheck_Datetime = dd;
            db.HomeChecks.Add(obj);
            db.SaveChanges();
            myList = db.Adoptions.ToList();
            TempData["HCSCMessage"] = "Please Save The Current HomeCheck on the Calendar Schedular";
            TempData["HomeCheckMessage"] = "HomeCheck Successfully Booked";
            //return View("HomeCheckSchedule");
            //return View("Index", myList);
            return View("ScheduleHomeCheckCalendar");

        }
        public ActionResult HomeCheckReport(int? id)
        {
            if (id != 0)
            {
                Adoption adoption = db.Adoptions.Find(id);
                ViewBag.ID = adoption.Adoption_ID;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult HomeCheckReport(int? id, string val ="")
        {
            List<Adoption> adoption1 = db.Adoptions.ToList();
            bool flag1 = false;
            Id = Convert.ToInt32(id);
            Adoption adoption = db.Adoptions.Find(id);
            Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);

            if (adoption != null)
            {
                
                if (val== "true")
                {
                    val = "true";
                    flag1 = true;
                    adoption.Adopt_Status_ID = 3;

                    TempData["HomeCheckReportMessage"] = "HomeCheck Report Success ";
                    aaa.Adopt_Status_ID = 3;
                    db.SaveChanges();

                    return View("Index",db.Adoptions);
                }
                else
                {
                    flag1 = false;
                    val = "false";
                    adoption.Adopt_Status_ID = 4;

                    TempData["HomeCheckReportErrorMessage"] = "HomeCheck Report Failed ";
                    aaa.Adopt_Status_ID = 4;
                    db.SaveChanges();
                }


                ViewBag.ID = adoption.Adoption_ID;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString();

               
                

                return View("Index",db.Adoptions);
            }
           
            if (id == null)
            {
                return View("Index"); //new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TempData["HomeCheckReportMessage"] = "HomeCheck Successfully Reported";
            return View("Index");
        }

        public ActionResult CaptureAdoptionPayment(int? id)
        {
            adoption = db.Adoptions.Find(id);//Display the animal details object
            if (adoption != null)
            {
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                ViewBag.Price = adoption.Animal.Animal_Type.Price;

            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaptureAdoptionPayment(int? id, string Payment="")
        {
          

            adoption = db.Adoptions.Find(id);//Display the animal details object
            if (adoption != null)
            {
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                ViewBag.Price = adoption.Animal.Animal_Type.Price;
           
                if (Payment=="Cash")
                {
                    Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
                    aaa.Adopt_Status_ID = 5;
                    //AdoptionPayment obj = db.AdoptionPayments.SingleOrDefault(x => x.Adoption_ID == id);
                    AdoptionPayment obj = new AdoptionPayment();
                    obj.Adoption_Fee = adoption.Animal.Animal_Type.Price;
                    obj.Adoption_ID = id;
                    obj.Animal_Type_ID = adoption.Animal.Animal_Type_ID;
                    obj.Payment_Type_ID = 1;
                    db.AdoptionPayments.Add(obj);
                    db.SaveChanges();
                }
                else if (Payment == "EFT")
                {
                    Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
                    aaa.Adopt_Status_ID = 5;
                    //AdoptionPayment obj = db.AdoptionPayments.SingleOrDefault(x => x.Adoption_ID == id);
                    AdoptionPayment obj = new AdoptionPayment(); 
                    obj.Adoption_Fee = adoption.Animal.Animal_Type.Price;
                    obj.Adoption_ID = id;
                    obj.Animal_Type_ID = adoption.Animal.Animal_Type_ID;
                    obj.Payment_Type_ID = 2;
                    db.AdoptionPayments.Add(obj);
                    db.SaveChanges();
                }
               
                TempData["PaymentMessage"] = "Payment Successfully";
                db.SaveChanges();
               
            }
            Id = Convert.ToInt32(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["PaymentMessage"] = "Payment Successfully";
            return View("CaptureAdoptionPayment");
        }
        public ActionResult CollectAnimal(int? id)
        {
            Adoption adoption = db.Adoptions.Find(id);
            ViewBag.ID = id;
            ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
            ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
            Id = Convert.ToInt32(id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CollectAnimal(int? id, DateTime? date)
        {
            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.AdoptionPayment).ToList();
            List<Adoption> adoption1 = db.Adoptions.ToList();

            Id = Convert.ToInt32(id);
            Adoption adoption = db.Adoptions.Find(id);//Display the animal details object
            if (adoption != null)
            {
                Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                aaa.Adopt_Status_ID = 6;
                DateTime dd = new DateTime();
                //    String year = date.Substring(0, 4);
                //   String month = date.Substring(5, 2);
                //    String day = date.Substring(8, 2);
              
                aaa.Collection_Date = date;
                db.SaveChanges();
                return View("Finalise");
            }
            Id = Convert.ToInt32(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ///TempData["CollectMessage"] = "Animal Successfully Collected";
            return View("Finalise");
        }
        public ActionResult Finalise(int? id)
        {
            //id = Id;
            Adoption adoption = db.Adoptions.Find(id);
            if (adoption != null)
            {
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
                aaa.Adopt_Status_ID = 7;
                aaa.Animal.Animal_Status_ID = 4;
                TempData["FinaliseMessage"] = "CONGRATULATION!!" + " " + adoption.Animal.Animal_Name + " " + "Successfully Adopted by" + " " + adoption.Adopter.Adopter_Name;
                db.SaveChanges();
            }
            TempData["FinaliseMessage"] = "CONGRATULATION!!" + " " + adoption.Animal.Animal_Name + " " + "Successfully Adopted by" + " " + adoption.Adopter.Adopter_Name;
            return Redirect("http://localhost:55003/Adoptions/Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Finalise(string name  ="", int id=0)
        {

             id = Id;
            Adoption adoption = db.Adoptions.Find(id);
            if (adoption != null)
            {
                ViewBag.ID = id;
                ViewBag.IDName = adoption.Adopter.Adopter_Name + " " + adoption.Adopter.Adopter_Surname + ", " + adoption.Adopter.Adopter_Email;
                ViewBag.IDet = adoption.Animal.Animal_Name + ", " + adoption.Animal.Animal_Type.Animal_Type_Name + ", " + adoption.Animal.Animal_Breed.Animal_Breed_Name.ToString() + ", " + adoption.Animal.Animal_Age.ToString() + " Years old";
                Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
                aaa.Adopt_Status_ID = 7;
                aaa.Animal.Animal_Status_ID = 4;
                TempData["FinaliseMessage"] = "CONGRATULATION!!" + " " + adoption.Animal.Animal_Name + " " + "Successfully Adopted by" + " " + adoption.Adopter.Adopter_Name;
                db.SaveChanges();
            }
            TempData["FinaliseMessage"] ="CONGRATULATION!!"+" "+ adoption.Animal.Animal_Name+" "+ "Successfully Adopted by" + " " +adoption.Adopter.Adopter_Name;
            return Redirect("http://localhost:55003/Adoptions/Index");
        }
        public ActionResult ReturnAnimal(int? id)
        {

            Adoption adoption = db.Adoptions.Find(id);

            Adoption aaa = db.Adoptions.FirstOrDefault(x => x.Adoption_ID == id);
            aaa.Adopt_Status_ID = 8;
            aaa.Animal.Animal_Status_ID = 2;
            db.SaveChanges();
            TempData["ReturnMessage"] = "SADDLY!!" + " " + adoption.Animal.Animal_Name + " " + "WAS RETURNED BY ADOPTER:" + " " + adoption.Adopter.Adopter_Name;
            return Redirect("http://localhost:55003/Adoptions/Index");

        }
        public ActionResult ReturnIndex()
        {

            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.AdoptionPayment).ToList();


            foreach (var item in adoptions)
            {
                if (item.Adopt_Status_ID == 7)
                {
                    statusID.Add(item);
                }
            }

            return View(statusID);
        }

        public ActionResult HomeCheckIndex(/*string searchBy, string search*/)
        {

            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.AdoptionPayment).ToList();


            foreach (var item in adoptions)
            {
                if (item.Adopt_Status_ID == 1)
                {
                    statusID.Add(item);
                }
            }

            //try
            //{
            //    if (searchBy == "Animal_Name")
            //        return View(db.Adoptions.Where(x => x.Animal.Animal_Name == search || search == null).ToList());
            //    else
            //        return View(db.Adoptions.Where(x => x.Adopter.Adopter_Name == search || search == null).ToList());
            //}
            //catch (Exception err)
            //{
            //    ViewBag.err = err.Message;
            //}

            return View(statusID);
        }

        public ActionResult AdoptionPaymentIndex()
        {

            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.AdoptionPayment).ToList();


            foreach (var item in adoptions)
            {
                if (item.Adopt_Status_ID == 3)
                {
                    statusID.Add(item);
                }
            }

            return View(statusID);
        }
        public ActionResult CollectionIndex()
        {
            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).Include(a => a.AdoptionPayment).ToList();


            foreach (var item in adoptions)
            {
                if (item.Adopt_Status_ID == 5)
                {
                    statusID.Add(item);
                }
            }

            return View(statusID);
        }
        public ActionResult HomeCheckReportIndex()
        {
            var statusID = new List<Adoption>();
            var adoptions = db.Adoptions.Include(a => a.Adopter).Include(a => a.Animal).ToList();


            foreach (var item in adoptions)
            {
                if (item.Adopt_Status_ID == 2)
                {
                    statusID.Add(item);
                }
            }

            return View(statusID);
        }


        



        // GET: Adoptions/Create
        public ActionResult Create()
        {
            var statusID = new List<Animal>();
            var adoptions = db.Animals.ToList();

            animalsss = db.Animals.ToList();

            foreach (Animal item in animalsss)
            {
                if (item.Animal_Status_ID == 2)
                {
                    statusID.Add(item);
                }

            }
            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name");
            ViewBag.Animal_ID = new SelectList(statusID, "Animal_ID", "Animal_Name");
            ViewBag.Payment_ID = new SelectList(db.Payments, "Payment_ID", "Payment_Description");
            ViewBag.Adopt_Status_ID = new SelectList(db.Adoption_Status, "Adopt_Status_ID", "Adopt_Status_Name");
            return View();
        }

        // POST: Adoptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Adoption_ID,Adoption_Date,Adoption_Form,Payment_ID,Adopter_ID,Adopt_Status_ID,Animal_ID,Collection_Date")] Adoption adoption, string ADate = "")
        {
            var statusID = new List<Animal>();
            var adoptions = db.Animals.ToList();
           
            if (ModelState.IsValid)
            {
                String year = ADate.Substring(0, 4);
                String month = ADate.Substring(5, 2);
                String day = ADate.Substring(8, 2);
                DateTime dd = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
                adoption.Adoption_Date = dd;
                db.Adoptions.Add(adoption);
               
               
               
                 animalsss = db.Animals.ToList();

                foreach (Animal item in animalsss)
                {
                    if (item.Animal_Status_ID == 2)
                    {
                        statusID.Add(item);
                    }

                }


                adoption.Animal.Animal_Status_ID = 3;
                db.SaveChanges();
                TempData["AdoptionCreateMessage"] = "Adoption Process Successfully Created";
                return RedirectToAction("Index");
            }

            ViewBag.Adopter_ID = new SelectList(db.Adopters, "Adopter_ID", "Adopter_Name", adoption.Adopter_ID);
            ViewBag.Animal_ID = new SelectList(statusID, "Animal_ID", "Animal_Name", adoption.Animal_ID);
            //ViewBag.Payment_ID = new SelectList(db.Payments, "Payment_ID", "Payment_Description", adoption.Payment_ID);
            ViewBag.Adopt_Status_ID = new SelectList(db.Adoption_Status, "Adopt_Status_ID", "Adopt_Status_Name", adoption.Adopt_Status_ID);
            return View(adoption);
        }

        
        public ActionResult Delete(int? id)
        {
            Id = Convert.ToInt32(id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adoption adoption = db.Adoptions.Find(id);
            if (adoption == null)
            {
                return HttpNotFound();
            }
            return View(adoption);
        }

        // POST: Adoptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try {
                List<Adoption> n = new List<Adoption>();
                Adoption adoption1 = db.Adoptions.Find(Id);
                adoption1.Animal.Animal_Status_ID = 2;

                db.Adoptions.Remove(adoption1);
            
                TempData["AdoptionDeleteMessage"] = "Adoption Process Successfully Deleted";
             
                db.SaveChanges();
               
            }
            catch {

            }
            
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

        public ActionResult ScheduleHomeCheckCalendar()
        {

            return Redirect("http://localhost:55003/Adoptions/Index");
        }
        public JsonResult GetEvents()
        {
            using (Wollies_ShelterEntities dc = new Wollies_ShelterEntities())
            {
                var events = dc.Event_Schedule.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        [HttpPost]
        public JsonResult SaveEvent(Event_Schedule e)
        {
            var status = false;
            using (Wollies_ShelterEntities dc = new Wollies_ShelterEntities())
            {
                if (e.EventID > 0)
                {
                    //Update the event
                    var v = dc.Event_Schedule.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.EventEnd = e.EventEnd;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColour = e.ThemeColour;
                    }

                }
                else //Add Event
                {
                    dc.Event_Schedule.Add(e);

                }
                TempData["HomeCheckReportMessage"] = "HomeCheck Successfully Scheduled";
                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (Wollies_ShelterEntities dc = new Wollies_ShelterEntities())
            {
                var v = dc.Event_Schedule.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Event_Schedule.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
