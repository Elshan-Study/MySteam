using System;
using System.IO;
using System.Text.Json;
using MySteam.Data;
using MySteam.Models;
using MySteam.Utilities;

while (true)
{
    Console.WriteLine("Menu:");
    Console.WriteLine("1. Login");
    Console.WriteLine("2. Logout");
    Console.WriteLine("3. Registration");
    Console.WriteLine("4. Exit");

    Console.WriteLine("=== Database Test ===");

    // Очистим текущие данные
    Database.LoadAll();
    Database.Users.Clear();

    // Создаём нового пользователя (с хешированием)
    var user = new User("id","demo_user", "Demo User", "demo@example.com", "qwerty123");
    var user2 = new User("id","demo_user", "Demo User", "demo@example.com", "qwerty123");
    var game = new Game("game", "id", "demo_game");
    var comment = new Comment("Author", "id", "ddd","demo_comment");
    Database.Users.Add(user);
    Database.Users.Add(user2);
    Database.Games.Add(game);
    Database.Comments.Add(comment);

    // Сохраняем в файл
    Database.SaveAll();
    Console.WriteLine("✅ User saved.");

    // Загружаем заново
    Database.LoadAll();

    // Проверяем: выводим всех пользователей
    Console.WriteLine("=== Loaded Users ===");
    foreach (var u in Database.Users)
    {
        Console.WriteLine($"Login: {u.Login}, Email: {u.Email}, PasswordHash: {u.Password}");
    }
    
    foreach (var u in Database.Games)
    {
        Console.WriteLine($"Game: {u.Name}");
    }

    foreach (var u in Database.Comments)
    {
        Console.WriteLine($"Comment: {u.Message}");
    }

    Console.WriteLine("=== End of Test ===");
    
    break;
}

