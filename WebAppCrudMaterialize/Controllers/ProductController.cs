using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudTest.DataAccess.V1;
using CrudTest.Framework.Logs;
using CrudTest.Models.V1.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAppCrudMaterialize.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IProduct _product;

        public ProductController(IConfiguration configuration, ILogger logger,IProduct product)
        {
            _configuration = configuration;
            _logger = logger;
            _product = product;
        }

        public IActionResult Index()
        {
            var products= _product.GetAllProducts();
            var productResponse = (ProductResponse)products.Data;            
            //var all = productResponse.Products;
            List<ProductResponse.Product> allProducts = productResponse.Products;
            return View(allProducts);            
        }
    }
}
