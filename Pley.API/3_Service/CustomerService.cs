
using Microsoft.AspNetCore.Http.HttpResults;
using Pley.API.DTO;
using Pley.API.Model;
using Pley.API.Repo;
using Pley.API.Util;

namespace Pley.API.Service;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepo _customerRepo;
    private readonly Utility _utility;
    public CustomerService(ICustomerRepo customerRepo, Utility utility)
    {
        _customerRepo = customerRepo;
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