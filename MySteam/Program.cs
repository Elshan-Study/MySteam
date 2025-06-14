using System;
using MySteam.Data;
using MySteam.Exceptions;
using MySteam.Services;
using MySteam.Utilities;

Database.LoadAll();

AccountManager.Notify += msg => Console.WriteLine($"[Info] {msg}");
AccountManager.Logout();

while (true)
{
    Console.Clear();

    Console.WriteLine("Menu:");
    Console.WriteLine("1. Login | Logout");
    Console.WriteLine("2. Registration");
    Console.WriteLine("3. Exit");
    Console.WriteLine("Or type anything else for anonymous mode");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            LoginLogoutMode();
            break;
        case "2":
            RegistrationMode();
            break;
        case "3":
            Database.SaveAll();
            return;
        default:
            Console.WriteLine("Continuing in anonymous mode...");
            //Add anonymous user or guest mode handling
            Console.ReadKey();
            break;
    }
}

bool LoginLogoutMode()
{
    Console.Clear();

    if (AccountManager.CurrentUser != null)
    {
        AccountManager.Logout();
        Console.WriteLine("Logged out.");
        Console.ReadKey();
        return true;
    }

    Console.WriteLine("Enter email or login:");
    var identifier = Console.ReadLine()?.Trim();

    Console.WriteLine("Enter password:");
    var password = Console.ReadLine()?.Trim();;

    if (string.IsNullOrWhiteSpace(identifier) || string.IsNullOrWhiteSpace(password))
    {
        Console.WriteLine("Fields cannot be empty.");
        Console.ReadKey();
        return false;
    }

    try
    {
        AccountManager.LoginByEmail(identifier, password);
    }
    catch (UserSearchException)
    {
        try
        {
            AccountManager.LoginByUserLogin(identifier, password);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Login failed: {ex.Message}");
            Logger.LogException(ex, "LoginByUserLogin");
            Console.ReadKey();
            return false;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Login failed: {ex.Message}");
        Logger.LogException(ex, "LoginByEmail");
        Console.ReadKey();
        return false;
    }

    Console.WriteLine($"Welcome, {AccountManager.CurrentUser!.Name}!");
    Console.ReadKey();
    return true;
}

bool RegistrationMode()
{
    Console.Clear();

    Console.WriteLine("Enter login:");
    var login = Console.ReadLine();
    if (!Validator.IsValidLogin(login))
    {
        Console.WriteLine("Login must be at least 3 characters and contain only letters, numbers, dashes or underscores.");
        Console.ReadKey();
        return false;
    }

    Console.WriteLine("Enter email:");
    var email = Console.ReadLine();
    if (!Validator.IsValidEmail(email))
    {
        Console.WriteLine("Invalid email format.");
        Console.ReadKey();
        return false;
    }

    Console.WriteLine("Enter name:");
    var name = Console.ReadLine();
    if (!Validator.IsValidName(name))
    {
        Console.WriteLine("Name must be at least 2 characters.");
        Console.ReadKey();
        return false;
    }

    Console.WriteLine("Enter password:");
    var password = Console.ReadLine();
    if (!Validator.IsValidPassword(password))
    {
        Console.WriteLine("Password must be at least 8 characters and contain both letters and digits.");
        Console.ReadKey();
        return false;
    }

    try
    {
        AccountManager.Register(login!, name!, email!, password!);
        Console.WriteLine("Registration successful.");
        Console.ReadKey();
        return true;
    }
    catch (UserExistsException ex)
    {
        Console.WriteLine(ex.Message);
        Logger.LogException(ex, "RegistrationMode");
        Console.ReadKey();
        return false;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Unexpected error: " + ex.Message);
        Logger.LogException(ex, "RegistrationMode");
        Console.ReadKey();
        return false;
    }
}
