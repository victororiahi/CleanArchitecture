using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        [HttpGet("Get-all-categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }


        [HttpPost("Add-category")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDTO categoryDTO)
        {

            await _categoryService.AddCategoryAsync(categoryDTO);
            return Ok("The category was added successfully!");

        }

        [HttpGet("Get-category-by-Id/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var Categorydto = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(Categorydto);
        }


        [HttpPut("Update-category/{Id}")]
        public async Task<IActionResult> UpdateCategoryAsync(Guid Id, [FromBody] CategoryDTO categoryDTO)
        {
            await _categoryService.UpdateCategoryAsync(Id, categoryDTO);
            return Ok("The category was updated successfully!");
        }


        [HttpDelete("Delete-category/{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return Ok("The category was deleted successfully!");
        }

    }
}
