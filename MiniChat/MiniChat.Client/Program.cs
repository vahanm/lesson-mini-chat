namespace MiniChat.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var client = new HttpClient();

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5296/WeatherForecast")
                {
                    Content = new StringContent("""
                    {
                        "Barev": "Arev"
                    }
                    """, null, "application/json")
                };

                var response = client.Send(request);

                response.EnsureSuccessStatusCode();

                Console.WriteLine(
                    response.Content
                        .ReadAsStringAsync()
                        .Result
                );
            }
        }
    }
}
