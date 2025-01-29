using Pley.API.Model;
using Pley.API.DTO;

namespace Pley.API.Service;

public interface IStoreService
{
    Store CreateNewStore(SignUpInDTO signUpInDTO);
    object Login(string username, string password);
    Store? UpdateLogin(Store store, EditLoginInDTO loginInDTO);
    Store? GetStoreById(int id); 
    Store? DeleteStoreById(int id);
    Store? UpdateStore(int id, EditStoreDTO editStoreDTO);
    IEnumerable<Store> GetAllStores();

    void Logout(string token);
}

public interface IReviewService
{
    Review? GetReviewById(int id);
    Review? DeleteReviewById(int id);
    Review EditReviewById(Review existingReview, ReviewInDTO reviewIn);     // guaranteedd not null ever because of controller
    IEnumerable<Review> GetAllReviews();
    Review CreateNewReview(int storeId, int customerId, ReviewInDTO newReview);   // *authentication
}

public interface ICustomerService
{
    IEnumerable<Customer> GetAllCustomers();
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByName(string name);
}