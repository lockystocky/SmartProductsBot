using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProductsWebApi.Models;

namespace NewProductsWebApi.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Category> GetAllCategories();

        IEnumerable<Product> GetProductsByCategory(Guid categoryId);

        string GetProductInfo(Guid productId);

        IEnumerable<Customer> GetAllCustomers();

        IEnumerable<Product> GetProductsFromCart(string customerId);

        bool BuyProductsFromCart(string customerId);

        ShoppingCart AddProductToCart(string customerId, Guid productId);
    }
}
