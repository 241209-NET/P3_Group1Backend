using Pley.API.DTO;
using Pley.API.Repo;
using Pley.API.Util;

namespace Pley.API.Service;

public class StoreService : IStoreService
{

    //     StoreOutDTO CreateNewStore(StoreInDTO newStore); 
    // IEnumerable<Store> GetAllStores();
    // StoreOutDTO? GetStoreById(int id); 
    // StoreOutDTO? DeleteStoreById(int id);
    // StoreOutDTO? Login(string userName, string Password);
    // StoreOutDTO? GetStoreByUsername(string username);

    private readonly IStoreRepo _storeRepo;
    private readonly Utility _utility;
    public StoreService(IStoreRepo storeRepo, Utility utility)
    {
        _storeRepo = storeRepo;
        _utility = utility;
    }

    public StoreOutDTO? Login(string Username, string Password)
    {
        throw new NotImplementedException();
    }

    public StoreOutDTO CreateNewStore(StoreInDTO newStoreDTO)
    {
        var store = _utility.StoreInDTOToStore(newStoreDTO);
        var newStore = _storeRepo.CreateNewStore(store);        
        return _utility.StoreToStoreOutDTO(newStore);
    }

    public IEnumerable<StoreOutDTO> GetAllStores()
    {
        var stores = _storeRepo.GetAllStores();

        return stores.Select(_utility.StoreToStoreOutDTO);
    }

    public StoreOutDTO? GetStoreById(int id)
    {
        var store = _storeRepo.GetStoreById(id);

        if(store is null)
            return null;

        return _utility.StoreToStoreOutDTO(store);
    }

    public StoreOutDTO? DeleteStoreById(int id)
    {
        var store = _storeRepo.GetStoreById(id);

        if (store is not null)
        {
            _storeRepo.DeleteStoreById(id);
            return _utility.StoreToStoreOutDTO(store);
        }

        return null;
    }

    public StoreOutDTO GetStoreByUsername(string Username)
    {
        throw new NotImplementedException();
    }

}