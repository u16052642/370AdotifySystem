using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AdoptifySystem.Models
{
    public class myLists
    {
        public myLists()
        {

        }
        public List<VetAppReason> reasons { get; set; }
        public List<Animal> animalName { get; set; }
        public List<Animal_Breed> animalBreed { get; set; }
        public List<Animal_Type> animalType{ get; set; }
        public List<Veterinarian> vetName { get; set; }
        public List<patients> patient { get; set; }

       


    }
}