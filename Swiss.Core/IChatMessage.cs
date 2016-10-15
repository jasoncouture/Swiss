namespace Swiss.Core
{
    public interface IChatMessage
    {
        IChatChannel Channel { get; }
        string Message { get; }
        IChatUser User { get; }
    }
}