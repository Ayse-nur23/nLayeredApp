namespace Business.Dtos.Products;

public class UpdatedProductResponse
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public required string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
    public required string QuantityPerUnit { get; set; }
    public Guid FileUploadId { get; set; }
}
