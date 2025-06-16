using System;
using System.Linq;
using MySteam.Models;

namespace MySteam.UI.Pages;

public static class GamePage
{
    public static Game? CurrentGame { get; set; }

    public static void Show()
    {
        if (CurrentGame == null)
        {
            Console.WriteLine("No game selected.");
            return;
        }

        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"{CurrentGame.Name} ({CurrentGame.AverageRating}/5)");
        Console.WriteLine(CurrentGame.Description);
        
        if (CurrentGame.Tags != null && CurrentGame.Tags.Any())
            Console.WriteLine(string.Join(" ", CurrentGame.Tags.Select(t => $"|{t}|")));

        Console.WriteLine("------------------------------------------------");
    }
}