using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewProductsWebApi.Models
{
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }

        public virtual List<Product> Products { get; set; }

        public virtual Customer Customer { get; set; }


    }
}