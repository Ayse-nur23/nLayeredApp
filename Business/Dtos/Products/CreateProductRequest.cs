using Business.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Products;
public class CreateProductRequest
{
    public Guid CategoryId { get; set; }
    public  string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public short UnitsInStock { get; set; }
    public string QuantityPerUnit { get; set; }
    public Guid FileUploadId { get; set; }
}
