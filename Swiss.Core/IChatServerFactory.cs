namespace Swiss.Core
{
    public interface IChatServerFactory
    {
        IChatServer CreateServerInstance(string key);
    }
}