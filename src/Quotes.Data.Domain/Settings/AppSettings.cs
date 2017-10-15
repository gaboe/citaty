namespace Quotes.Domain.Settings
{
    public class AppSettings
    {
        public DatabaseSettings DatabaseSettings { get; set; }
        public TokenProviderSettings TokenProviderSettings { get; set; }
    }
}