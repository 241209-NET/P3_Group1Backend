using Pley.API.Model;
using Pley.API.Data;
//using Microsoft.EntityFrameworkCore;

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

    public Store LoginStore(string Username, string Password)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _pleyContext.Stores;
    }

    public Store? GetStoreById(int reviewId)
    {
        return _pleyContext.Stores.Find(reviewId);
    }

    public Store? DeleteStoreById(int id)
    {
        Store? review = GetStoreById(id);
        if(review is null) 
            return null; //throw new StoreNotFoundException();

        // review is not null, proceed
        _pleyContext.Stores.Remove(review);
        _pleyContext.SaveChanges();
        return review;
    }

    public Store GetStoreByName(string name)
    {
        return _pleyContext.Stores.Find(name)!;
    }
}