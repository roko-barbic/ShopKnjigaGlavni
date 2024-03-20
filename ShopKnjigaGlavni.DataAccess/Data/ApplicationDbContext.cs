using Microsoft.EntityFrameworkCore;
using ShopKnjigaGlavni.Models.Models;
using System.ComponentModel;

namespace ShopKnjigaGlavni.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
         
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasData(
            new
            {
                Id = 1,
                Title = "Petar Pan",
                Author = "Nez ko je",
                Description = "Djecak koji zivi u snu",
                ISBN = "aSDNJfjabaA",
                ListPrice = 15.0,
                Price = 13.0,
                Price50 = 10.0,
                Price100 = 7.0,
                CategoryId = 2,
                ImageUrl = "https://th.bing.com/th/id/OIP.c2QKb4Z32HkDaKSSdojQHwHaF7?w=229&h=183&c=7&r=0&o=5&pid=1.7",
            }, new
            {
                Id = 2,
                Title = "Headshot",
                Author = "Rita Bullwinkel",
                Description = "Zivot je borba",
                ISBN = "agDNJfjfassaA",
                ListPrice = 18.0,
                Price = 13.0,
                Price50 = 13.0,
                Price100 = 10.0,
                CategoryId = 2,
                ImageUrl = "https://th.bing.com/th/id/OIP.c2QKb4Z32HkDaKSSdojQHwHaF7?w=229&h=183&c=7&r=0&o=5&pid=1.7",
            }, new
            {
                Id = 3,
                Title = "Nightwatching",
                Author = "Tracy Sierra",
                Description = "Gledanje mraka",
                ISBN = "giasdbfajfaS",
                ListPrice = 20.0,
                Price = 13.0,
                Price50 = 10.0,
                Price100 = 7.0,
                CategoryId = 2,
                ImageUrl = "https://th.bing.com/th/id/OIP.c2QKb4Z32HkDaKSSdojQHwHaF7?w=229&h=183&c=7&r=0&o=5&pid=1.7",
            });
        modelBuilder.Entity<Category>().HasData(
            new
            {
                Id = 1,
                Name = "SciFi",
                DisplayOrder = 1
            },
            new 
            {
                Id = 2,
                Name = "Novel",
                DisplayOrder = 2
            },
            new 
            {
                Id = 3,
                Name = "Thriller",
                DisplayOrder = 3
            }
            );
    }
}
