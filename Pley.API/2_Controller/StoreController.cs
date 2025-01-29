using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;
using System.Security.Claims;
using Pley.API.Model;

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
            var result = _storeService.Login(loginDTO.Username, loginDTO.Password);
            return Ok(result);
        }
        catch (Exception)
        {
            return BadRequest("Invalid username or password.");
        }
    }

    [Authorize]
    [HttpPatch("login")]
    public IActionResult UpdateLogin([FromBody] EditLoginInDTO loginInDTO)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var store = _storeService.GetStoreById(int.Parse(userID));

            var updatedStore = _storeService.UpdateLogin(store, loginInDTO);
            return Ok(updatedStore);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Authorize]
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        try
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest("Invalid token.");
            }

            _storeService.Logout(token);
            return Ok("Logged out successfully.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //[Authorize]
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
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteStoreById(int id)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var store = _storeService.DeleteStoreById(int.Parse(userID));
            if (store == null)
            {
                return NotFound($"No store found with Id {id}");
            }

            if (id != int.Parse(userID))
                return Unauthorized("You do not have permission to perform this action.");


            return Ok($"Successfully deleted store {id}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPatch("{id}")]
    public IActionResult UpdateStore(int id, [FromBody] EditStoreDTO editStoreDTO)
    {
        try
        {
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var store = _storeService.GetStoreById(int.Parse(userID));
            if (int.Parse(userID) != id)
            {
                return Unauthorized("You do not have permission to perform this action.");
            }

            var updatedStore = _storeService.UpdateStore(id, editStoreDTO);
            return Ok(updatedStore);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    //[Authorize]
    [HttpGet]
    public IActionResult GetAllStores()
    {
        var userList = _storeService.GetAllStores();
        if (userList is null || !userList.Any())
        {
            return NotFound("No stores found");
        }
        return Ok(userList);
    }
}