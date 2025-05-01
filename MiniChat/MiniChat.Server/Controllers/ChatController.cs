using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniChat.Shared;

namespace MiniChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ILogger<ChatController> _logger;

        public ChatController(ILogger<ChatController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        public int TestRequest()
        {
            return Random.Shared.Next();
        }

        private static readonly List<User> users = [
            new User { UserId = 0, Name = "Vahan" },
            new User { UserId = 1, Name = "Hasmik" },
            new User { UserId = 2, Name = "Irina" },
            new User { UserId = 3, Name = "Erik" },
        ];
        // LINQ
        private static List<UserMessage> messages =
            users.Select(user => new UserMessage
            {
                Content = "Barev",
                Sender = user.Name
            })
            .ToList();

        [HttpGet(nameof(Register))]
        public int Register(string name)
        {
            foreach (var user in users)
            {
                if (user.Name == name)
                {
                    return user.UserId;
                }
            }

            var newUser = new User { UserId = users.Count, Name = name };

            users.Add(newUser);

            return newUser.UserId;
        }

        [HttpGet(nameof(Send))]
        public void Send(int userId, string message)
        {
            this._logger.LogDebug($"{users[userId]}: {message}");

            var user = users[userId];

            messages.Add(new UserMessage
            {
                Content = message,
                Sender = user.Name,
            });
        }

        [HttpGet(nameof(GetMessages))]
        public List<UserMessage> GetMessages()
        {
            return messages;
        }

        [HttpGet(nameof(GetUsers))]
        public List<User> GetUsers()
        {
            return users;
        }
    }
}
