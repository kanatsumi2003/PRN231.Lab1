using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;

namespace Petalaka.Account.Contract.Service.Interface;

public interface ICategoryService
{
    Task<PaginationResponse<CategoryModel>> GetCategories(PaginationRequest request);
    Task<CategoryModel> GetCategoryById(int id);
    Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest category);
    Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest category);
    Task<DeleteCategoryResponse> DeleteCategory(int id);
    
}