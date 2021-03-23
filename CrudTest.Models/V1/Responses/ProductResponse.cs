using System;
using System.Collections.Generic;
using System.Text;

namespace CrudTest.Models.V1.Responses
{
    public class ProductResponse
    {
        public List<Product> Products { get; set; }
        public ProductResponse()
        {
            Products = new List<Product>();
        }
        public class Product 
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public float Cost { get; set; }
            public float Price { get; set; }
            public string ImageUrl { get; set; }
        }

    }
}
