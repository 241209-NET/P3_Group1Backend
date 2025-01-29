using Pley.API.Model;
using Pley.API.Data;

namespace Pley.API.Repo;

public class CustomerRepo : ICustomerRepo
{
    private readonly PleyContext _pleyContext;
    public CustomerRepo(PleyContext pleyContext) => _pleyContext = pleyContext;

    public IEnumerable<Customer> GetAllCustomers()
    {
        return _pleyContext.Customers.ToList();
    }

    public Customer? GetCustomerById(int id)
    {
        return _pleyContext.Customers.Find(id);
    }

    public Customer? GetCustomerByName(string name)
    {
        return _pleyContext.Customers.Where(c => c.Name == name).First();
    }
}