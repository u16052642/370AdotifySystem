using AdoptifySystem.Models;
using AdoptifySystem.Models.nickeymodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdoptifySystem.Controllers.Zinhle
{
    public class AnimalController : Controller
    {
        // GET: Animal

        Wollies_ShelterEntities db = new Wollies_ShelterEntities();
        static public Innovation innovation = new Innovation();

     
        public ActionResult AddTemporaryAnimal()
        {
            try
            {
                innovation.animalTypes = db.Animal_Type.ToList();
                innovation.breedTypes = db.Animal_Breed.ToList();
                
                return View(innovation);
            }
            catch (Exception e)
            {
                TempData["EditMessage"] = e.Message;
                return RedirectToAction("Index","Employees");

            }

        }
        [HttpPost]
        public ActionResult AddTemporaryAnimal(Animal animal, int[] Animal_Breed,Microchip micro,HttpPostedFileBase animalPicture)
        {
            try
            {
                if(animalPicture != null)
                {
                    //this is where we convert the contract to add to the database
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(animalPicture.InputStream))
                    {

                        bytes = br.ReadBytes(animalPicture.ContentLength);
                    }
                    animal.Animal_Image_Name = Path.GetFileName(animalPicture.FileName);
                    animal.Animal_Image_Type = animalPicture.ContentType;
                    animal.Animal_Image = bytes;

                }
                if(animal !=null)
                {
                    Animal_Status status = db.Animal_Status.Where(zz => zz.Animal_Status_Name == "Available").FirstOrDefault();
                    if (status == null)
                    {
                        TempData["EditMessage"] = "";
                        return RedirectToAction("AddTemporaryAnimal");
                    }
                    animal.Animal_Status_ID = status.Animal_Status_ID;
                    db.Animals.Add(animal);
                    db.SaveChanges();
                }

                if (micro != null)
                {
                    Animal animalid = db.Animals.Where(zz => zz.Animal_Name == animal.Animal_Name && zz.Animal_Size == animal.Animal_Size && zz.Animal_Age == animal.Animal_Age && zz.Animal_Entry_Date == animal.Animal_Entry_Date).FirstOrDefault();
                    micro.Animal_ID = animalid.Animal_ID;
                    db.Microchips.Add(micro);
                    db.SaveChanges();
                }
                TempData["SuccessMessage"] = "";




                return RedirectToAction("Index", "Employees");
            }
            catch (Exception e)
            {
                TempData["EditMessage"] = e.Message;
                return RedirectToAction("Index", "Employees");

            }


            
        }

        public ActionResult SearchAnimal()
        {
            List<Animal> animals = new List<Animal>();
            try
            {
                animals = db.Animals.ToList();
            }
            catch (Exception e)
            {
                TempData["EditMessage"] = e.Message;
                return RedirectToAction("AddTemporaryAnimal", "Animal");
            }


            return View(animals);
        }
        [HttpPost]
        public ActionResult SearchAnimal(string search)
        {
            
            if (!(search == ""))
            {
                var animallist = db.Animals.Where(z=>z.Animal_Name.Equals(search)).ToList();
                if (animallist == null)
                {
                    return RedirectToAction("SearchAnimal");
                }
                List<Animal> animals = new List<Animal>();
                animals = animallist;
                
                return View("SearchAnimal",animals);

            }
            TempData["SuccessMessage"] = "Enter Valid Details";
            return View();
        }

        public ActionResult MaintainAnimal(int? id)
        {
            try
            {
                Animal animals = db.Animals.Find(id);

                if (animals == null)
                {
                    TempData["EditMessage"] = "Animal not Found";
                    return View("SearchAnimal");
                }
                var micro = db.Microchips.Where(z=>z.Animal_ID ==animals.Animal_ID).FirstOrDefault();
                if (micro != null)
                {
                    innovation.micro = micro;
                }
                
                innovation.animal = animals;
                var emp = db.Animal_Type.ToList();
                var breed = db.Animal_Breed.ToList();
                innovation.animalTypes = emp;
                innovation.breedTypes = breed;
            }
            catch (Exception e)
            {
                TempData["EditMessage"] = e.Message;
                return View("SearchAnimal");
            }
            return View(innovation);
        }

        [HttpPost]
        public ActionResult MaintainAnimal(Animal animal, Microchip micro, HttpPostedFileBase animalPicture, string button)
        {
            if (button == "Save")
            {
                try
                {
                    Animal searchanimal = db.Animals.Find(animal.Animal_ID);
                    if (searchanimal == null)
                    {
                        return HttpNotFound();
                    }
                    if(animalPicture != null)
                    {

                        //this is where we convert the contract to add to the database
                        byte[] bytes;
                        using (BinaryReader br = new BinaryReader(animalPicture.InputStream))
                        {

                            bytes = br.ReadBytes(animalPicture.ContentLength);
                        }
                        animal.Animal_Image_Name = Path.GetFileName(animalPicture.FileName);
                        animal.Animal_Image_Type = animalPicture.ContentType;
                        animal.Animal_Image = bytes;
                    }
                        db.Entry(searchanimal).CurrentValues.SetValues(animal);
                        db.SaveChanges();
                    TempData["SuccessMessage"] = "Successfully";
                }
                catch (Exception e)
                {
                    ViewBag.err = e.Message;
                    return RedirectToAction("MaintainDonationType", "Donations");
                }
            }
            else if (button == "Cancel")
            {
                TempData["SuccessMessage"] = "Successfully";
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AddAnimalType()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddAnimalType(Animal_Type animal_type, string button)
        {

            ViewBag.errorMessage = "";
            //Donation_Type asd = new Donation_Type();
            if (button == "Save")
            {
                try
                {

                    List<Animal_Type> Animal_Type = new List<Animal_Type>();
                    Animal_Type = db.Animal_Type.ToList();
                    if (Animal_Type.Count != 0)
                    {
                        int count = 0;
                        foreach (var item in Animal_Type)
                        {
                            if (item.Animal_Type_Name == animal_type.Animal_Type_Name)
                            {
                                count++;
                                ViewBag.errorMessage = "There is a duplicate Donation Type Already";
                                return View();
                            }

                        }
                        if (count == 0)
                        {
                            db.Animal_Type.Add(animal_type);
                            db.SaveChanges();
                            TempData["SuccessMessage"] = "Successfully Stored";
                        }
                    }
                    else
                    {

                        db.Animal_Type.Add(animal_type);
                        db.SaveChanges();

                        TempData["SuccessMessage"] = "Successfully Stored";
                    }

                }
                catch (Exception e)
                {
                    TempData["EditMessage"] = "There was an Error with network please try again: " + e.Message;
                    return View();
                }

            }
            else if (button == "Cancel")
            {
                
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MainatainAnimalType(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Animal_Type animal_type = db.Animal_Type.Find(id);
            if (animal_type == null)
            {
                return HttpNotFound();
            }
            return View(animal_type);
        }
        [HttpPost]
        public ActionResult MainatainAnimalType(Animal_Type animal_Type, string button)
        {
            if (button == "Save")
            {
                try
                {
                    Animal_Type searchaniaml = db.Animal_Type.Find(animal_Type.Animal_Type_ID);
                    if (searchaniaml == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Entry(searchaniaml).CurrentValues.SetValues(animal_Type);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    TempData["EditMessage"] = e.Message;
                    return RedirectToAction("MaintainDonationType", "Donations");
                }
            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        public ActionResult SearchAnimalType()
        {
            ViewBag.errormessage = "";
            List<Animal_Type> animal_Types = new List<Animal_Type>();
            try
            {
                animal_Types = db.Animal_Type.ToList();
                if(animal_Types.Count == 0)
                {

                }
                return View(animal_Types);
            }
            catch (Exception e)
            {
                ViewBag.errormessage = "there was a network error: "+ e.Message;
                return View();
            }
        }
        [HttpGet]
        public ActionResult SearchAnimalType(string search)
        {
            return View();
        }
            public ActionResult AddBreedType()
        {
            try
            {
                innovation.animalTypes = db.Animal_Type.ToList();
                return View(innovation);
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpPost]
        public ActionResult AddBreedType(Animal_Breed animal_breed, string button)
        {
            ViewBag.errorMessage = "";
            if (button == "Save")
            {
                try
                {

                    List<Animal_Breed> Animal_Breeds = new List<Animal_Breed>();
                    Animal_Breeds = db.Animal_Breed.ToList();
                    if (Animal_Breeds.Count != 0)
                    {
                        int count = 0;
                        foreach (var item in Animal_Breeds)
                        {
                            if (item.Animal_Breed_Name == animal_breed.Animal_Breed_Name)
                            {
                                count++;
                                ViewBag.errorMessage = "There is a duplicate Donation Type Already";
                                return View();
                            }

                        }
                        if (count == 0)
                        {
                            db.Animal_Breed.Add(animal_breed);
                            db.SaveChanges();
                        }
                    }
                    else
                    {

                        db.Animal_Breed.Add(animal_breed);
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

        public ActionResult MaintainBreedType(int? id)
        {
            try
            {
                if (id != null)
                {
                    innovation.animalTypes = db.Animal_Type.ToList();
                    var breed = db.Animal_Breed.Find(id);
                    if (breed == null)
                    {
                        return View("AddBreedType");
                    }
                    innovation.breed = breed;
                    return View(innovation);
                }
                return View("AddBreedType");
            }
            catch (Exception e)
            {
                ViewBag.err = e.Message;
                throw;
            }
        }
        [HttpPost]
        public ActionResult MaintainBreedType(Animal_Breed animal_breed, string button)
        {
            if (button == "Save")
            {
                try
                {
                    Animal_Breed searchbreed = db.Animal_Breed.Find(animal_breed.Animal_Breed_ID);
                    if (searchbreed == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        db.Entry(searchbreed).CurrentValues.SetValues(animal_breed);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.err = e.Message;
                    return RedirectToAction("MaintainDonationType", "Donations");
                }
            }
            else if (button == "Cancel")
            {

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SearchBreedType()
        {
            return View();
        }

        //public ActionResult ClaimAnimal()
        //{
        //    //try
        //    //{
        //    //    innovation.animals = db.Animals.Select( x=> new Animal { Animal_Name = x.Animal_Name}).ToList();
             

        //    //    return View(innovation);
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    ViewBag.err = e.Message;
        //    //    return RedirectToAction("Index", "Employees");

        //    //}

        //}

        //[HttpPost]
        //public ActionResult ClaimAnimal()
        //{


 
        //        Animal animal = new Animal();
        //        Animal_Status status = db.Animal_Status.Where(zz => zz.Animal_Status_Name == "Temporary").FirstOrDefault();
        //        animal.Animal_Status_ID = status.Animal_Status_ID;
        //        db.Animals.Add(animal);
        //        db.SaveChanges();
            
            
        //}
    }
}