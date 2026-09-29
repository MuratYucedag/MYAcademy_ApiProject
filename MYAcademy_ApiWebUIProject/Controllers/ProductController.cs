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


        public async Task<IActionResult> DeleteProduct(int id)
        {
            var client = new HttpClient();

            var responseMessage = await client.DeleteAsync($"https://localhost:7009/api/Products?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return RedirectToAction("ProductList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync($"https://localhost:7009/api/Products/GetProduct?id={id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                var value = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);

                return View(value);
            }

            return RedirectToAction("ProductList");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            var client = new HttpClient();

            var jsonData = JsonConvert.SerializeObject(updateProductDto);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PutAsync("https://localhost:7009/api/Products", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            return View(updateProductDto);
        }

        public async Task<IActionResult> ProductCount()
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync("https://localhost:7009/api/Products/ProductCount");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                int count = JsonConvert.DeserializeObject<int>(jsonData);

                ViewBag.ProductCount = count;
            }

            return View();
        }
    }
}
