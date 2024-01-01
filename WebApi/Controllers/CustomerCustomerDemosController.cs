using Business.Abstract;
using Business.Dtos.Requests;
using Core.DataAccess.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCustomerDemosController : ControllerBase
    {
        ICustomerCustomerDemoService _customerCustomerDemoService;

        public CustomerCustomerDemosController(ICustomerCustomerDemoService customerCustomerDemoService)
        {
            _customerCustomerDemoService = customerCustomerDemoService;
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateCustomerCustomerDemoRequest createCustomerCustomerDemoRequest)
        {
            await _customerCustomerDemoService.Add(createCustomerCustomerDemoRequest);
            return Ok();
        }


        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCustomerDemoRequest deleteCustomerCustomerDemoRequest)
        {
            await _customerCustomerDemoService.Delete(deleteCustomerCustomerDemoRequest);
            return Ok();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCustomerCustomerDemoRequest updateCustomerCustomerDemo)
        {
            await _customerCustomerDemoService.Update(updateCustomerCustomerDemo);
            return Ok();
        }

        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            var result = await _customerCustomerDemoService.GetListAsync(pageRequest);
            return Ok(result);
        }
    }
}
