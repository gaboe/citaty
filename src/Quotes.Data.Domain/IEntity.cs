using System;

namespace Quotes.Domain
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }

        DateTime DateCreated { get; set; }

        DateTime DateUpdated { get; set; }
    }
}