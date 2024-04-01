using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class Employee : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid? FileUploadId { get; set; }

    public virtual FileUpload FileUpload { get; set; }
    public virtual User User { get; set; }

}
