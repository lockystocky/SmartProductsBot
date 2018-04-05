using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsBot.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public string Info { get; set; }
        
        public virtual Category Category { get; set; }
    }
}