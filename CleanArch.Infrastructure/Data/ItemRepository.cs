using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly RestaurantDbContext _context;
        public ItemRepository (RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public  IQueryable<Item> GetAll()
        {
           return _context.Items;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
           return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(Guid id)
        {
            return await _context.Items.FindAsync(id);
        }

        public async Task UpdateItemAsync(Item item)
        {
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

    }
}
