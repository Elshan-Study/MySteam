using System;
using System.Collections.Generic;
using System.Linq;

namespace MySteam.Models;

/// <summary>
/// Game model in the global library: contains genres, ratings and comments.
/// </summary>
public class Game(string id, string name, string description)
{
    public string Id { get; set; } = id; //Guid.NewGuid().ToString(); - не забываем
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public List<string> Tags { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<int> Ratings { get; set; } = [];

    public double AverageRating => Ratings.Count == 0 ? 0 : Ratings.Average();
}