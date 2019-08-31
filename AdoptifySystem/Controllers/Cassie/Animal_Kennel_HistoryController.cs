using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdoptifySystem.Models;
using OfficeOpenXml;

namespace AdoptifySystem.Controllers.Cassie
{
    public class Animal_Kennel_HistoryController : Controller
    {
        private Wollies_ShelterEntities db = new Wollies_ShelterEntities();

        // GET: Animal_Kennel_History
        public ActionResult Index()
        {
            List<KennelHistoryViewModel> kenlist = db.Animal_Kennel_History.Select(x => new KennelHistoryViewModel
            {
                Animal_Kennel_History_ID = x.Animal_Kennel_History_ID,
                Animal_ID = x.Animal_ID,
                Kennel_ID = x.Kennel_ID,
            }).ToList();
            return View(kenlist);
        }

        // GET: Animal_Kennel_History/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Kennel_History animal_Kennel_History = db.Animal_Kennel_History.Find(id);
            if (animal_Kennel_History == null)
            {
                return HttpNotFound();
            }
            return View(animal_Kennel_History);
        }

        // GET: Animal_Kennel_History/Create
        public ActionResult Create()
        {
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name");
            ViewBag.Kennel_ID = new SelectList(db.Kennels, "Kennel_ID", "Kennel_Name");
            return View();
        }

        // POST: Animal_Kennel_History/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Animal_Kennel_History_ID,Animal_ID,Kennel_ID")] Animal_Kennel_History animal_Kennel_History)
        {
            if (ModelState.IsValid)
            {
                db.Animal_Kennel_History.Add(animal_Kennel_History);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name", animal_Kennel_History.Animal_ID);
            ViewBag.Kennel_ID = new SelectList(db.Kennels, "Kennel_ID", "Kennel_Name", animal_Kennel_History.Kennel_ID);
            return View(animal_Kennel_History);
        }

        // GET: Animal_Kennel_History/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Kennel_History animal_Kennel_History = db.Animal_Kennel_History.Find(id);
            if (animal_Kennel_History == null)
            {
                return HttpNotFound();
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name", animal_Kennel_History.Animal_ID);
            ViewBag.Kennel_ID = new SelectList(db.Kennels, "Kennel_ID", "Kennel_Name", animal_Kennel_History.Kennel_ID);
            return View(animal_Kennel_History);
        }

        // POST: Animal_Kennel_History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Animal_Kennel_History_ID,Animal_ID,Kennel_ID")] Animal_Kennel_History animal_Kennel_History)
        {
            if (ModelState.IsValid)
            {
                db.Entry(animal_Kennel_History).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Animal_ID = new SelectList(db.Animals, "Animal_ID", "Animal_Name", animal_Kennel_History.Animal_ID);
            ViewBag.Kennel_ID = new SelectList(db.Kennels, "Kennel_ID", "Kennel_Name", animal_Kennel_History.Kennel_ID);
            return View(animal_Kennel_History);
        }

        // GET: Animal_Kennel_History/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Kennel_History animal_Kennel_History = db.Animal_Kennel_History.Find(id);
            if (animal_Kennel_History == null)
            {
                return HttpNotFound();
            }
            return View(animal_Kennel_History);
        }

        // POST: Animal_Kennel_History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Animal_Kennel_History animal_Kennel_History = db.Animal_Kennel_History.Find(id);
            db.Animal_Kennel_History.Remove(animal_Kennel_History);
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

        public void ExportToExcel()
        {
            List<KennelHistoryViewModel> kenlist = db.Animal_Kennel_History.Select(x => new KennelHistoryViewModel
            {
                Animal_Kennel_History_ID = x.Animal_Kennel_History_ID,
                Animal_ID = x.Animal_ID,
                Kennel_ID = x.Kennel_ID,
            }).ToList();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Com1";

            ws.Cells["A2"].Value = "Report";
            ws.Cells["B2"].Value = "Report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "Animal_Kennel_History_ID";
            ws.Cells["B6"].Value = "Animal_ID";
            ws.Cells["C6"].Value = "Kennel_ID";

            int rowStart = 7;
            foreach (var item in kenlist)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.Animal_Kennel_History_ID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.Animal_ID;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.Kennel_ID;
                rowStart++;


            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();


        }
    }
}
