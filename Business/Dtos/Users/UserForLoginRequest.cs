namespace Business.Dtos.Users;

public class UserForLoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
