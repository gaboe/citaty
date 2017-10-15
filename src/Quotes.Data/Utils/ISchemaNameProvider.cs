namespace Quotes.Data.Utils
{
    public interface ISchemaNameProvider<TEntity> where TEntity : class
    {
        string GetSchemaName();
    }
}