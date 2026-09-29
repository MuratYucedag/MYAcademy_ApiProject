using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_ApiProject.Context;
using MYAcademy_ApiProject.Entities;

namespace MYAcademy_ApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApiContext _context;
        public ProductsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _context.Products.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok("Ekleme başarılı");
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var value = _context.Products.Find(id);
            _context.Products.Remove(value);
            _context.SaveChanges();
            return Ok("Silme başarılı");
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var value = _context.Products.Find(id);
            return Ok(value);
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
            return Ok("Güncelleme başarılı");
        }

        [HttpGet("ProductCount")]
        public IActionResult ProductCount()
        {
            int count = _context.Products.Count();
            return Ok(count);
        }

        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var values = _context.Products.Include(x => x.Category)
                .Select(x => new
                {
                    x.ProductId,
                    x.ProductName,
                    x.ProductStock,
                    x.ProductPrice,
                    x.IsCritic,
                    CategoryName = x.Category.CategoryName
                })
                .ToList();

            return Ok(values);
        }
    }
}
