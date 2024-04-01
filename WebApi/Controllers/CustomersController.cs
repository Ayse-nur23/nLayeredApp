using Business.Abstract;
using Business.Dtos.Customers;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            await _customerService.Add(createCustomerRequest);
            return Ok();
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerRequest deleteCustomerRequest)
        {
            await _customerService.Delete(deleteCustomerRequest);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            await _customerService.Update(updateCustomerRequest);
            return Ok();
        }
        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _customerService.GetListAsync(pageRequest);
            return Ok(result);
        }
    }
}
