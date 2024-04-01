using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class User : Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? IdentityNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime? BirthDate { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; } 
    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; }
}
