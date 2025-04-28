using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiniChat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
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

        private static List<string> messages = [
            "Vahan: Barev",
            "Hasmik: Barev",
            "Irina: Barev",
            "Erik: Barev",
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
            messages.Add($"{users[userId]}: {message}");
        }

        [HttpGet(nameof(GetMessages))]
        public List<string> GetMessages()
        {
            return messages;
        }
    }
}
