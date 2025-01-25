
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

        if (customers == null)
        {
            return null!;
        }

        return customers;
    }

    public Customer? GetCustomerById(int id)
    {
        var customer = _customerRepo.GetCustomerById(id);
        if (customer == null)
        {
            return null;
        }

        List<Review> list = _reviewRepo.GetAllReviews().Where(r => r.CustomerId == id).ToList();

        customer.AvgRating = _utility.GetAvgRating(list);
        return customer;
    }

    public Customer? GetCustomerByName(string name)
    {
        var customer = _customerRepo.GetCustomerByName(name);
        if (customer == null)
        {
            return null;
        }
        return customer;
    }
}