using System;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Completions;

class Program
{
    static async Task Main()
    {
        string apiKey = "addyourkeyhere";
        var api = new OpenAIAPI(apiKey);

        Console.WriteLine("Welcome to the OpenAI Interactive Console!");
        Console.WriteLine("Type 'exit' to quit the program.");

        while (true)
        {
            Console.Write("You: ");
            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "exit")
            {
                break;
            }

            try
            {
                var completionRequest = new CompletionRequest
                {
                    Prompt = userInput,
                    MaxTokens = 150, 
                    Temperature = 0.7 
                };

                var completion = await api.Completions.CreateCompletionAsync(completionRequest);
                string response = completion.Completions[0].Text.Trim();

                Console.WriteLine("AI: " + response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
