namespace MiniChat.Client
{
    internal class Program
    {
        static HttpClient client = new HttpClient();

        static string Request(string endpoint, string args)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"http://localhost:5296/api/Chat/{endpoint}?{args}"
            );

            var response = client.Send(request);

            response.EnsureSuccessStatusCode();

            return response.Content
                        .ReadAsStringAsync()
                        .Result;
        }


        static void Main(string[] args)
        {
            var userId = Int32.Parse(Console.ReadLine());

            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    Request("GetMessages", "")
                );

                //Thread.Sleep(1000);

                var message = Console.ReadLine();

                if (!String.IsNullOrEmpty(message))
                {
                    Request("Send", $"userId={userId}&message={message}");
                }
            }
        }
    }
}
