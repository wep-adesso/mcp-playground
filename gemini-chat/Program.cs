using Microsoft.Extensions.Configuration;
using Gemini;
using GeminiDotNET;
using GeminiDotNET.ApiModels.Enums;

try
{
    var config = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    var apiKey = config["Gemini:ApiKey"];
    if (string.IsNullOrWhiteSpace(apiKey) || apiKey == "your-gemini-api-key")
    {
        throw new InvalidOperationException("No valid Gemini API key configured. Please set a valid key using .NET user-secrets.");
    }

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Hi, I am Gemini, please ask me anything!");

    var generator = new Generator(apiKey);

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

        var request = new ApiRequestBuilder()
       .WithPrompt(userInput)
       .WithDefaultGenerationConfig(temperature: 0.7f, maxOutputTokens: 512) // Optional configuration
       .Build();

        var response = await generator.GenerateContentAsync(request, ModelVersion.Gemini_20_Flash_Lite);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"Gemini: {response.Content}");
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.ReadLine();
}
