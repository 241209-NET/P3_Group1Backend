using System.Collections.Generic;
using Pley.API.DTO;
using Pley.API.Model;
using Pley.API.Service;

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

    // public Review ReviewInDTOToReview(ReviewInDTO reviewInDTO)
    // {
    //     return new Review
    //     {
    //         Comment = reviewInDTO.Comment,
    //         CustomerId = reviewInDTO.CustomerId,
    //         StoreId = reviewInDTO.StoreId,
    //         Rating = reviewInDTO.Rating
    //     };
    // }

    public Review ReviewInDTOToReview(ReviewInDTO reviewInDTO, int customerId, int storeId)
    {
        return new Review
        {
            Rating = reviewInDTO.Rating,
            Comment = reviewInDTO.Comment,
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
}