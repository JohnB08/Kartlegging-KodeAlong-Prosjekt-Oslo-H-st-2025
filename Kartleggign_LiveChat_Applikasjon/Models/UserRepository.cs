using Kartleggign_LiveChat_Applikasjon.Models.Extensions;

namespace Kartleggign_LiveChat_Applikasjon.Models;

public class UserRepository
{
    private List<User> _users = [];

    public string SetUser(string userName, string password)
    {
        var newUser = User.New(userName, password);
        if (_users.Contains(newUser)) throw new Exception("User already exists");
        _users.Add(newUser with {Online = true});
        return userName;
    }
    
    public IEnumerable<string> GetAllUsers() => _users.Select(user => user.NickName);
    
    public bool RemoveUser(string userName) =>  _users.Remove(_users.FirstOrDefault(u => u.NickName == userName) ?? throw new Exception("User not found"));
    
    public bool Contains(string userName, string password) =>  _users.Contains(User.New(userName, password));
    
    public User VerifyUser(string userName, string password) => _users.FirstOrDefault(u => u.Verify(userName, password)) ?? throw new Exception("User not found");

    public void LogIn(string userName, string password)
    {
        if (VerifyUser(userName, password) is not { } user) throw new Exception("Cannot Log in current user.");
        _users.Remove(user);
        _users.Add(user with {Online = true});
    }

    public void LogOut(string userName) => _users.RemoveAll(u => u.Online && u.NickName.Equals(userName, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<User> LoggedInUsers() => _users.Where(u => u.Online);
    
    public bool CheckUserLoggedIn(string userName) => LoggedInUsers().Any(u => u.NickName.Equals(userName, StringComparison.OrdinalIgnoreCase));
}