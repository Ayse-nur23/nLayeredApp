using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class Product : Entity<Guid>
{
    public Guid CategoryId { get; set; }
    public string ProductName { get; set; } 
    public decimal UnitPrice { get; set; } 
    public short UnitsInStock { get; set; }
    public string QuantityPerUnit { get; set; }
    public Guid FileUploadId { get; set; }
    public virtual FileUpload FileUpload { get; set; }
    public Category Category { get; set; }

    //public Product()
    //{
        
    //}
    //public Product(int id, int categoryId, string productName, decimal unitPrice, short unitsInStock, string quantityPerUnit): this()
    //{
    //    Id = id;
    //    CategoryId = categoryId;
    //    ProductName = productName;
    //    UnitPrice = unitPrice;
    //    UnitsInStock = unitsInStock;
    //    QuantityPerUnit = quantityPerUnit;
    //}
}
