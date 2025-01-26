using Microsoft.AspNetCore.Mvc;
using Pley.API.Service;
using Pley.API.DTO;

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

    [HttpDelete("{customerId}/reviews/{reviewId}")]
    public IActionResult DeleteReviewById(int reviewId)
    {
        try
        {
            var deleteReview = _reviewService.DeleteReviewById(reviewId);
            if (deleteReview == null)
            {
                return NotFound($"No review found with Id {reviewId}");
            }
            return Ok($"Successfully deleted review {reviewId}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("{customerId}/reviews/{reviewId}")]
    public IActionResult EditReviewById(int reviewId, [FromBody] ReviewInDTO reviewIn)
    {
        try
        {
            var existingReview = _reviewService.GetReviewById(reviewId);            // get it first to extract storeId and customerId
            if (existingReview == null)
            {
                return NotFound($"No review found with Id {reviewId}");
            }

            var editReview = _reviewService.EditReviewById(existingReview, reviewIn);

            return Ok(editReview);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpPost("{customerId}/reviews")]
    // public IActionResult CreateNewReview([FromBody] ReviewInDTO newReviewInDTO)
    // {
    //     var review = _reviewService.CreateNewReview(newReviewInDTO);
    //     if(review is null){
    //         return BadRequest("improper input.");
    //     }
    //     return Ok(review);
    // }

    // [HttpPost("{customerId}/reviews")]
    // public IActionResult CreateNewReview([FromBody] ReviewInDTO reviewIn)
    // {
    //     try
    //     {
    //         // var customerId = find a way to get customerId 
    //         // var storeId = find a way to get storeId from authentication
    //         var review = _reviewService.CreateNewReview(reviewIn, storeId, customerId);
    //         if (review is null)
    //         {
    //             return BadRequest("Invalid input");
    //         }
    //         return Ok(review);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }

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