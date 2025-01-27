using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Model;

namespace Pley.API.Service;

public class StoreService : IStoreService
{

    //     Store CreateNewStore(StoreInDTO newStore); 
    // IEnumerable<Store> GetAllStores();
    // Store? GetStoreById(int id); 
    // Store? DeleteStoreById(int id);
    // Store? Login(string userName, string Password);
    // Store? GetStoreByUsername(string username);

    private readonly IStoreRepo _storeRepo;
    private readonly Utility _utility;
    public StoreService(IStoreRepo storeRepo, Utility utility)
    {
        _storeRepo = storeRepo;
        _utility = utility;
    }

    public Store Login(string Username, string Password)
    {
        throw new NotImplementedException();
    }

    public Store CreateNewStore(StoreInDTO newStoreDTO)
    {
        return _utility.StoreInDTOToStore(newStoreDTO);   
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _storeRepo.GetAllStores();
    }

    public Store? GetStoreById(int id)
    {
        return _storeRepo.GetStoreById(id);
    }

    public Store? DeleteStoreById(int id)
    {
        return _storeRepo.GetStoreById(id);
    }

    public Store GetStoreByUsername(string Username)
    {
        throw new NotImplementedException();
    }

}