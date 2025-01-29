using Pley.API.Model;
using Pley.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Pley.API.Repo;

public class ReviewRepo : IReviewRepo
{
    private readonly PleyContext _pleyContext;
    public ReviewRepo(PleyContext pleyContext) => _pleyContext = pleyContext;

    public Review? GetReviewById(int reviewId)
    {
        try
        {
            var review = _pleyContext.Reviews.Include(r => r.Store).Include(r => r.Customer).FirstOrDefault(r => r.Id == reviewId);
            return review;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public Review? DeleteReviewById(int id)
    {
        var review = _pleyContext.Reviews.Find(id);
        if (review == null)
        { 
            return null;
        }
        _pleyContext.Reviews.Remove(review);
        _pleyContext.SaveChanges();

        return review;
    }

    public Review UpdateReview(Review existingReview)
    {
        _pleyContext.Reviews.Update(existingReview);
        _pleyContext.SaveChanges();

        return existingReview;
    }

    public IEnumerable<Review> GetAllReviews()
    {
        return _pleyContext.Reviews
            .Include(r => r.Store)
            .Include(r => r.Customer)
            .ToList();
    }





    // public IEnumerable<Review> GetReviewsByStoreId(int storeId)
    // {
    //     return _pleyContext.Reviews.Where(r => r.StoreId == storeId); //.Include(u => u.Store).ToList();
    // }

    public Review CreateNewReview(Review newReview)
    {
        _pleyContext.Reviews.Add(newReview);
        _pleyContext.SaveChanges();
        return newReview;
    }
}