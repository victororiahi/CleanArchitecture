using CleanArch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface IItemService
    {
        Task<IEnumerable<ItemDTO>> GetAllItems();

        Task AddItemAsync(ItemDTO itemDTO, Guid categoryId);

        Task<ItemDTO> GetItemByIdAsync(Guid id);

        Task UpdateItemAsync(Guid Id, ItemDTO itemDTO);

        Task DeleteItemAsync(Guid id);



    }

}

