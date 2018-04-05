using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using NewProductsWebApi.Classes;
using NewProductsWebApi.Interfaces;
using Autofac.Integration.WebApi;
using NewProductsWebApi.Controllers;
using System.Web.Http;

namespace NewProductsWebApi.Classes
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProductsController>().InstancePerDependency();
            builder.RegisterType<ShoppingCartsController>().InstancePerDependency();
            builder.RegisterType<ProductsRepository>().As<IRepository>().InstancePerRequest();
            var container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}