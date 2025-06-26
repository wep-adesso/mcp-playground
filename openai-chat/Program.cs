using Microsoft.Extensions.Configuration;
using OpenAI.Chat;

try
{
    var config = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    var apiKey = config["OpenAi:ApiKey"];

    if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "your-openai-api-key")
    {
        throw new InvalidOperationException("No valid OpenAi API key configured. Please set a valid key using .NET user-secrets.");
    }

    var client = new ChatClient(model: "gpt-3.5-turbo", apiKey: apiKey);

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Hi, I am ChatGPT, please ask me anything!");

    while (true)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("You: ");
        var userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Please enter a valid question!");
            continue;
        }

        var chatCompletion = await client.CompleteChatAsync(userInput);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"ChatGPT: {chatCompletion.Value.Content.FirstOrDefault()?.Text}");
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.ReadLine();
}