namespace NewProductsWebApi.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NewProductsWebApi.Models.ProductsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NewProductsWebApi.Models.ProductsDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. 

            var vegetablesCategory = new Category { Id = Guid.NewGuid(), CategoryName = "Vegetables" };
            var fruitsCategory = new Category { Id = Guid.NewGuid(), CategoryName = "Fruits" };
            var dishesCategory = new Category { Id = Guid.NewGuid(), CategoryName = "Dishes" };
            var appliancesCategory = new Category { Id = Guid.NewGuid(), CategoryName = "Appliances" };
            var cutleryCategory = new Category { Id = Guid.NewGuid(), CategoryName = "Cutlery" };

            context.Categories.AddOrUpdate(
                vegetablesCategory,
                fruitsCategory,
                dishesCategory,
                appliancesCategory,
                cutleryCategory
                );

            context.Products.AddOrUpdate(

                new Product { Id = Guid.NewGuid(), Name = "Apples", Category = fruitsCategory, Info = "Delicious fresh apples", Price = 11 },
                new Product { Id = Guid.NewGuid(), Name = "Bananas", Category = fruitsCategory, Info = "Delicious fresh bananas", Price = 15 },
                new Product { Id = Guid.NewGuid(), Name = "Plate", Category = dishesCategory, Info = "Beautiful plate", Price = 12 },
                new Product { Id = Guid.NewGuid(), Name = "Knife", Category = cutleryCategory, Info = "Beautiful knife", Price = 20 },
                new Product { Id = Guid.NewGuid(), Name = "Cup", Category = dishesCategory, Info = "Beautiful cup", Price = 8 },
                new Product { Id = Guid.NewGuid(), Name = "Spoon", Category = cutleryCategory, Info = "Beautiful spoon", Price = 10 },
                new Product { Id = Guid.NewGuid(), Name = "Fork", Category = cutleryCategory, Info = "Beautiful fork", Price = 18 }
           );

            context.Customers.AddOrUpdate(
                new Customer { Id = "12345", Balance = 100 },
                new Customer { Id = "56789", Balance = 100 },
                new Customer { Id = "98765", Balance = 100 }
                );
        }
    }
}
