using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet("{id}")]
    public IActionResult GetStoreById(int id)
    {
        try
        {
            var store = _storeService.GetStoreById(id);
            if (store == null)
            {
                return NotFound("No store found for id = " + id);
            }
            return Ok(store);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    
    [HttpGet("username/{username}")]
    public IActionResult GetStoreByUsername(string username)
    {
        try
        {
            var store = _storeService.GetStoreByUsername(username);
            if (store == null)
            {
                return NotFound("No user found for username = " + username);
            }
            return Ok(store);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    [HttpPost]
    public IActionResult CreateNewStore([FromBody] StoreInDTO newStoreInDTO)
    {
        var store = _storeService.CreateNewStore(newStoreInDTO);
        if (store == null)
        {
            return BadRequest("Invalid input for creating a new store.");
        }
        return Ok(store);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStoreById(int id)
    {
        try
        {
            var store = _storeService.DeleteStoreById(id);
            if (store == null)
            {
                return NotFound($"No store found to delete for id = " + id);
            }
            return Ok(store);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("home")]
    public IActionResult Home()
    {
        return Ok("this works");
    }

    [HttpGet]
    public IActionResult GetAllStores()
    {
        var userList = _storeService.GetAllStores();
        if(userList is null || !userList.Any()) 
        {
            return NotFound("No stores found.");
        }
        return Ok(userList);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] StoreInDTO loginDTO)
    {
        try
        {
            var user = _storeService.Login(loginDTO.Username, loginDTO.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok(user);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // edit store
    // patch - store name, description, URL
    // patch - username, password
}