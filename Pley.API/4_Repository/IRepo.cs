using Pley.API.Model;

namespace Pley.API.Repo;

public interface IStoreRepo
{

    Store CreateNewStore(Store newStore); 

    IEnumerable<Store> GetAllStores(); 

    Store? GetStoreById(int id); 
    
    Store? DeleteStoreById(int id);

    Store? LoginStore(string userName, string Password);

    Store? GetStoreByName(string username);
}

public interface IReviewRepo
{

    Review? GetReviewById(int id);

    IEnumerable<Review> GetAllReviews(); 

    Review? DeleteReviewById(int id);

    Review CreateNewReview(Review newReview);

}