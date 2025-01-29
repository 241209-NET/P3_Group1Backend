using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pley.API.DTO;
using Pley.API.Service;

namespace Pley.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
  private readonly IStoreService _storeService;

  public AccountController(IStoreService storeService)
  {
    _storeService = storeService;
  }

  [Authorize]
  [HttpPatch("login/edit")]
  public IActionResult UpdateLogin([FromBody] EditLoginDTO loginInDTO)
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

  [HttpPost("login")]
  public IActionResult Login([FromBody] LoginDTO loginDTO)
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

  [Authorize]
  [HttpGet]
  public IActionResult GetLoginInfo()
  {
    try{
      var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var loginInfo = _storeService.GetLoginInfo(int.Parse(userID!));

      if (loginInfo == null)
      {
        return Unauthorized("You must be logged in to access the account.");
      }
      return Ok(loginInfo);
    }catch(Exception e){
      return Unauthorized(e.Message);
    }
  }

}