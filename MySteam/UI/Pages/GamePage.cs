using System;
using System.Linq;
using MySteam.Models;
using MySteam.Services;
using MySteam.Data;
using MySteam.Utilities;

namespace MySteam.UI.Pages;

public static class GamePage
{
    public static Game? CurrentGame { get; set; }

    public static void Show()
    {
        if (CurrentGame == null)
        {
            Logger.Log("[GamePage] No game selected.");
            Console.WriteLine("No game selected.");
            Pause();
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{CurrentGame.Name} ({CurrentGame.AverageRating}/5)");
            Console.WriteLine(CurrentGame.Description);
            Console.WriteLine($"Price: {CurrentGame.Price}");

            if (CurrentGame.Tags is { Count: > 0 })
                Console.WriteLine(string.Join(" ", CurrentGame.Tags.Select(t => $"|{t}|")));

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Options:");
            Console.WriteLine("1. Rate this game");
            Console.WriteLine("2. Buy this game");
            Console.WriteLine("3. Leave a comment");
            Console.WriteLine("4. View comments");
            Console.WriteLine("Any other key to go back");
            Console.Write("Choice: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    RateGame();
                    break;
                case "2":
                    if (AccountManager.CurrentUser != null &&
                        !AccountManager.CurrentUser.Games.Contains(CurrentGame.Name))
                        BuyGame();
                    break;
                case "3":
                    LeaveComment();
                    break;
                case "4":
                    ViewComments();
                    break;
                default:
                    return;
            }
        }
    }

    private static void RateGame()
    {
        Console.Write("Enter your rating (1-5): ");
        var input = Console.ReadLine();

        if (int.TryParse(input, out var rating) && rating is >= 1 and <= 5)
        {
            CurrentGame!.Ratings.Add(rating);
            Logger.Log($"[GamePage] {AccountManager.CurrentUser?.Login} rated {CurrentGame.Name} as {rating}/5");
            Console.WriteLine("Thank you for your rating!");
        }
        else
        {
            Logger.Log("[GamePage] Invalid rating input");
            Console.WriteLine("Invalid rating.");
        }

        Pause();
    }

    private static void BuyGame()
    {
        if (AccountManager.CurrentUser == null)
        {Console.WriteLine("You are not logged in."); return;}
        
        var user = AccountManager.CurrentUser;

        if (user.Balance < CurrentGame!.Price)
        {
            Logger.Log($"[GamePage] {user.Login} tried to buy {CurrentGame.Name} but had insufficient balance.");
            Console.WriteLine($"Not enough balance. Game costs {CurrentGame.Price:C}, your balance is {user.Balance:C}.");
        }
        else
        {
            user.Balance -= CurrentGame.Price;
            user.Games.Add(CurrentGame.Name);
            Logger.Log($"[GamePage] {user.Login} purchased {CurrentGame.Name} for {CurrentGame.Price:C}");
            Console.WriteLine($"You bought {CurrentGame.Name}!");
        }

        Pause();
    }

    private static void LeaveComment()
    {
        var user = AccountManager.CurrentUser;

        if (user == null)
        {
            Logger.Log("[GamePage] Comment attempt by anonymous user");
            Console.WriteLine("You must be logged in to leave a comment.");
            Pause();
            return;
        }

        Console.WriteLine("Enter your comment (empty input cancels):");
        var message = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(message))
        {
            Logger.Log("[GamePage] Comment cancelled by user");
            Console.WriteLine("Comment cancelled.");
            Pause();
            return;
        }

        var comment = new Comment(user.Name, user.Id, CurrentGame!.Id, message);
        CurrentGame.Comments.Add(comment);
        Database.Comments.Add(comment);
        Database.SaveAll();

        Logger.Log($"[GamePage] {user.Login} left comment on {CurrentGame.Name}: \"{message}\"");
        Console.WriteLine("Comment posted!");
        Pause();
    }

    private static void ViewComments()
    {
        Console.Clear();
        Console.WriteLine($"Comments for {CurrentGame!.Name}:");
        Console.WriteLine("------------------------------------------------");

        var comments = CurrentGame.Comments
            .OrderByDescending(c => c.DatePosted)
            .ToList();

        if (!comments.Any())
        {
            Console.WriteLine("No comments yet.");
        }
        else
        {
            foreach (var comment in comments)
            {
                Console.WriteLine($"{comment.AuthorName} ({comment.DatePosted:g}):");
                Console.WriteLine(comment.Message);
                Console.WriteLine("------------------------------------------------");
            }
        }

        Pause();
    }

    private static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey(true);
    }
}
