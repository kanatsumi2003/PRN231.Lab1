using AutoMapper;
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

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PaginationResponse<ProductModel>> GetProducts(PaginationRequest request)
    {
        var products = await _unitOfWork.ProductRepository.GetPagination(request.PageNumber, request.PageSize);
        return _mapper.Map<PaginationResponse<ProductModel>>(products);
    }
    
    public async Task<GetProductResponse> GetProductById(int id)
    {
        var product = await _unitOfWork.ProductRepository.FindUndeletedAsync(p => p.ProductId == id);
        if (product == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Product not found");
        }
        return _mapper.Map<GetProductResponse>(product);
    }
    
    public async Task<CreateProductResponse> CreateProduct(CreateProductRequest product)
    {
        var category = await _unitOfWork.CategoryRepository.FindUndeletedAsync(c => c.CategoryId == product.CategoryId);
        if (category == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }
        var newProduct = _mapper.Map<Product>(product);
        await _unitOfWork.ProductRepository.InsertAsync(newProduct);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<CreateProductResponse>(newProduct);
    }
    
    public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request)
    {

        // Retrieve the existing product
        var productExist = await _unitOfWork.ProductRepository
            .FindUndeletedAsync(p => p.ProductId == request.ProductId);

        if (productExist == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Product not found");
        }

        // Retrieve and validate the category
        var category = await _unitOfWork.CategoryRepository
            .FindUndeletedAsync(c => c.CategoryId == request.CategoryId);

        if (category == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Category not found");
        }

        // Update the existing product's properties
        _mapper.Map(request, productExist);

        // Optional: Ensure the product's Category is correctly updated
        productExist.Category = category;
        _unitOfWork.ProductRepository.Update(productExist);
        // Save changes to the database
        await _unitOfWork.SaveChangesAsync();

        // Return the updated product response
        return _mapper.Map<UpdateProductResponse>(productExist);
    }
    public async Task<DeleteProductResponse> DeleteProduct(int id)
    {
        var product = await _unitOfWork.ProductRepository.FindUndeletedAsync(x => x.ProductId == id);
        if (product == null)
        {
            throw new CoreException(StatusCodes.Status400BadRequest, "Product not found");
        }
        _unitOfWork.ProductRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<DeleteProductResponse>(product);
    }
}