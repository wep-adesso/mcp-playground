using Microsoft.Extensions.Configuration;

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

    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Hi, I am ChatGpt, please ask me anything!");

    while (true)
    {
        //using var client = new AnthropicClient(apiKey);

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("You: ");
        var userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Please enter a valid question!");
            continue;
        }

        /*var messageParams = new CreateMessageParams()
        {
            Model = new Model(ModelVariant6.ClaudeSonnet420250514),
            Messages = [
                new InputMessage(){
                    Role = InputMessageRole.User,
                    Content = userInput
                },
            ],
            MaxTokens = 250
        };
        var response = await client.Messages.MessagesPostAsync(messageParams);

        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"Claude: {response.AsSimpleText()}");*/

    }
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"An error occurred: {ex.Message}");
}