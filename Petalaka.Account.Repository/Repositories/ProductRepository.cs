using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    private readonly PetalakaDbContext _dbContext;
    public ProductRepository(PetalakaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}