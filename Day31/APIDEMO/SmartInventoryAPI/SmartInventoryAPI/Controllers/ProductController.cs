using Microsoft.AspNetCore.Mvc;
using SmartInventoryAPI_RoutingDemo.Models;

namespace SmartInventoryAPI_RoutingDemo.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // Temporary product list
        static List<Product> products = new List<Product>()
        {
            new Product{ Id=1, Name="Laptop", Price=75000, Quantity=5},
            new Product{ Id=2, Name="Mouse", Price=800, Quantity=20}
        };

        // GET: api/products
        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return Ok(products);
        }

        // GET: api/products/1
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            products.Add(product);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = product.Id },
                product);
        }

        // PUT: api/products/1
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Quantity = updatedProduct.Quantity;

            return NoContent();
        }

        // DELETE: api/products/1
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            products.Remove(product);

            return NoContent();
        }
    }
}