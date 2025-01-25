using Microsoft.AspNetCore.Mvc;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomersController(ICustomerService customerService) => _customerService = customerService;

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var customerList = _customerService.GetAllCustomers();
        if(customerList is null || !customerList.Any())
        {
            return NotFound("No customers found");
        }
            
        return Ok(customerList);
    }

    [HttpGet("name/{name}")]
    public IActionResult GetCustomerByName(string name)
    {
        try
        {
            var findCustomer = _customerService.GetCustomerByName(name);
            return Ok(findCustomer);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{customerId}")]
    public IActionResult GetCustomerById(int customerId)
    {
        try
        {
            var customer = _customerService.GetCustomerById(customerId);
            if(customer is null)
            {
                return NotFound($"No customer found with Id {customerId}");
            }
            
            return Ok(customer);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}