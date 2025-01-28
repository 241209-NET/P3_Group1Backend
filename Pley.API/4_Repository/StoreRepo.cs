using Pley.API.Model;
using Pley.API.Data;

namespace Pley.API.Repo;

public class StoreRepo : IStoreRepo
{
    private readonly PleyContext _pleyContext;
    public StoreRepo(PleyContext pleyContext) => _pleyContext = pleyContext;

    public Store CreateNewStore(Store newStore)
    {
        _pleyContext.Stores.Add(newStore);
        _pleyContext.SaveChanges();
        return newStore;
    }

    public Store Login(string username)
    {
        return _pleyContext.Stores.FirstOrDefault(u => u.Username == username)!;
    }

    public Store? GetStoreById(int id)
    {
        return _pleyContext.Stores.Find(id);
    }

    public Store? DeleteStoreById(Store store)
    {
        _pleyContext.Stores.Remove(store);
        _pleyContext.SaveChanges();
        return store;
    }

    public Store UpdateStore(Store store)
    {
        _pleyContext.Stores.Update(store);
        _pleyContext.SaveChanges();
        return store;
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _pleyContext.Stores.ToList();
    }
}