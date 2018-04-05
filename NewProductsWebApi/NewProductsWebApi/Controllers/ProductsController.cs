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
using NewProductsWebApi.Models;
using NewProductsWebApi.Interfaces;
using NewProductsWebApi.Classes;

namespace NewProductsWebApi.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly IRepository repository;// = new ProductsRepository();

        public ProductsController(IRepository repo)
        {
            repository = repo;
        }

        [Route("AllProducts")]
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        [Route("AllCategories")]
        public IEnumerable<Category> GetAllCategories()
        {

            return repository.GetAllCategories();
        }

        [Route("ByCategory/{categoryId}")]
        [ResponseType(typeof(IEnumerable<Product>))]
        public IHttpActionResult GetProductsByCategory(Guid categoryId)
        {
            var products = repository.GetProductsByCategory(categoryId);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [Route("Info/{productId}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult GetProductInfo(Guid productId)
        {
            var productInfo = repository.GetProductInfo(productId);

            if (productInfo == null)
            {
                return NotFound();
            }

            return Ok(productInfo);
        }



        
    }
}