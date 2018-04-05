using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using NewProductsWebApi.Models;
using NewProductsWebApi.Interfaces;

namespace NewProductsWebApi.Classes
{
    public class ProductsRepository : IRepository
    {
        private ProductsDbContext db = new ProductsDbContext();

        public IEnumerable<Product> GetAllProducts()
        {
            return db.Products.ToList();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return db.Categories.ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(Guid categoryId)
        {
            var products = db.Products
                            .Where(product => product.Category.Id == categoryId)
                            .ToList();

            return products;
        }

        public string GetProductInfo(Guid productId)
        {
            var productInfo = db.Products
                            .Where(product => product.Id == productId)
                            .Select(product => product.Info)
                            .FirstOrDefault();

            return productInfo;
        }

       
        public IEnumerable<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        private ShoppingCart GetShoppingCartForCustomer(string customerId)
        {
            return db.ShoppingCarts
                   .Where(cart => cart.Customer.Id == customerId)
                   .FirstOrDefault();
        }

        private Customer AddCustomerToDb(string customerId)
        {
            Customer customer = new Customer { Id = customerId, Balance = 100 };
            db.Customers.Add(customer);
            db.SaveChanges();
            return customer;
        }

        private ShoppingCart CreateNewShoppingCartForCustomer(string customerId)
        {
            var customer = db.Customers.Find(customerId);

            if (customer == null)
                customer = AddCustomerToDb(customerId);

            var newShoppingCart = new ShoppingCart
            {
                Customer = customer,
                Products = new List<Product>()
            };

            db.ShoppingCarts.Add(newShoppingCart);
            db.SaveChanges();

            return newShoppingCart;
        }

        public IEnumerable<Product> GetProductsFromCart(string customerId)
        {
            var shoppingCart = GetShoppingCartForCustomer(customerId);

            if (shoppingCart == null)
                return null;

            var products = shoppingCart.Products.ToList();

            return products;
        }


        public bool BuyProductsFromCart(string customerId)
        {
            var shoppingCart = GetShoppingCartForCustomer(customerId);

            if (shoppingCart == null)
                return false;

            decimal totalPrice = shoppingCart.Products
                                .Select(product => product.Price)
                                .Sum();

            if (shoppingCart.Customer.Balance < totalPrice)
                return false;

            foreach (var product in shoppingCart.Products)
            {
                shoppingCart.Products.Remove(product);
            }

            shoppingCart.Customer.Balance -= totalPrice;

            db.SaveChanges();

            return true;
        }


        public ShoppingCart AddProductToCart(string customerId, Guid productId)
        {
            ShoppingCart shoppingCart = GetShoppingCartForCustomer(customerId);

            if (shoppingCart == null)
                shoppingCart = CreateNewShoppingCartForCustomer(customerId);

            Product product = db.Products.Find(productId);

            if (product == null)
                return null;

            shoppingCart.Products.Add(product);

            db.SaveChanges();

            return shoppingCart;
        }











    }
}