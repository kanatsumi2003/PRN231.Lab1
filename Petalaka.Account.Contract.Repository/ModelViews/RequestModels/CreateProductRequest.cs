namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class CreateProductRequest
{
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }
}