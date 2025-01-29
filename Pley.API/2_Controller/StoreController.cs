using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
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