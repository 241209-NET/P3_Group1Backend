using Pley.API.DTO;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pley.API.Repo;
using Pley.API.Util;
using Pley.API.Model;
using BCrypt.Net;

namespace Pley.API.Service;

public class StoreService : IStoreService
{
    private readonly IStoreRepo _storeRepo;
    private readonly Pley.API.Util.Utility _utility;
    private readonly IConfiguration _configuration;

    public StoreService(IStoreRepo storeRepo, Pley.API.Util.Utility utility, IConfiguration configuration)
    {
        _storeRepo = storeRepo;
        _utility = utility;
        _configuration = configuration;
    }

    public Store CreateNewStore(SignUpInDTO newStoreDTO)
    {
        var store = _utility.SignUpInDTOToStore(newStoreDTO);
        return _storeRepo.CreateNewStore(store);
    }

    public object Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException();

        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException();

        var user = _storeRepo.Login(username);

        if (user == null || password != user.Password)
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            null,
            claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: signIn
        );

        return new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            User = user
        };
    }

    public Store? UpdateLogin(int id, LoginInDTO loginInDTO)
    {
        var store = _storeRepo.GetStoreById(id);
        if (store == null)
        {
            throw new KeyNotFoundException($"Store with Id {id} not found");
        }

        if (!string.IsNullOrWhiteSpace(loginInDTO.Username))
        {
            store.Username = loginInDTO.Username;
        }
        if (!string.IsNullOrWhiteSpace(loginInDTO.Password))
        {
            store.Password = loginInDTO.Password;
        }

        return _storeRepo.UpdateStore(store);
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

    public Store? UpdateStore(int id, EditStoreDTO editStoreDTO)
    {
        var store = _storeRepo.GetStoreById(id);

        if (store == null)
        {
            throw new KeyNotFoundException($"Store with Id {id} not found");
        }

        if (!string.IsNullOrWhiteSpace(editStoreDTO.Name))
        {
            store.Name = editStoreDTO.Name;
        }
        if (!string.IsNullOrWhiteSpace(editStoreDTO.Description))
        {
            store.Description = editStoreDTO.Description;
        }
        if (!string.IsNullOrWhiteSpace(editStoreDTO.URL))
        {
            store.URL = editStoreDTO.URL;
        }

        return _storeRepo.UpdateStore(store);
    }

    public IEnumerable<Store> GetAllStores()
    {
        return _storeRepo.GetAllStores();
    }

}