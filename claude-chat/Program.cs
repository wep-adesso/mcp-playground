using Anthropic;


        Console.ForegroundColor = ConsoleColor.Cyan; 
        Console.WriteLine("Hi, I am Claude, please ask me anything!");
        
while (true)
{

    try
    {
        using var client = new AnthropicClient("");

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("You: ");
        var userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Please enter a valid question!");
            continue;
        }

        var messageParams = new CreateMessageParams()
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
        Console.WriteLine($"Claude: {response.AsSimpleText()}");
    }
    catch (Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
