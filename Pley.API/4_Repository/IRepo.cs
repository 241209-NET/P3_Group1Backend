using Pley.API.Model;

namespace Pley.API.Repo;

public interface IStoreRepo
{
    Store CreateNewStore(Store newStore); 
    Store? Login(string userName);
    Store? GetStoreById(int id); 
    Store? DeleteStoreById(Store store);
    Store? UpdateStore(Store store);
    IEnumerable<Store> GetAllStores(); 
    void BlacklistToken(string token);
    bool IsTokenBlacklisted(string token);
}

public interface IReviewRepo
{
    Review? GetReviewById(int id);
    Review? DeleteReviewById(int id);
    Review UpdateReview(Review existingReview); // guaranteedd not null ever because of controller
    IEnumerable<Review> GetAllReviews(); 
    Review CreateNewReview(Review newReview);
}

public interface ICustomerRepo
{
    IEnumerable<Customer> GetAllCustomers(); 
    Customer? GetCustomerById(int id);
    Customer? GetCustomerByName(string name);
}