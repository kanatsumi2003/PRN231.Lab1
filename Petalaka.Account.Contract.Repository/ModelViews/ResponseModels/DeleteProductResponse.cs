using Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;

namespace Petalaka.Account.Contract.Repository.ModelViews.ResponseModels;

public class DeleteProductResponse : ProductModel
{
    public DateTimeOffset DeleteTime { get; set; }
}