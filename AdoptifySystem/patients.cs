using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models
{
    public class patients
    {
        private string appDateM;
        private string reasonsM;
        private string animalNameM;
        private string descriptionM;
        private string vetNameM;

        public patients()
        {
            appDateM="";
            reasonsM="";
           animalNameM="";
             descriptionM="";
            vetNameM = "";
        }

        public string appDate {
            get { return appDateM; }
            set { appDateM = value; }
             }
        public string reasons {
            get { return reasonsM; }
            set { reasonsM = value; }

        }
        public string animalName {
            get { return animalNameM; }
            set { animalNameM = value; }
        }
        public string description {
            get { return descriptionM; }
            set { descriptionM = value; }
        }

        public string vetName
        {
            get { return vetNameM; }
            set { vetNameM = value; }

        }
    }
}