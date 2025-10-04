using Kartleggign_LiveChat_Applikasjon.Models;
using Microsoft.AspNetCore.SignalR;

namespace Kartleggign_LiveChat_Applikasjon.SignalrHubs;

public class ChatHub(
    ChannelRepository channelRepository,
    UserRepository userRepository,
    UserChannelRelationshipRepository userChannelRelationshipRepository,
    ChatHistoryRepository chatHistoryRepository,
    ILogger<ChatHub> logger
    ): Hub
{
    
    
    public async Task JoinChannel(string channelName, string userName)
    {
        if (!channelRepository.Contains(channelName)) throw new HubException("Channel not found");
        if (!userRepository.CheckUserLoggedIn(userName)) throw new HubException("User not found");
        userChannelRelationshipRepository.AddUserChannelRelationship(userName, channelName);
        logger.LogInformation($"User {userName} has joined the channel {channelName}");
        await Groups.AddToGroupAsync(Context.ConnectionId, channelName);
        var history = chatHistoryRepository.GetHistoryForChannel(channelName);
        if (history.Count > 0)
        {
            for (var i = 0; i < history.Count; i++)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", history[i].UserName, history[i].Content);
            }
        }
    }

    public async Task CreateChannel(string channelName, string userName)
    {
        if (channelRepository.Contains(channelName)) throw new HubException("Channel not found");
        if (!userRepository.CheckUserLoggedIn(userName)) throw new HubException("User not found");
        userChannelRelationshipRepository.AddUserChannelRelationship(userName, channelName);
        logger.LogInformation($"User {userName} has created the channel {channelName}");
        channelRepository.SetChannel(channelName);
        chatHistoryRepository.StartNewHistoryForChannel(channelName);
        await Groups.AddToGroupAsync(Context.ConnectionId, channelName);
    }

    public async Task LeaveChannel(string channelName, string userName)
    {
        if (!userRepository.CheckUserLoggedIn(userName)) throw new HubException("User not found");
        if (!userChannelRelationshipRepository.ChannelContainsUser(channelName, userName)) throw new HubException("Channel not found");
        userChannelRelationshipRepository.RemoveUserChannelRelationship(userName, channelName);
        logger.LogInformation($"User {userName} has left the channel {channelName}");
        await  Groups.RemoveFromGroupAsync(Context.ConnectionId, channelName);
    }

    public async Task SendMessageToChannel(string channel, string userName, string message)
    {
        if (!userChannelRelationshipRepository.ChannelContainsUser(channel, userName)) throw new HubException("User not found in channel");
        logger.LogInformation($"User {userName} has sent the message {message} to the channel {channel}");
        var messageForHistory = new Message
        {
            UserName = userName,
            Content = message,
            SentAt = DateTime.UtcNow
        };
        chatHistoryRepository.AddHistoryToChannel(channel, messageForHistory);
        await Clients.OthersInGroup(channel).SendAsync("ReceiveMessage", userName, message);
    }
}