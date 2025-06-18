using System;

namespace MySteam.Models;

/// <summary>
/// Represents a comment left by a user on a game page.
/// </summary>
public class Comment(string authorName, string authorId, string gameId, string message)
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AuthorName { get; set; } = authorName;
    public string AuthorId { get; set; } = authorId;
    public string GameId { get; set; } = gameId;
    public string Message { get; set; } = message;
    public DateTime DatePosted { get; set; } = DateTime.UtcNow;
}