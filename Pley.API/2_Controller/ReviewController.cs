using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewController(IReviewService reviewService) => _reviewService = reviewService;

    [HttpGet]
    public IActionResult GetAllReviews()
    {
        var reviewList = _reviewService.GetAllReviews();
        if(reviewList is null || !reviewList.Any()) 
            return NotFound("No reviews found"); //throw new ReviewNotFoundException();

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
                return NotFound("No reviews found for reviewId = " + reviewId);
            }
            return Ok(review);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateNewReview([FromBody] ReviewInDTO newReviewInDTO)
    {
        var review = _reviewService.CreateNewReview(newReviewInDTO);
        if(review is null){
            return BadRequest("Wrong format input for createNewReview");
        }
        return Ok(review);
    }
}