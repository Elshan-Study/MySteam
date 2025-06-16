using System;
using System.Collections.Generic;
using System.Linq;

namespace MySteam.Models;

/// <summary>
/// Game model in the global library: contains genres, ratings and comments.
/// </summary>
public class Game(string name, string description, string id, decimal price)
{ 
    public string Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    
    public decimal Price { get; set; } = price;

    public List<string>? Tags { get; set; } = [];
    public List<Comment> Comments { get; set; } = [];
    public List<int> Ratings { get; set; } = [];

    public double AverageRating => Ratings.Count == 0 ? 0 : Math.Round(Ratings.Average(), 2);
}