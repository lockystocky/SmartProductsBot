using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using NewProductsWebApi.Classes;
using NewProductsWebApi.Interfaces;
using NewProductsWebApi.Models;

namespace NewProductsWebApi.Controllers
{
    [RoutePrefix("api/cart")]
    public class ShoppingCartsController : ApiController
    {
        private readonly IRepository repository;// = new ProductsRepository();

        public ShoppingCartsController(IRepository repo)
        {
            repository = repo;
        }

        [Route("getallcustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return repository.GetAllCustomers();
        }

        [Route("GetProducts/{customerId}")]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetProductsFromCart(string customerId)
        {
            var products = repository.GetProductsFromCart(customerId);

            if (products == null)
                return NotFound();

            return Ok(products);
        }

        [Route("BuyProducts/{customerId}")]
        public string PostProductFromCart(string customerId)
        {
            var successfulBuy = repository.BuyProductsFromCart(customerId);

            if (!successfulBuy)
                return "You cannot buy products.";

            return "You successfully bought products!";
        }

        [Route("AddProduct/{customerId}/{productId}")]
        [ResponseType(typeof(ShoppingCart))]
        public IHttpActionResult PostProductToCart(string customerId, Guid productId)
        {
            var shoppingCart = repository.AddProductToCart(customerId, productId);

            if (shoppingCart == null)
                return NotFound();

            return Ok();
        }

        
    }
}