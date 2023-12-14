using Business.Abstract;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.DataAccess.Dynamic;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete;

public class ProductManagerSmell
{
    IProductDal _productDal;

    public ProductManagerSmell(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public async Task<CreatedProductResponse> Add(CreateProductRequest createProductRequest)
    {
        Product product = new Product();

        product.ProductName = createProductRequest.ProductName;
        product.UnitPrice = createProductRequest.UnitPrice;
        product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
        product.UnitsInStock = createProductRequest.UnitsInStock;

        Product createdProduct = await _productDal.AddAsync(product);

        CreatedProductResponse createdProductResponse = new CreatedProductResponse();
        createdProductResponse.Id = createdProduct.Id;
        createdProductResponse.ProductName = createdProduct.ProductName;
        createdProductResponse.UnitPrice = createdProduct.UnitPrice;
        createdProductResponse.QuantityPerUnit = createdProduct.QuantityPerUnit;
        createdProductResponse.UnitsInStock = createdProduct.UnitsInStock;

        return createdProductResponse;
    }

    public async Task<CreatedProductResponse> Delete(CreateProductRequest createProductRequest)
    {
        Product product = new Product();
        product.ProductName = createProductRequest.ProductName;
        product.UnitPrice = createProductRequest.UnitPrice;
        product.QuantityPerUnit = createProductRequest.QuantityPerUnit;
        product.UnitsInStock = createProductRequest.UnitsInStock;

        Product deletedProduct = await _productDal.DeleteAsync(product, true);

        CreatedProductResponse deletedProductResponse = new CreatedProductResponse();
        deletedProductResponse.Id = deletedProduct.Id;
        deletedProductResponse.ProductName = deletedProduct.ProductName;
        deletedProductResponse.UnitPrice = deletedProduct.UnitPrice;
        deletedProductResponse.QuantityPerUnit = deletedProduct.QuantityPerUnit;
        deletedProductResponse.UnitsInStock = deletedProduct.UnitsInStock;

        return deletedProductResponse;

    }

    public async Task<GetListProductResponse> GetAsync(int id)
    {
        var product =  await _productDal.GetAsync(p=>p.Id == id);
        GetListProductResponse getListProductResponse = new GetListProductResponse();
        getListProductResponse.Id = product.Id;
        getListProductResponse.ProductName = product.ProductName;
        getListProductResponse.UnitPrice = product.UnitPrice;
        getListProductResponse.UnitsInStock = product.UnitsInStock;
        return  getListProductResponse;
    }

    public async Task<IPaginate<GetListProductResponse>> GetListAsync()
    {
        IPaginate<Product> products = await _productDal.GetListAsync();

        List<GetListProductResponse> listProductResponse = new List<GetListProductResponse>();

        foreach (var product in products.Items)
        {
            GetListProductResponse productResponse = new GetListProductResponse();
            productResponse.Id = product.Id;
            productResponse.ProductName = product.ProductName;
            productResponse.UnitPrice = product.UnitPrice;
            productResponse.QuantityPerUnit = product.QuantityPerUnit;
            productResponse.UnitsInStock = product.UnitsInStock;

            listProductResponse.Add(productResponse);
        }

        Paginate<GetListProductResponse> getListProductRespone = new()
        {
            Index = products.Index,
            Size = products.Size,
            Count = products.Count,
            From = products.From,
            Pages = products.Pages,
            Items = listProductResponse,
        };

        return getListProductRespone;
    }

    public Task<CreatedProductResponse> Update(CreateProductRequest createProductRequest)
    {
        throw new NotImplementedException();
    }
    public async Task AddT(Product product)
    {
        await _productDal.AddAsync(product);
    }
}
