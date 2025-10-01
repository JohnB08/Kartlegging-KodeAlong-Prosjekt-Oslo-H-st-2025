namespace Kartleggign_LiveChat_Applikasjon.Models;

public class ChatHistoryRepository
{
    private Dictionary<string, List<Message>> _history = [];
    
    public List<Message> GetHistoryForChannel(string channelName) => _history[channelName];

    public void StartNewHistoryForChannel(string channelName) => _history[channelName] = [];
    
    public void AddHistoryToChannel(string channelName,  Message message) => _history[channelName].Add(message);
}