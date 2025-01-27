using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpPost("register")]
    public IActionResult CreateNewStore([FromBody] SignUpInDTO signUpInDTO)
    {
        var store = _storeService.CreateNewStore(signUpInDTO);
        if (store == null)
        {
            return BadRequest("Invalid input for creating a new store.");
        }
        return Ok(store);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginInDTO loginDTO)
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // *authentication   (username, password)
    // [HttpPatch("login")]
    // public IActionResult EditLogin(int [FromBody] LoginInDTO)


    [HttpGet("{id}")]
    public IActionResult GetStoreById(int id)
    {
        try
        {
            var store = _storeService.GetStoreById(id);
            if (store == null)
            {
                return NotFound($"No store found with Id {id}");
            }
            return Ok(store);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteStoreById(int id)
    {
        try
        {
            var store = _storeService.DeleteStoreById(id);
            if (store == null)
            {
                return NotFound($"No store found with Id {id}");
            }
            return Ok($"Successfully deleted store {id}");
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    // *authentication
    // patch store details: Name, Description, URL

    [HttpGet]
    public IActionResult GetAllStores()
    {
        var userList = _storeService.GetAllStores();
        if(userList is null || !userList.Any()) 
        {
            return NotFound("No stores found");
        }
        return Ok(userList);
    }
}