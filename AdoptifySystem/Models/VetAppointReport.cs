using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models
{
    public class VetAppointReport
    {
        public Vet_Appointment_Master AppointList { get; set; }
        public Veterinarian VetList { get; set; }
        public Animal AnimalList { get; set; }
    }
}