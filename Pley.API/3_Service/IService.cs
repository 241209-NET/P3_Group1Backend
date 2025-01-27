using Pley.API.Model;
using Pley.API.DTO;

namespace Pley.API.Service;

public interface IStoreService
{
    Store CreateNewStore(SignUpInDTO signUpInDTO); 
    Store? Login(string username, string password);
    // edit login info
    Store? GetStoreById(int id); 
    Store? DeleteStoreById(int id);
    // edit store by id
    IEnumerable<Store> GetAllStores();
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