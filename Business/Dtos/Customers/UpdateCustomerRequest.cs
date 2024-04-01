namespace Business.Dtos.Customers;

public class UpdateCustomerRequest
{
    public required Guid Id { get; set; }
    public required string CompanyName { get; set; }
    public required string ContactName { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}