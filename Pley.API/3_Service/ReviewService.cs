using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;

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
    public ReviewOutDTO CreateNewReview(ReviewInDTO newReviewInDTO)
    {
        var review = _utility.ReviewInDTOToReview(newReviewInDTO);
        review.Time = DateTime.UtcNow;
        var newReview = _reviewRepo.CreateNewReview(review);        
        return _utility.ReviewToReviewOutDTO(newReview);
    }

    public IEnumerable<ReviewOutDTO> GetAllReviews()
    {
        var reviews = _reviewRepo.GetAllReviews();

        return reviews.Select(_utility.ReviewToReviewOutDTO);
    }

    public ReviewOutDTO? GetReviewById(int id)
    {
        var review = _reviewRepo.GetReviewById(id);

        if(review is null)
            return null;

        return _utility.ReviewToReviewOutDTO(review);
    }

    public ReviewOutDTO? DeleteReviewById(int id)
    {
        var review = _reviewRepo.GetReviewById(id);

        if (review is not null)
        {
            _reviewRepo.DeleteReviewById(id);
            return _utility.ReviewToReviewOutDTO(review);
        }

        return null;
    }

    // public IEnumerable<ReviewOutDTO> GetReviewsByCustomerId(int customerId)
    // {
    //     var reviews = _reviewRepo.GetReviewsByCustomerId(customerId);

    //     return reviews.Select(_utility.ReviewToReviewOutDTO);
    // }
}