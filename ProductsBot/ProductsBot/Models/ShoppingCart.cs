using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsBot.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}