using System;
using System.Collections.Generic;
using System.Linq;
using MySteam.Data;
using MySteam.Models;

namespace MySteam.UI.Pages;

public static class GameCatalogue
{
    public static bool Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Game Catalogue");
            Console.WriteLine("----------------------------------------------");

            DisplayGames(Database.Games);

            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Commands:");
            Console.WriteLine("1. Search by name");
            Console.WriteLine("2. Search by tag");
            Console.WriteLine("3. Show game page");
            Console.WriteLine("Other - Exit");
            Console.Write("Choice: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    SearchGameByName();
                    break;
                case "2":
                    SearchGameByTag();
                    break;
                case "3":
                    ShowGamePage();
                    break;
                default:
                    return false;
            }
        }
    }

    private static void DisplayGames(List<Game> games)
    {
        if (!games.Any())
        {
            Console.WriteLine("No games found.");
            return;
        }

        foreach (var game in games)
        {
            if (game != null)
                PrintGameInfo(game);
        }
    }

    private static void PrintGameInfo(Game game)
    {
        Console.WriteLine($"{game.Name} ({game.AverageRating}/5)");
        Console.WriteLine(game.Description);
        if (game.Tags != null)
            Console.WriteLine(string.Join(" ", game.Tags.Select(t => $"|{t}|")));
        Console.WriteLine();
    }

    private static void SearchGameByName()
    {
        Console.Clear();
        Console.Write("Enter game name or part of it: ");
        var input = Console.ReadLine()?.Trim();

        var foundGames = Database.Games
            .Where(g => g.Name.Contains(input ?? "", StringComparison.OrdinalIgnoreCase))
            .ToList();

        Console.Clear();
        Console.WriteLine("Search results:");
        Console.WriteLine("----------------------------------------------");
        DisplayGames(foundGames);

        if (PromptGamePage())
        {
            ShowGamePage();
        }
    }

    private static void SearchGameByTag()
    {
        Console.Clear();
        Console.Write("Enter tag: ");
        var input = Console.ReadLine()?.Trim();

        var foundGames = Database.Games
            .Where(g => g.Tags != null && g.Tags.Any(t => t.Equals(input, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        Console.Clear();
        Console.WriteLine("Search results:");
        Console.WriteLine("----------------------------------------------");
        DisplayGames(foundGames);

        if (PromptGamePage())
        {
            ShowGamePage();
        }
    }

    private static bool PromptGamePage()
    {
        Console.WriteLine("----------------------------------------------");
        Console.WriteLine("Commands:");
        Console.WriteLine("1. Show game page");
        Console.WriteLine("Other - Back");
        Console.Write("Choice: ");
        var input = Console.ReadLine();

        return input == "1";
    }

    private static void ShowGamePage()
    {
        Console.Clear();
        Console.Write("Enter exact game name: ");
        var input = Console.ReadLine()?.Trim();

        var game = Database.Games
            .FirstOrDefault(g => g.Name.Equals(input, StringComparison.OrdinalIgnoreCase));

        if (game != null)
        {
            GamePage.CurrentGame = game;
            Console.Clear();
            GamePage.Show();
            Pause();
        }
        else
        {
            Console.WriteLine("Game not found.");
            Pause();
        }
    }

    private static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
