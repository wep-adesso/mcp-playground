while (true)
{
    try
    {
        Console.WriteLine("Hi, I am Claude, please ask me anything!");
        var userInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(userInput))
        {
            Console.WriteLine("Please enter a valid question.");
            continue;
        }
 
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
}
