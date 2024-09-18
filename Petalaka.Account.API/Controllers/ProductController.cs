using Microsoft.AspNetCore.Mvc;
using Petalaka.Account.API.Base;
using Petalaka.Account.Contract.Repository.Base;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;
using Petalaka.Account.Contract.Service.Interface;

namespace Petalaka.Account.API.Controllers;

public class ProductController : BaseController
{
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    [Route("api/v1/products")]
    public async Task<ActionResult<PaginationResponse<ProductModel>>> GetProducts([FromQuery] PaginationRequest request)
    {
        var products = await _productService.GetProducts(request);
        return Ok(new PaginationResponse<ProductModel>(StatusCodes.Status200OK, "Get products successfully", products));
    }
    
    [HttpGet]
    [Route("api/v1/product/{id}")]
    public async Task<ActionResult<BaseResponse<GetProductResponse>>> GetProductById(int id)
    {
        var product = await _productService.GetProductById(id);
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Get product successfully" ,product));
    }
    
    [HttpPost]
    [Route("api/v1/product")]
    public async Task<ActionResult<BaseResponse<CreateProductResponse>>> CreateProduct(CreateProductRequest product)
    {
        var createdProduct = await _productService.CreateProduct(product);
        return CreatedAtAction("", new BaseResponse(StatusCodes.Status201Created, "Product created successfully", createdProduct));
    }
    
    [HttpPut]
    [Route("api/v1/product")]
    public async Task<ActionResult<BaseResponse<UpdateProductResponse>>> UpdateProduct(UpdateProductRequest product)
    {
        var updatedProduct = await _productService.UpdateProduct(product);
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Product updated successfully", updatedProduct));
    }
    
    [HttpDelete]
    [Route("api/v1/product/{id}")]
    public async Task<ActionResult<BaseResponse<DeleteProductResponse>>> DeleteProduct(int id)
    {
        var deleteProduct = await _productService.DeleteProduct(id);
        return Ok(new BaseResponse(StatusCodes.Status200OK, "Product deleted successfully", deleteProduct));
    }
    
}