using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsBot.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        
        public string CategoryName { get; set; }
    }
}