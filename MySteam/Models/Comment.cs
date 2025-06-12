using System;

namespace MySteam.Models;

/// <summary>
/// Represents a comment left by a user on a game page.
/// </summary>
public class Comment
{
    public Comment(string authorName, string authorId, string gameId, string message)
    {
        Id = Guid.NewGuid().ToString();
        AuthorName = authorName;
        AuthorId = authorId;
        GameId = gameId;
        Message = message;
        DatePosted = DateTime.UtcNow;
    }
    
    // var comment = new Comment(user.Name, user.Id, game.Id, "Крутая игра!");
    // Database.Comments.Add(comment);
    // Database.SaveAll();
    
    //А чтобы получить комментарии к конкретной игре:
    //var gameComments = Database.Comments.Where(c => c.GameId == game.Id).ToList();

    public string Id { get; set; }
    public string AuthorName { get; set; }
    public string AuthorId { get; set; }
    public string GameId { get; set; }
    public string Message { get; set; }
    public DateTime DatePosted { get; set; }
}