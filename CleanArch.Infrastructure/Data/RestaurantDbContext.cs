using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Data
{
    public class RestaurantDbContext : IdentityDbContext<User, Role, Guid>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {
              
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
