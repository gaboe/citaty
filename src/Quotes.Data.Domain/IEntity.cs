namespace Quotes.Data.Domain
{
    public interface IEntity<TKey>
    {
        TKey ID { get; set; }
    }
}