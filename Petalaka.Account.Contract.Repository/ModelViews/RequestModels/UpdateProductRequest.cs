namespace Petalaka.Account.Contract.Repository.ModelViews.RequestModels;

public class UpdateProductRequest
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int CategoryId { get; set; }
    public int UnitsInStock { get; set; }
    public decimal UnitPrice { get; set; }
}