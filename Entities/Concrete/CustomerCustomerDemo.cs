using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class CustomerCustomerDemo : Entity<string>
    {
        public string CustomerTypeID { get; set; }

        public Customer Customer { get; set; }
        public CustomerDemographic CustomerDemographic { get; set; }
    }
}
