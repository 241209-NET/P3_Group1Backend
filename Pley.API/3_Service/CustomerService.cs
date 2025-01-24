
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

    public CustomerOutDTO CreateNewCustomer(CustomerInDTO newCustomerInDTO)
    {
        var customer = _utility.CustomerInDTOToCustomer(newCustomerInDTO);

        var newCustomer = _customerRepo.CreateNewCustomer(customer);        
        return _utility.CustomerToCustomerOutDTO(newCustomer);
    }

    public IEnumerable<CustomerOutDTO> GetAllCustomers()
    {
        var customers = _customerRepo.GetAllCustomers();

        if (customers == null)
            throw null!;

        List<CustomerOutDTO> list = new List<CustomerOutDTO>();
        foreach(var customer in customers)
        {
            var dto = _utility.CustomerToCustomerOutDTO(customer);
            list.Add(dto);
        }

        return list;
    }

    public CustomerOutDTO GetCustomerById(int id)
    {
        var customer = _customerRepo.GetCustomerById(id);
        var dto = _utility.CustomerToCustomerOutDTO(customer!);
        return dto;
    }
}