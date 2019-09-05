namespace DeveVipAccess.Tests.Tokens
{
    public interface ITokenInformation
    {
        int Version { get; }
        string Secret { get; }
        string Id { get; }
        string Expiry { get; }
    }
}
