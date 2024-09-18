using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    private readonly PetalakaDbContext _dbContext;
    public CategoryRepository(PetalakaDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}