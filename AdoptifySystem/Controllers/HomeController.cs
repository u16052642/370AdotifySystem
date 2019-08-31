using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using AdoptifySystem;



namespace AdoptifySystem.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
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