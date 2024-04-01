namespace Business.Dtos.Users;

public class UpdateUserRequest
{
    public Guid Id { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }

}

