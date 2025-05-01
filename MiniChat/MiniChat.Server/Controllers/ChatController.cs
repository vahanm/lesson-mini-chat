using System;
using System.Collections.Generic;
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

        private static List<string> users = [
            "Vahan",
            "Hasmik",
            "Irina",
            "Erik",
        ];

        private static List<UserMessage> messages = [
            new UserMessage {
                Content = "Barev",
                Sender = "Vahan"
            },
            new UserMessage {
                Content = "Barev",
                Sender = "Erik" 
            },
            
        ];

        [HttpGet(nameof(Register))]
        public int Register(string name)
        {
            users.Add(name);

            return users.Count - 1;
        }

        [HttpGet(nameof(Send))]
        public void Send(int userId, string message)
        {
            this._logger.LogDebug($"{users[userId]}: {message}");

            messages.Add(new UserMessage
            {
                Content = message,
                Sender = users[userId]
            });
            //$"{users[userId]}: {message}"
        }

        [HttpGet(nameof(GetMessages))]
        public List<UserMessage> GetMessages()
        {
            return messages;
        }
    }
}
