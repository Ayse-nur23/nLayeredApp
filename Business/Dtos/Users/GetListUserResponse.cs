using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Users
{
    public class GetListUserResponse
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string Lastname { get; set; }
        public required string PhoneNumber { get; set; }
        public required string? Email { get; set; }
    }
}
