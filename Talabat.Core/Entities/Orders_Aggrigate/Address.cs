﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders_Aggrigate
{
    public class Address
    {

        public Address()
        {
            
        }
        public Address( string firstname  , string lastname , string country , string city , string street)
        {
            FirstName = firstname;
            LastName = lastname;
            Country = country;
            City = city;
            Street = street;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }


    }
}
