using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Model;

namespace Pley.API.Service;

public class StoreService : IStoreService
{
    private readonly IStoreRepo _storeRepo;
    private readonly Utility _utility;
    public StoreService(IStoreRepo storeRepo, Utility utility)
    {
        _storeRepo = storeRepo;
        _utility = utility;
    }

    public Store CreateNewStore(SignUpInDTO newStoreDTO)
    {
        var store = _utility.SignUpInDTOToStore(newStoreDTO);
        return _storeRepo.CreateNewStore(store);
    }

    // *authentication
    public Store Login(string username, string password) 
    {
        throw new NotImplementedException();        
    }

    public Store? GetStoreById(int id)
    {
        return _storeRepo.GetStoreById(id);
    }

    public Store? DeleteStoreById(int id)
    {
        var store = _storeRepo.GetStoreById(id);
        return _storeRepo.DeleteStoreById(store!);
    }

    // *authentication
    // edit store details function

    public IEnumerable<Store> GetAllStores()
    {
        return _storeRepo.GetAllStores();
    }
}