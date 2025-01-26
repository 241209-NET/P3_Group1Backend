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