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

    public Store LoginInDTOToStore(LoginInDTO loginInDTO)
    {
        return new Store
        {
            Username = loginInDTO.Username,
            Password = loginInDTO.Password
        };
    }

    public Store SignUpInDTOToStore(SignUpInDTO signUpInDTO)
    {
        return new Store
        {
            Name = signUpInDTO.Name,
            Username = signUpInDTO.Username,
            Password = signUpInDTO.Password
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
        foreach (var review in list)
        {
            sum += review.Rating;
        }
        if (list.Count > 0) return Math.Round(sum / list.Count, 1);

        return 0;
    }
}