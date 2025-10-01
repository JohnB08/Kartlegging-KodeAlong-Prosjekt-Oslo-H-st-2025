namespace Kartleggign_LiveChat_Applikasjon.Models;

public class Message
{
    public string UserName { get; set; }
    public string Content { get; set; }
    public int Likes;
    public int Dislikes;
    public DateTime SentAt;
}