using Core.Entities;

namespace Entities.Concrete
{
    public class CustomerDemographic : Entity<string>
    {
        public CustomerDemographic() { Customers = new HashSet<CustomerCustomerDemo>(); }
        public string CustomerDesc { get; set; }
        public virtual ICollection<CustomerCustomerDemo> Customers { get; set; }
    }
}