using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models.nickeymodel
{
    public class Innovation
    {
        public List<Employee_Type> empTypes { get; set; }
        public List<Employee> empList { get; set; }

        public List<Title> Titles { get; set; }
        public Employee employee { get; set; }

        public List<Animal_Breed> breedTypes { get; set; }
        public List<Animal_Type> animalTypes { get; set; }
        //public Password password { get; set; }

        public List<Role_> Roles { get; set; }
        public Animal_Breed breed { get; set; }
        public Animal animal { get; set; }
        public List<Animal> animals { get; set; }
        public Microchip micro { get; set; }
    }
}