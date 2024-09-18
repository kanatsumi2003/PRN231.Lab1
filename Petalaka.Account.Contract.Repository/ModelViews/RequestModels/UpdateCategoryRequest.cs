namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class UpdateCategoryRequest
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}