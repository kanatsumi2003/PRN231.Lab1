using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;
using Petalaka.Account.Contract.Service.Interface;
using Petalaka.Account.Core.ExceptionCustom;

namespace Petalaka.Account.Service.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PaginationResponse<Category>> GetCategories(PaginationRequest request)
    {
        return await _unitOfWork.CategoryRepository.GetPagination(request.PageNumber, request.PageSize);
    }
    
    public async Task<Category> GetCategoryById(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.FindUndeletedAsync(p => p.CategoryId == id);
        if (category == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        return category;
    }
    
    public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest category)
    {
        var newCategory = _mapper.Map<Category>(category);
        await _unitOfWork.CategoryRepository.InsertAsync(newCategory);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CreateCategoryResponse>(newCategory);
    }
    
    public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest category)
    {
        var updatedCategory = _mapper.Map<Category>(category);
        Category existingCategory = await _unitOfWork.CategoryRepository.FindUndeletedAsync(x => x.CategoryId == updatedCategory.CategoryId);
        if (existingCategory == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(updatedCategory));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UpdateCategoryResponse>(updatedCategory);
    }
    
    public async Task<DeleteCategoryResponse> DeleteCategory(int id)
    {
        var existingCategory = await _unitOfWork.CategoryRepository.FindUndeletedAsync(x => x.CategoryId == id);
        if (existingCategory == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        _unitOfWork.CategoryRepository.Delete(existingCategory);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<DeleteCategoryResponse>(existingCategory);
    }

    
}