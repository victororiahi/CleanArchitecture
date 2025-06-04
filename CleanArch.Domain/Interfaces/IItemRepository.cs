using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task AddItemAsync(Item item);

        Task<IEnumerable<Item>> GetAllItemsAsync();

        Task<Item> GetItemByIdAsync(Guid id);

        IQueryable<Item> GetAll();

        Task UpdateItemAsync(Item item);

        Task DeleteItemAsync(Guid id);
    }
}
