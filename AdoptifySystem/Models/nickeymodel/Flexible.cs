
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models.nickeymodel
{
    public class Flexible
    {

        //these are the list of classes
        public List<Employee> employeelist { get; set; }
        public List<Packaging_Type> packaging_Types { get; set; }
        public List<Unit_Type> unit_Types { get; set; }
        public List<Title> Titles { get; set; }
        public List<Donor> DonorList { get; set; }
        public List<Stock> Stocklist { get; set; }
        public List<Stock_Type> Stock_Types { get; set; }
        public List<Donation_Line> adddonationlist { get; set; }
        public List<Foster_Care> Fostercarelist { get; set; }
        public List<Animal> animallist { get; set; }
        public List<Foster_Care_Parent> fostercareparent { get; set; }

        //these are the single classes
        public Donor donor { get; set; }
        public Employee employee { get; set; }
        public Foster_Care_Parent parent { get; set; }
        public Animal animal { get; set; }
        public Stock stock { get; set; }
        public Donation donation { get; set; }

        public User_ currentuser { get; set; }
    }
}