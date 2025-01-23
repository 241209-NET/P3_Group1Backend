using Pley.API.Model;
using Pley.API.Data;
//using Microsoft.EntityFrameworkCore;

namespace Pley.API.Repo;

public class ReviewRepo : IReviewRepo
{

    private readonly PleyContext _pleyContext;

    public ReviewRepo(PleyContext pleyContext) => _pleyContext = pleyContext;

    public Review CreateNewReview(Review newReview)
    {
        _pleyContext.Reviews.Add(newReview);
        _pleyContext.SaveChanges();
        return newReview;
    }

    public IEnumerable<Review> GetAllReviews()
    {
        return _pleyContext.Reviews;
    }

    public Review? GetReviewById(int reviewId)
    {
        return _pleyContext.Reviews.Find(reviewId);
    }

    public Review? DeleteReviewById(int id)
    {
        Review? review = GetReviewById(id);
        if(review is null) 
            return null; //throw new ReviewNotFoundException();

        // review is not null, proceed
        _pleyContext.Reviews.Remove(review);
        _pleyContext.SaveChanges();
        return review;
    }

    public IEnumerable<Review> GetReviewsByStoreId(int storeId)
    {
        return _pleyContext.Reviews.Where(r => r.StoreId == storeId); //.Include(u => u.Store).ToList();
    }
}