using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NewProductsWebApi.Models
{
    public class ProductsDbContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProductsDbContext() : base("name=ProductsDbContext")
        {
        }

        public System.Data.Entity.DbSet<NewProductsWebApi.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<NewProductsWebApi.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<NewProductsWebApi.Models.ShoppingCart> ShoppingCarts { get; set; }

        public System.Data.Entity.DbSet<NewProductsWebApi.Models.Category> Categories { get; set; }
    }
}
