using Microsoft.AspNetCore.Mvc;
using MYAcademy_ApiWebUIProject.Dtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> ProductList()
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync("https://localhost:7009/api/Products");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var client = new HttpClient();

            var jsonData = JsonConvert.SerializeObject(createProductDto);

            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync("https://localhost:7009/api/Products", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View(createProductDto);
        }
    }
}
