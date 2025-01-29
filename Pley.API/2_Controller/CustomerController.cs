using Microsoft.AspNetCore.Mvc;
using Pley.API.Service;
using Microsoft.AspNetCore.Authorization;
using Pley.API.DTO;
using System.Security.Claims;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly IReviewService _reviewService;
    public CustomersController(ICustomerService customerService, IReviewService reviewService) 
    {
        _customerService = customerService;
        _reviewService = reviewService;
    }

    [Authorize]
    [HttpDelete("{customerId}/reviews/{reviewId}")]
    public IActionResult DeleteReviewById(int reviewId)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var deleteReview = _reviewService.DeleteReviewById(reviewId);

            if (deleteReview == null)
            {
                return NotFound($"No review found with Id {reviewId}");
            }

            if (deleteReview.StoreId != int.Parse(userID!))
                return Unauthorized("You do not have permission to perform this action.");

            return Ok($"Successfully deleted review {reviewId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPatch("{customerId}/reviews/{reviewId}")]
    public IActionResult EditReviewById(int reviewId, [FromBody] ReviewInDTO reviewIn)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingReview = _reviewService.GetReviewById(reviewId);            // get it first to extract storeId and customerId
            if (existingReview == null)
            {
                return NotFound($"No review found with Id {reviewId}");
            }

            if (existingReview.StoreId != int.Parse(userID!))
            {
                return Unauthorized("You do not have permission to perform this action.");
            }

            var editReview = _reviewService.EditReviewById(existingReview, reviewIn);

            return Ok(editReview);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPost("{customerId}/reviews")]
    public IActionResult CreateNewReview(int customerId, [FromBody] ReviewInDTO reviewIn)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var review = _reviewService.CreateNewReview(int.Parse(userID), customerId, reviewIn);
            
            return Ok(review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
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

    [HttpGet]
    public IActionResult GetAllCustomers()
    {
        var customerList = _customerService.GetAllCustomers();
        if (customerList is null || !customerList.Any())
        {
            return NotFound("No customers found");
        }

        return Ok(customerList);
    }
}