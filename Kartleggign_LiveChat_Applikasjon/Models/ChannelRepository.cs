namespace Kartleggign_LiveChat_Applikasjon.Models;

public class ChannelRepository
{
    private List<string> _channels = [];

    public string SetChannel(string channelName)
    {
        if (_channels.Contains(channelName, StringComparer.OrdinalIgnoreCase))
        {
            throw new Exception("Channel already exists");
        }
        _channels.Add(channelName);
        return channelName;
    }
    
    public List<string> GetChannels() =>  _channels;

    public bool RemoveChannel(string channelName)
    {
        return _channels.Remove(channelName);
    }
    
    public bool Contains(string  channelName) => _channels.Contains(channelName, StringComparer.OrdinalIgnoreCase); 
}