using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) { 
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<Category>> Get() => await _categoryService.GetAsync();

    }
}
