using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;
using Petalaka.Account.Contract.Repository.Pagination;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class MappingProduct : Profile
{
    public MappingProduct()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();

        CreateMap<CreateProductResponse, Product>().ReverseMap();
        CreateMap<ProductModel, Product>().ReverseMap()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        CreateMap<GetProductResponse, Product>().ReverseMap();
        CreateMap<UpdateProductResponse, Product>().ReverseMap();
        CreateMap<DeleteProductResponse, Product>().ReverseMap();
        CreateMap(typeof(PaginationResponse<>), typeof(PaginationResponse<>));



    }
}