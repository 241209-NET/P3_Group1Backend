using Pley.API.Model;

namespace Pley.API.Repo;

public interface IStoreRepo
{
    Store CreateNewStore(Store newStore); 
    Store? Login(string userName, string Password);
    // edit login    // *authentication
    Store? GetStoreById(int id); 
    Store? DeleteStoreById(Store store);
    // edit store    // *authentication
    IEnumerable<Store> GetAllStores(); 
}

public interface IReviewRepo
{
    Review? GetReviewById(int id);
    Review? DeleteReviewById(int id);
    Review UpdateReview(Review existingReview); // guaranteedd not null ever because of controller
    IEnumerable<Review> GetAllReviews(); 
    // Review CreateNewReview(Review newReview);
}

public interface ICustomerRepo
{
    IEnumerable<Customer> GetAllCustomers(); 
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByName(string name);
}