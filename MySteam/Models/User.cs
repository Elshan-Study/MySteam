using System;
using System.Collections.Generic;

namespace MySteam.Models;

/// <summary>
/// Represents the platform user: id, login, name, email, password, balance and list of games.
/// </summary>
public class User
{
    public User(string id, string login, string name, string email, string password)
    {
        Id = id;
        Login = login;
        Name = name;
        Email = email;
        Password = password; 
    }

    public string Id { get; init; }
    public string Login { get; init; }
    public string Name { get; set; }
    public string Email { get; init; }
    public string Password { get; init; }

    public decimal Balance { get; set; } = 0;
    public List<string> Games { get; set; } = [];
    public List<string> HiddenGames { get; set; } = [];
}