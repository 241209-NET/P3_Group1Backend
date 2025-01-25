using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewsController(IReviewService reviewService) => _reviewService = reviewService;

    [HttpGet]
    public IActionResult GetAllReviews()
    {
        var reviewList = _reviewService.GetAllReviews();
        if(reviewList is null || !reviewList.Any()) 
        {
            return NotFound("No reviews found"); 
        }

        return Ok(reviewList);
    }

    [HttpGet("{reviewId}")]
    public IActionResult GetReviewById(int reviewId)
    {
        try
        {
            var review = _reviewService.GetReviewById(reviewId);
            if(review is null)
            {
                return NotFound($"No review found for Id {reviewId}");
            }

            return Ok(review);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // [HttpPost]
    // public IActionResult CreateNewReview([FromBody] ReviewInDTO newReviewInDTO)
    // {
    //     var review = _reviewService.CreateNewReview(newReviewInDTO);
    //     if(review is null){
    //         return BadRequest("improper input.");
    //     }
    //     return Ok(review);
    // }

    [HttpPost]
    public IActionResult CreateNewReview([FromBody] ReviewInDTO reviewIn)
    {
        try
        {
            // var customerId = find a way to get customerId 
            // var storeId = find a way to get storeId from authentication
            var review = _reviewService.CreateNewReview(reviewIn, storeId, customerId);
            if (review is null)
            {
                return BadRequest("Invalid input");
            }
            return Ok(review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{reviewId}")]
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
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("reviewId")]
    public IActionResult EditReviewById(int reviewId, [FromBody] ReviewInDTO reviewIn)
    {
        try 
        {
            var existingReview = _reviewService.GetReviewById(reviewId);
            if (existingReview == null)
            {
                return NotFound($"No review found with Id {reviewId}");
            }

            var editReview = _reviewService.EditReviewById(reviewId, reviewIn);

            return Ok(editReview);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}