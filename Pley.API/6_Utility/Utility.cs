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
            Time = review.LastUpdated,
            Rating = review.Rating
        };
    }

    public Review ReviewInDTOToReview(ReviewInDTO reviewInDTO)
    {
        return new Review
        {
            Comment = reviewInDTO.Comment,
            CustomerId = reviewInDTO.CustomerId,
            StoreId = reviewInDTO.StoreId,
            Rating = reviewInDTO.Rating
        };

    //         public int Id { get; set; }
    // public int StoreId { get; set; }
    // public int CustomerId { get; set; }
    // public required string Comment { get; set; }
    // public DateTime Time { get; set; }
    // public int Rating { get; set; }
    }
}