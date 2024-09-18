using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;

namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

public class DeleteCategoryResponse : CategoryModel
{
    public DateTimeOffset DeletedTime { get; set; }
}