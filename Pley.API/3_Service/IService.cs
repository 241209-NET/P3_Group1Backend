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
    ReviewOutDTO? GetReviewById(int id);
    IEnumerable<ReviewOutDTO> GetAllReviews();
    ReviewOutDTO CreateNewReview(ReviewInDTO newReview);
}

public interface ICustomerService
{
    CustomerOutDTO? GetCustomerById(int id);
    IEnumerable<CustomerOutDTO> GetAllCustomers();
    CustomerOutDTO CreateNewCustomer(Customer customer);
}