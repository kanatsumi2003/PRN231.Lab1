using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;

namespace Petalaka.Account.Repository.Base;

public class PetalakaDbContext : DbContext
{
    public PetalakaDbContext(DbContextOptions<PetalakaDbContext> options) : base(options)
    {
        
    }

    public virtual DbSet<Product> Products => Set<Product>();
    public virtual DbSet<Category> Categories => Set<Category>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}