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

    [HttpGet("{reviewId}")]
    public IActionResult GetReviewById(int reviewId)
    {
        try
        {
            var review = _reviewService.GetReviewById(reviewId);
            if (review is null)
            {
                return NotFound($"No review found for Id {reviewId}");
            }

            return Ok(review);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

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
}