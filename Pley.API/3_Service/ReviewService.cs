using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Model;

namespace Pley.API.Service;

public class ReviewService : IReviewService
{
    private readonly IReviewRepo _reviewRepo;
    private readonly Utility _utility;
    public ReviewService(IReviewRepo reviewRepo, Utility utility)
    {
        _reviewRepo = reviewRepo;
        _utility = utility;
    }

    public Review? GetReviewById(int id)
    {
        var review = _reviewRepo.GetReviewById(id);

        if(review is null)
        {
            return null;
        }

        return review;
    }

    public Review? DeleteReviewById(int id)
    {
        var deletedReview = _reviewRepo.DeleteReviewById(id);
        if (deletedReview == null)
        {
            return null;
        }
        return deletedReview;
    }

    public Review EditReviewById(Review existingReview, ReviewInDTO reviewIn)
    {
        if (reviewIn.Rating != 0)                       // if rating is changed
        {
            existingReview.Rating = reviewIn.Rating;
        }

        if (!string.IsNullOrEmpty(reviewIn.Comment))    // if comment is changed
        {
            existingReview.Comment = reviewIn.Comment;
        }

        existingReview.LastUpdated = DateTime.Now;      // update the timestamp

        return _reviewRepo.UpdateReview(existingReview);
    }

    public IEnumerable<Review> GetAllReviews()
    {
        var reviews = _reviewRepo.GetAllReviews();
        return reviews;
    }

    // public ReviewOutDTO CreateNewReview(ReviewInDTO newReviewInDTO)
    // {
    //     var review = _utility.ReviewInDTOToReview(newReviewInDTO);
    //     review.LastUpdated = DateTime.UtcNow;
    //     var newReview = _reviewRepo.CreateNewReview(review);
    //     return _utility.ReviewToReviewOutDTO(newReview);
    // }
}