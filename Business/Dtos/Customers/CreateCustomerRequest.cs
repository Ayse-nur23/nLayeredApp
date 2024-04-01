namespace Business.Dtos.Customers;

public class CreateCustomerRequest
{
    public required string CompanyName { get; set; }
    public required string ContactName { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}
