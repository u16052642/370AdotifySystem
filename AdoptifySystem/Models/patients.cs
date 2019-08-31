using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models
{
    public class patient
    {
        public string appDate { get; set; }
        public string reasons { get; set; }
        public string animalName { get; set; }
        public string description { get; set; }

        public string vetName
        {
            get; set;
        }
    }
}