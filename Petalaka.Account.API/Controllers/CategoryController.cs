using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    [HttpGet]
    [Route("v1/categories")]
    public async Task<ActionResult<PaginationResponse<Category>>> GetCategories([FromQuery] PaginationRequest request)
    {
        var categories = await _categoryService.GetCategories(request);
        return Ok(new PaginationResponse<Category>(StatusCodes.Status200OK, "Categories retrieved successfully", categories));
    }
    
    [HttpGet]
    [Route("v1/category/{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryById(id);
        return Ok(category);
    }
    
    [HttpPost]
    [Route("v1/category")]
    public async Task<ActionResult<BaseResponse<CreateCategoryResponse>>> CreateCategory(CreateCategoryRequest category)
    {
        var createdCategory = await _categoryService.CreateCategory(category);
        return Created(String.Empty, new BaseResponse(StatusCodes.Status201Created, "Category created successfully", createdCategory));
    }
    
    [HttpPut]
    [Route("v1/category")]
    public async Task<ActionResult<BaseResponse<UpdateCategoryResponse>>> UpdateCategory(UpdateCategoryRequest category)
    {
        var updatedCategory = await _categoryService.UpdateCategory(category);
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Category updated successfully", updatedCategory));
    }
    
    [HttpDelete]
    [Route("v1/category/{id}")]
    public async Task<ActionResult<BaseResponse<DeleteCategoryResponse>>> DeleteCategory(int id)
    {
        var deleteCategory = await _categoryService.DeleteCategory(id);
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Category deleted successfully", deleteCategory));
    }
}