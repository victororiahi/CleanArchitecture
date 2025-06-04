using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch.Application.Interfaces;
using CleanArch.Application.DTOs;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using Microsoft.Extensions.Logging;


namespace CleanArch.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<ItemService> _logger;
        public ItemService(IItemRepository itemRepository, ICategoryRepository categoryRepository, ILogger<ItemService> logger)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }


        public async Task AddItemAsync(ItemDTO itemDTO, Guid categoryId)
        {
            try
            {
                //Let us check if this category Id has an associated category 
                //if not lets throw exception 
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    throw new ArgumentException();
                }
                var item = new Item
                {
                    Id = Guid.NewGuid(),
                    Name = itemDTO.Name,
                    ImageUrl = itemDTO.ImageUrl,
                    Description = itemDTO.Description,
                    Price = itemDTO.Price,
                    CategoryId = categoryId
                };

                await _itemRepository.AddItemAsync(item);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: {ex.Message} STACKTRACE: {ex.StackTrace} ");
               
                return;
            }
        }


        public async Task<IEnumerable<ItemDTO>> GetAllItems()
        {
            var items = await _itemRepository.GetAllItemsAsync();
            var ItemsDTOs = items
                .Select(x => new ItemDTO
                {
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                    Price = x.Price
                }).ToList();

            return ItemsDTOs;
        }


        public async Task<ItemDTO> GetItemByIdAsync(Guid id)
        {
            var Item = await _itemRepository.GetItemByIdAsync(id);
            var ItemDTO = new ItemDTO
            {
                Name = Item.Name,
                ImageUrl = Item.ImageUrl,
                Description = Item.Description,
                Price = Item.Price
            };
            return ItemDTO;
        }


        public async Task UpdateItemAsync(Guid Id, ItemDTO itemDTO)
        {
            var existingItem = await _itemRepository.GetItemByIdAsync(Id);

            if (existingItem == null)
            {
                throw new Exception("Item not found");
            }

            existingItem.Name = itemDTO.Name;
            existingItem.ImageUrl = itemDTO.ImageUrl;
            existingItem.Description = itemDTO.Description;
            existingItem.Price = itemDTO.Price;
                
            await _itemRepository.UpdateItemAsync(existingItem);
        }


        public async Task DeleteItemAsync(Guid id)
        {
            await _itemRepository.DeleteItemAsync(id);
        }
    }
}