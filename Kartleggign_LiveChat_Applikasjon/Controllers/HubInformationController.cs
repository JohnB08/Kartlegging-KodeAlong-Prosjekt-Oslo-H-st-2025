using Kartleggign_LiveChat_Applikasjon.Models;
using Kartleggign_LiveChat_Applikasjon.SignalrHubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Kartleggign_LiveChat_Applikasjon.Controllers;

[Controller]
[Route("api/[controller]")]
public class HubInformationController(UserRepository userRepository, ChannelRepository channelRepository, UserChannelRelationshipRepository userChannelRelationshipRepository): ControllerBase
{
    [HttpGet("channels")]
    public IActionResult GetChannels()
    {
        return Ok(channelRepository.GetChannels());
    }

    [HttpGet("{channelName}/users")]
    public IActionResult GetUsers(string channelName)
    {
        return Ok(userChannelRelationshipRepository.GetUsersInChannel(channelName));
    }

    [HttpPost("users/signUp")]
    public IActionResult PostNewUser([FromBody] UserDto user)
    {
        try
        {
            return Ok(userRepository.SetUser(user.userName, user.password));
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpPost("users/logIn")]
    public IActionResult LogIn([FromBody]UserDto user)
    {
        try
        {
            userRepository.LogIn(user.userName, user.password);
            return Ok();
        } catch (Exception ex) {return Unauthorized(ex);}
    }

    [HttpPost("users/logOut/{userName}")]
    public IActionResult LogOut(string userName)
    {
        try
        {
            if (!userRepository.CheckUserLoggedIn(userName)) return NotFound();
            userRepository.LogOut(userName);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
    
    
}