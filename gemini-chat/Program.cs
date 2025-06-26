using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

// TODO: Set your Gemini API key using .NET user-secrets or environment variable
// and update the endpoint if needed.

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

    using var httpClient = new HttpClient();
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

    var endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent";

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

        var requestBody = new
        {
            contents = new[]
            {
                new { parts = new[] { new { text = userInput } } }
            }
        };
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(endpoint + "?key=" + apiKey, content);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Error: {response.StatusCode}\n{responseString}");
            continue;
        }

        // Parse and print the Gemini response
        using var doc = JsonDocument.Parse(responseString);
        var text = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text").GetString();

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"Gemini: {text}");
    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"An error occurred: {ex.Message}");
    Console.ReadLine();
}
