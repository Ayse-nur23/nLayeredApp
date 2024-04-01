using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class Customer : Entity<Guid>
{
    public string CompanyName { get; set; }
    public string ContactName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public Customer()
    {
    }
    public Customer(string companyName, string contactName, string city, string country) : this()
    {
        CompanyName = companyName;
        ContactName = contactName;
        City = city;
        Country = country;
    }
}
