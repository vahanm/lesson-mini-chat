﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using MiniChat.Shared;

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

        private static readonly JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
        };

        static T Request<T>(string endpoint, string args)
        {
            var json = Request(endpoint, args);

            return JsonSerializer.Deserialize<T>(json, options);
        }

        static void Main(string[] args)
        {
            var users = Request<List<User>>("GetUsers", "");

            foreach (var user in users)
            {
                Console.WriteLine($"{user.UserId}: {user.Name}");
            }

            Console.Write("Enter user name: ");

            var userName = Console.ReadLine();

            if (String.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Invalid user name.");

                return;
            }

            var userId = Request<int>("Register", $"name={userName}");

            while (true)
            {
                Console.Clear();

                Console.WriteLine($"Me: {userId}: {userName}");
                Console.WriteLine();
                Console.WriteLine();

                var messages = Request<List<UserMessage>>("GetMessages", "");

                foreach (var item in messages)
                {
                    Console.WriteLine(item.Sender);
                    Console.WriteLine("\t" + item.Content);
                }

                var message = Console.ReadLine();

                if (!String.IsNullOrEmpty(message))
                {
                    Request("Send", $"userId={userId}&message={message}");
                }
            }
        }
    }
}
