namespace Infrastructure.Common;

public class DatabaseSettings
{
    public string Host { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int MaxConnections { get; set; } = 100;
    public string TrustServerCertificate { get; set; } = "True";
    public string ConnectionString
    {
        get
        {
            return $"Host={Host};Port={Port};Username={UserId};Password={Password};Database={Database};Pooling=true;Maximum Pool Size={MaxConnections};";
        }
    }
}

