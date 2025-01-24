using Microsoft.AspNetCore.Mvc;
using Pley.API.Service;

namespace Pley.API.Controller;

public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService) => _customerService = customerService;

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var reviewList = _customerService.GetAllCustomers();
        if(reviewList is null || !reviewList.Any()) 
            return NotFound("No customers found"); 

        return Ok(reviewList);
    }

    [HttpGet("{customerId}")]
    public IActionResult GetCustomerById(int customerId)
    {
        try
        {
            var review = _customerService.GetCustomerById(customerId);
            if(review is null)
            {
                return NotFound("No customer found for Id = " + customerId);
            }
            return Ok(review);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}