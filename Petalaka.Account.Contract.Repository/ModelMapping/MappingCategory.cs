using AutoMapper;
using Petalaka.Account.Contract.Repository.Entities;
using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;
using Petalaka.Account.Contract.Repository.ModelViews.RequestModels;
using Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

namespace Petalaka.Account.Contract.Repository.ModelMapping;

public class MappingCategory : Profile
{
    public MappingCategory()
    {
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();

        CreateMap<CreateCategoryResponse, Category>().ReverseMap();
        CreateMap<UpdateCategoryResponse, Category>().ReverseMap();
        CreateMap<DeleteCategoryResponse, Category>().ReverseMap();
        
        CreateMap<CategoryModel, Category>().ReverseMap();
    }
}