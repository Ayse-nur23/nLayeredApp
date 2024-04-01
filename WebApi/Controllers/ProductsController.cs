using Business.Abstract;
using Business.Dtos.Products;
using Core.DataAccess.Paging;
using Entities.Concrete;
//using Core.Validation.ActionFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // [Authorize(Roles = "admin")]
        // [ServiceFilter(typeof(ValidationFilter))]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateProductRequest createProductRequest)
        {
            await _productService.Add(createProductRequest);
            return Ok(createProductRequest);
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteProductRequest deleteProductRequest)
        {
            await _productService.Delete(deleteProductRequest);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateProductRequest updateProductRequest)
        {
            await _productService.Update(updateProductRequest);
            return Ok();
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _productService.GetListAsync(pageRequest);
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await _productService.GetAsync(id);
            return Ok(result);
        }

        [HttpPost("transaction")]
        public IActionResult TransactionTest()
        {
            var result = _productService.TransactionalOperation();

            return Ok(result);
        }
    }

}
