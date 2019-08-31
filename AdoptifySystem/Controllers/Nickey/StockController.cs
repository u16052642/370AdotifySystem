using AdoptifySystem;
using AdoptifySystem.Models.nickeymodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdoptifySystem.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        Wollies_ShelterEntities db = new Wollies_ShelterEntities();
        public static Flexible flex = new Flexible();
        public ActionResult AddStock()
        {
            List<Stock_Type> Stock_Types = new List<Stock_Type>();
            List<Packaging_Type> Packaging_Type = new List<Packaging_Type>();
            List<Unit_Type> unit_Types = new List<Unit_Type>();
            try
            {
                Stock_Types = db.Stock_Type.ToList();
                Packaging_Type = db.Packaging_Type.ToList();
                unit_Types = db.Unit_Type.ToList();
                flex.Stock_Types = Stock_Types;
                flex.packaging_Types = Packaging_Type;
                flex.unit_Types = unit_Types;
            }
            catch (Exception)
            {

                return RedirectToAction("Index", "Home");
            }


            return View(flex);
        }
        [HttpPost]
        public ActionResult AddStock(Stock stock,string button)
        {
            try
            {
                if (button=="Save")
                {
                    var searchstock = db.Stocks.Where(z=>z.Packaging_Type_ID == stock.Packaging_Type_ID && z.Stock_Description == stock.Stock_Description && z.Unit_Type_ID == stock.Unit_Type_ID && z.Unit_number == stock.Unit_number).FirstOrDefault();
                    if (searchstock==null)
                    {
                        db.Stocks.Add(stock);
                        db.SaveChanges();
                    }
                    return RedirectToAction("AddStock");
                }
                if (button == "Cancel")
                {

                }
            }
            catch (Exception e)
            {
                

                    return RedirectToAction("AddStock");
            }
            return View("Index","Home");
        }
        public ActionResult SearchStock()
        {
            ViewBag.errormessage = "";
            List<Stock> stock = new List<Stock>();
            try
            {
                stock = db.Stocks.ToList();
                if (stock.Count == 0)
                {

                }
                return View(stock);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = "there was a network error: " + e.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult SearchStock(Stock stock)
        {
            return View();
        }
        public ActionResult MaintainStock()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MaintainStock(Stock stock2, string button)
        {
            if (button == "Save")
            {
                try
                {
                    Stock stock1 = db.Stocks.Find(stock2.Stock_ID);
                    if (stock1 == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Entry(stock1).CurrentValues.SetValues(stock2);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.err = e.Message;
                    return RedirectToAction("MaintainStock", "Stock");
                }
            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CaptureStockTake(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<Stock_Type> Stock_Types = new List<Stock_Type>();
                List<Packaging_Type> Packaging_Type = new List<Packaging_Type>();
                List<Unit_Type> unit_Types = new List<Unit_Type>();

                Stock_Types = db.Stock_Type.ToList();
                Packaging_Type = db.Packaging_Type.ToList();
                unit_Types = db.Unit_Type.ToList();
                flex.Stock_Types = Stock_Types;
                flex.packaging_Types = Packaging_Type;
                flex.unit_Types = unit_Types;

                Stock stock_ = db.Stocks.Find(id);
                if (stock_ == null)
                {
                    return HttpNotFound();
                }
                flex.stock = stock_;
                return View(flex);

            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult CaptureStockTake(Stock stock, string button)
        {
            try
            {
                if (button == "Save")
                {
                    Stock oldstock = db.Stocks.Find(stock.Stock_ID);
                    Stock newstock = db.Stocks.Find(stock.Stock_ID);
                    if (oldstock == null)
                    {
                        ViewBag.err = "Error not found";
                        return HttpNotFound();
                    }
                    if(!(oldstock.Stock_Quantity > stock.Stock_Quantity))
                    {
                        ViewBag.err = "Quantity is will be in negatives";
                        return HttpNotFound(); 
                    }
                    newstock.Stock_Quantity -= stock.Stock_Quantity;
                    db.Entry(oldstock).CurrentValues.SetValues(newstock);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                if (button == "Cancel")
                {
                    return RedirectToAction("searchstock");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
        public ActionResult AddStockType()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStockType(Stock_Type stock_type,string button)
        {
            ViewBag.errorMessage = "";
            if (button == "Save")
            {
                try
                {

                    List<Stock_Type> stock_types = new List<Stock_Type>();
                    stock_types = db.Stock_Type.ToList();
                    if (stock_types.Count != 0)
                    {
                        int count = 0;
                        foreach (var item in stock_types)
                        {
                            if (item.Stock_Type_Name == stock_type.Stock_Type_Name && item.Stock_Type_Description == stock_type.Stock_Type_Description)
                            {
                                count++;
                                ViewBag.errorMessage = "There is a duplicate Stock Type Already";
                                return View();
                            }

                        }
                        if (count == 0)
                        {
                            db.Stock_Type.Add(stock_type);
                            db.SaveChanges();
                        }
                    }
                    else
                    {

                        db.Stock_Type.Add(stock_type);
                        db.SaveChanges();


                    }

                }
                catch (Exception e)
                {
                    ViewBag.errorMessage = "There was an Error with network please try again: " + e.Message;
                    return View();
                }

            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult ReceiveStock(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                List<Stock_Type> Stock_Types = new List<Stock_Type>();
                List<Packaging_Type> Packaging_Type = new List<Packaging_Type>();
                List<Unit_Type> unit_Types = new List<Unit_Type>();
               
                    Stock_Types = db.Stock_Type.ToList();
                    Packaging_Type = db.Packaging_Type.ToList();
                    unit_Types = db.Unit_Type.ToList();
                    flex.Stock_Types = Stock_Types;
                    flex.packaging_Types = Packaging_Type;
                    flex.unit_Types = unit_Types;
                
                Stock stock_ = db.Stocks.Find(id);
                if (stock_ == null)
                {
                    return HttpNotFound();
                }
                flex.stock = stock_;
                return View(flex);

            }
            catch (Exception)
            {

                throw;
            }
            
        }
        [HttpPost]
        public ActionResult ReceiveStock(Stock stock, string button)
        {
            try
            {
                if (button == "Save")
                {
                    Stock oldstock = db.Stocks.Find(stock.Stock_ID);
                    Stock newstock = db.Stocks.Find(stock.Stock_ID);
                    if (oldstock == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        newstock.Stock_Quantity += stock.Stock_Quantity; 
                        db.Entry(oldstock).CurrentValues.SetValues(newstock);
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index","Home");
                }
                if (button == "Cancel")
                {
                    return RedirectToAction("searchstock");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet]
        public ActionResult SearchStockType()
        {
            ViewBag.errormessage = "";
            List<Stock_Type> stock_Types = new List<Stock_Type>();
            try
            {
                stock_Types = db.Stock_Type.ToList();
                if (stock_Types.Count == 0)
                {

                }
                return View(stock_Types);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = "there was a network error: " + e.Message;
                return View();
            }

        }
        public ActionResult MaintainStockType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stock_Type stock_Type = db.Stock_Type.Find(id);
            if (stock_Type == null)
            {
                return HttpNotFound();
            }
            return View(stock_Type);
        }
        [HttpPost]
        public ActionResult MaintainStockType(Stock_Type stock_Type,string button)
        {
            if (button == "Save")
            {
                try
                {
                    Stock_Type Stock_Type = db.Stock_Type.Find(stock_Type.Stock_Type_ID);
                    if (Stock_Type == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Entry(Stock_Type).CurrentValues.SetValues(stock_Type);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.err = e.Message;
                    return RedirectToAction("MaintainStockType", "Stock");
                }
            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}