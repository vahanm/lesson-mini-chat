namespace MiniChat.Shared
{
    public class UserMessage
    {
        public string Content { get; set; }
        public string Sender { get; set; }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}
