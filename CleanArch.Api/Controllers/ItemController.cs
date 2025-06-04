using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Application.Interfaces;
using CleanArch.Application.DTOs;


namespace CleanArch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }


        [HttpGet("Get-All-Items")]
        public async Task<IActionResult> GetAllItems()
        {
            var items = await _itemService.GetAllItems();
            return Ok(items);
        }


        [HttpPost("Add-Item")]
        public async Task<IActionResult> AddItemAsync(Guid categoryId, [FromBody] ItemDTO itemDTO)
        {
            await _itemService.AddItemAsync(itemDTO, categoryId);
            return Ok("Item was added successfully");
        }


        [HttpGet("Get-Item-by-Id/{id}")]
        public async Task<IActionResult> GetItemByIdAsync(Guid id)
        {
            var Itemdto = await _itemService.GetItemByIdAsync(id);
            return Ok(Itemdto);
        }


        [HttpPut("Update-Item/{Id}")]
        public async Task<IActionResult> UpdateItemAsync(Guid Id, [FromBody] ItemDTO itemDTO)
        {
            await _itemService.UpdateItemAsync(Id, itemDTO);
            return Ok("Item was updated successfully");
        }


        [HttpDelete("Delete-Item/{id}")]
        public async Task<IActionResult> DeleteItemAsync(Guid id)
        {
            await _itemService.DeleteItemAsync(id);
            return Ok("Item was deleted successfully");
        }

    }

}
