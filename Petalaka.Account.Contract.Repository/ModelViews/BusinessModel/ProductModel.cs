namespace Petalaka.Account.Contract.Repository.ModelViews.BusinessModel;

public class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int UnitInStock { get; set; }
    public decimal UnitPrice { get; set; }
    public CategoryModel Category { get; set; }
}