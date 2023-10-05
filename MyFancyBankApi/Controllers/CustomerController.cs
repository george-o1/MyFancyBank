using Microsoft.AspNetCore.Mvc;
using MyFancyBank.BusinessLogic.Interfaces;
using MyFancyBank.BusinessLogic.Models;

namespace MyFancyBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        public CustomerModel Get(Guid id)
        {
            return _customerService.GetCustomerById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerRequestModel model)
        {
            try
            {
                var customer = _customerService.CreateCustomer(model);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] CustomerRequestModel model)
        {
            try
            {
               var customer = _customerService.UpdateCustomer(id, model);

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
