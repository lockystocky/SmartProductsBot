using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewProductsWebApi.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Range(0.0, 100000.0)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(250)]
        public string Info { get; set; }

        [Required]
        public virtual Category Category { get; set; }
    }
}