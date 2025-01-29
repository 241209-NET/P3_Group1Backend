using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Model;

namespace Pley.API.Service;

public class ReviewService : IReviewService
{
    private readonly IReviewRepo _reviewRepo;
    private readonly IStoreRepo _storeRepo;
    private readonly ICustomerRepo _customerRepo;

    private readonly Utility _utility;
    public ReviewService(IReviewRepo reviewRepo, Utility utility, ICustomerRepo customerRepo, IStoreRepo storeRepo)
    {
        _reviewRepo = reviewRepo;
        _storeRepo = storeRepo;
        _customerRepo = customerRepo;
        _utility = utility;
    }

    public Review? GetReviewById(int id)
    {
        return _reviewRepo.GetReviewById(id);
    }

    public Review? DeleteReviewById(int id)
    {
        return _reviewRepo.DeleteReviewById(id);
    }

    public Review EditReviewById(Review existingReview, ReviewInDTO reviewIn)
    {
        if (reviewIn.Rating.HasValue)                       // if rating is changed
        {
            existingReview.Rating = reviewIn.Rating.Value;
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

        foreach (var review in reviews)
        {
            var customer = review.Customer;
            var custReviews = reviews.Where(r => r.CustomerId == customer.Id);

            customer.Reviews = custReviews.ToList();

            customer.AvgRating = _utility.GetAvgRating(customer.Reviews);
            //Console.WriteLine($"Customer reviews: {customer.Reviews.Count} ");
        }

        return reviews;
    }

    public Review CreateNewReview(int storeId, int customerId, ReviewInDTO newReview)
    {

        var store = _storeRepo.GetStoreById(storeId);
        var customer = _customerRepo.GetCustomerById(customerId);
        if(store == null ){
            throw new ArgumentException("Invalid Store.");
        }

        if (customer == null)
            throw new ArgumentException("Invalid Customer.");

        var reviewDTO = _utility.ReviewInDTOToReview(newReview, customerId, storeId);
        var review = _reviewRepo.CreateNewReview(reviewDTO); 
        
        // this SHOULD capture the newly created review as well. 
        var reviews = GetAllReviews().Where(r => r.CustomerId == customer.Id).ToList();
        // if not, reviews.Add(reviews); ????
        review.Customer.AvgRating = _utility.GetAvgRating(reviews);

        return review;
    }

}