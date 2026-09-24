using Microsoft.AspNetCore.Mvc;
using MYAcademy_ApiWebUIProject.Dtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> CategoryList()
        {
            var client = new HttpClient();
            var responseMessage = await client.GetAsync("https://localhost:7009/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7009/api/Categories", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }
            return View(createCategoryDto);
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            var client = new HttpClient();
            await client.DeleteAsync("https://localhost:7009/api/Categories?id=" + id);
            return RedirectToAction("CategoryList");

        }

        [HttpGet]
        public IActionResult UpdateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = new HttpClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7009/api/Categories", stringContent);
            return RedirectToAction("CategoryList");
        }
    }
}
