using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext _context;
        public CategoryRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {

            return await _context.Categories.ToListAsync();
        }

        public IQueryable<Category> GetAll()
        {
            return _context.Categories;
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

    }
}
