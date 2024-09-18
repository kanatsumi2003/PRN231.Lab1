using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Repository.Base;

namespace Petalaka.Account.Repository.Repositories;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    private readonly PetalakaDbContext _dbContext;

    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductRepository _productRepository;
    public UnitOfWork(PetalakaDbContext dbContext,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository) : base(dbContext)
    {
        _dbContext = dbContext;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }


    public ICategoryRepository CategoryRepository => _categoryRepository;
    public IProductRepository ProductRepository => _productRepository;
}