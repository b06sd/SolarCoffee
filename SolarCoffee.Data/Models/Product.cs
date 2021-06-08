using System;
using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [MaxLength(64)]
        public string Name { get; set; }
        
        [MaxLength(128)]
        public string Description { get; set; }
        
        public decimal CostPrice { get; set; }
        
        public decimal SellingPrice { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsArchived { get; set; }

        public DateTime DateArchived { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }        
    }
}