using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.Interface;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
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
    
    public async Task<PaginationResponse<CategoryModel>> GetCategories(PaginationRequest request)
    {
        var categories = await _unitOfWork.CategoryRepository.GetPagination(request.PageNumber, request.PageSize);
        return _mapper.Map<PaginationResponse<CategoryModel>>(categories);
    }
    
    public async Task<CategoryModel> GetCategoryById(int id)
    {
        Category category = await _unitOfWork.CategoryRepository.FindUndeletedAsync(p => p.CategoryId == id);
        if (category == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        return _mapper.Map<CategoryModel>(category);
    }
    
    public async Task<CreateCategoryResponse> CreateCategory(CreateCategoryRequest category)
    {
        var newCategory = _mapper.Map<Category>(category);
        await _unitOfWork.CategoryRepository.InsertAsync(newCategory);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CreateCategoryResponse>(newCategory);
    }
    
    public async Task<UpdateCategoryResponse> UpdateCategory(UpdateCategoryRequest request)
    {
        /*var updatedCategory = _mapper.Map<Category>(category);
        Category existingCategory = await _unitOfWork.CategoryRepository.FindUndeletedAsync(x => x.CategoryId == updatedCategory.CategoryId);
        if (existingCategory == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        _unitOfWork.CategoryRepository.Update(_mapper.Map<Category>(updatedCategory));
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<UpdateCategoryResponse>(updatedCategory);*/
        // Retrieve the existing product
        var categoryExist = await _unitOfWork.CategoryRepository
            .FindUndeletedAsync(p => p.CategoryId == request.CategoryId);

        if (categoryExist == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Product not found");
        }

        // Update the existing product's properties
        _mapper.Map(request, categoryExist);

        // Optional: Ensure the product's Category is correctly updated
        _unitOfWork.CategoryRepository.Update(categoryExist);
        // Save changes to the database
        await _unitOfWork.SaveChangesAsync();

        // Return the updated product response
        return _mapper.Map<UpdateCategoryResponse>(categoryExist);
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