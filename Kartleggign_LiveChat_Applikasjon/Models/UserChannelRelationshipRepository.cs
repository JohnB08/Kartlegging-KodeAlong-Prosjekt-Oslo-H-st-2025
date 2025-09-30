namespace Kartleggign_LiveChat_Applikasjon.Models;

public class UserChannelRelationshipRepository
{
    private Dictionary<string, List<string>> _userChannelRelationship = [];
    
    public List<string> GetUsersInChannel(string userId) =>  _userChannelRelationship[userId];

    public void AddUserChannelRelationship(string userName, string channelName)
    {
        if (_userChannelRelationship.TryGetValue(channelName, out var users))
        {
            users.Add(userName);
        }
        else _userChannelRelationship.Add(channelName, [userName]);
    }

    public void RemoveUserChannelRelationship(string userName, string channelName)
    {
        if (_userChannelRelationship.TryGetValue(channelName, out var users))
        {
            users.Remove(userName);
        }
    }

    public bool ChannelContainsUser(string channelName, string userName)
    {
        var channel = _userChannelRelationship[channelName];
        return channel.Contains(userName);
    }
}