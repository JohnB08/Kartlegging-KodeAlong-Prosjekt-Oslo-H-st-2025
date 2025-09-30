namespace Kartleggign_LiveChat_Applikasjon.Models;

public class UserRepository
{
    private List<string> _users = [];

    public string SetUser(string userName)
    {
        if (_users.Contains(userName, StringComparer.OrdinalIgnoreCase)) throw new Exception("User already exists");
        _users.Add(userName);
        return userName;
    }
    
    public List<string> GetAllUsers() => _users;
    
    public bool RemoveUser(string userName) =>  _users.Remove(userName);
    
    public bool Contains(string userName) =>  _users.Contains(userName, StringComparer.OrdinalIgnoreCase);
}