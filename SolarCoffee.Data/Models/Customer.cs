using System;
using System.Collections.Generic;
using System.Text;

namespace SolarCoffee.Data.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public CustomerAddress PrimaryAddress { get; set; }
    }
}
