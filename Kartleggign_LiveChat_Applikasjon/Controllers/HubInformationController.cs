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
    
    
}