using Pley.API.Model;
using Pley.API.DTO;

namespace Pley.API.Service;

public interface IStoreService
{
    StoreOutDTO CreateNewStore(StoreInDTO newStore); 
    IEnumerable<StoreOutDTO> GetAllStores();
    StoreOutDTO? GetStoreById(int id); 
    StoreOutDTO? DeleteStoreById(int id);
    StoreOutDTO? Login(string userName, string Password);
    StoreOutDTO? GetStoreByUsername(string username);
}

public interface IReviewService
{
    Review? GetReviewById(int id);
    Review? DeleteReviewById(int id);
    Review EditReviewById(Review existingReview, ReviewInDTO reviewIn);     // guaranteedd not null ever because of controller
    IEnumerable<Review> GetAllReviews();
    // Review CreateNewReview(ReviewInDTO newReview);
}

public interface ICustomerService
{
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByName(string name);
}