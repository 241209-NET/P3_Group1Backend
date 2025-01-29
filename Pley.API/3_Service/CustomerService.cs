
using Microsoft.AspNetCore.Http.HttpResults;
using Pley.API.DTO;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Util;

namespace Pley.API.Service;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _customerRepo;
    private readonly IReviewRepo _reviewRepo;
    private readonly Utility _utility;
    public CustomerService(ICustomerRepo customerRepo, IReviewRepo reviewRepo, Utility utility)
    {
        _customerRepo = customerRepo;
        _reviewRepo = reviewRepo;
        _utility = utility;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customers = _customerRepo.GetAllCustomers();

        if (customers.Count() == 0)
        {
            return null!;
        }
        
        foreach (var c in customers)
        {
            AvgRatingHelper(c);
        }

        return customers;
    }

    public Customer? GetCustomerById(int id)
    {
        var customer = _customerRepo.GetCustomerById(id);
        AvgRatingHelper(customer!);
        return customer;
    }

    public Customer? GetCustomerByName(string name)
    {
        var customer = _customerRepo.GetCustomerByName(name);
        AvgRatingHelper(customer!);
        return customer;
    }

    public void AvgRatingHelper(Customer customer)
    {
        List<Review> list = _reviewRepo.GetAllReviews().Where(r => r.CustomerId == customer.Id).ToList();
        customer.AvgRating = _utility.GetAvgRating(list);
    }
}