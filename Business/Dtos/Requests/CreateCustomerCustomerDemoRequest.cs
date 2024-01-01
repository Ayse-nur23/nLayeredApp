using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Requests;

public class CreateCustomerCustomerDemoRequest
{
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string CustomerDesc { get; set; }
}
