using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_ApiWebUIProject.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult CategoryList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory()
        {
            return RedirectToAction("CategoryList");
        }
    }
}
