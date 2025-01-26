using Pley.API.DTO;
using Pley.API.Model;

namespace Pley.API.Util;

public class Utility
{
    public StoreOutDTO StoreToStoreOutDTO(Store store)
    {
        return new StoreOutDTO
        {
            Id = store.Id,
            Username = store.Username!
        };
    }

    public Store StoreInDTOToStore(StoreInDTO storeInDTO)
    {
        return new Store
        {
            Username = storeInDTO.Username,
            Password = storeInDTO.Password
        };
    }

    public ReviewOutDTO ReviewToReviewOutDTO(Review review)
    {
        return new ReviewOutDTO
        {
            Comment = review.Comment,
            CustomerId = review.CustomerId, 
            StoreId = review.StoreId,
            LastUpdated = review.LastUpdated,
            Rating = review.Rating
        };
    }

    public Review ReviewInDTOToReview(ReviewInDTO reviewInDTO, int customerId, int storeId)
    {
        return new Review
        {
            Rating = reviewInDTO.Rating ?? 0,           // default 0 if null
            Comment = reviewInDTO.Comment ?? "",        // default "" if null (need to be nullable for patch)
            CustomerId = customerId,
            StoreId = storeId,
            LastUpdated = DateTime.Now
        };
    }

    public double GetAvgRating(List<Review> list)
    {
        double sum = 0;
        foreach(var review in list)
        {
            sum += review.Rating;
        }
        if (list.Count > 0) return sum / list.Count;

        return 0;
    }

    public double GetAvgRating(List<Review> list)
    {
        double sum = 0;
        foreach (var review in list)
        {
            sum += review.Rating;
        }
        if (list.Count > 0) return sum / list.Count;

        return 0;
    }
}