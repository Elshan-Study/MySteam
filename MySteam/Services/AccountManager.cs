using System;
using System.Dynamic;
using System.Linq;
using MySteam.Data;
using MySteam.Exceptions;
using MySteam.Models;
using MySteam.Utilities;

namespace MySteam.Services;

public static class AccountManager
{
    public delegate void Action(string message);
    public static event Action? Notify;
    public static User CurrentUser { get; set; } = null!;
    
    public static void Register(string login, string name, string email, string password)
    {
        if (Database.Users.Any(u => u.Login == login || u.Email == email))
        {
            throw new UserExistsException("User already exists");
        } 
        
        var hashedPassword = PasswordHasher.Hash(password);
        
        var id = Guid.NewGuid().ToString();
        
        var newUser = new User(id, login, name, email, hashedPassword);
        
        Database.Users.Add(newUser);
        
        Notify?.Invoke("User created");
    }
    
    public static bool LoginByEmail(string email, string password)
    {
        
    }
    
    public static bool LoginByUserLogin(string login, string password)
    {
        
    }
    
    public static void Logout()
    {
        
    }
}