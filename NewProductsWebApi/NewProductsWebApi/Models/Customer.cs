using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewProductsWebApi.Models
{
    public class Customer
    {
        [Key]
        public string Id { get; set; }

        public decimal Balance { get; set; }
    }
}