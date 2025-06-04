using CleanArch.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAllCategories();
        public Task AddCategoryAsync(CategoryDTO categoryDTO);

        Task<CategoryDTO> GetCategoryByIdAsync(Guid Id);
        Task UpdateCategoryAsync(Guid Id, CategoryDTO categoryDTO);
        Task DeleteCategoryAsync(Guid Id);
    }
}
