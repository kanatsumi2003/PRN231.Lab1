using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;

namespace Petalaka.Account.Contract.Service.Interface;

public interface IProductService
{
    Task<PaginationResponse<ProductModel>> GetProducts(PaginationRequest request);
    Task<GetProductResponse> GetProductById(int id);
    Task<CreateProductResponse> CreateProduct(CreateProductRequest product);
    Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest product);
    Task<DeleteProductResponse> DeleteProduct(int id);
    
}