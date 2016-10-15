namespace Swiss.Core
{
    public class ChatMessage : IChatMessage
    {
        public virtual IChatChannel Channel { get; set; }
        public virtual string Message { get; set; }
        public virtual IChatUser User { get; set; }

    }
}