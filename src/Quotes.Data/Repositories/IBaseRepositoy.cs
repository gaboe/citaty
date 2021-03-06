﻿using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Quotes.Data.Repositories
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> Get(TKey id);

        Task<TEntity> Get(string id);

        Task<List<TEntity>> GetMany(IEnumerable<TKey> ids);

        TEntity Add(TEntity entity);

        Task<ReplaceOneResult> Replace(TEntity entity);

        Task<bool> Exists(TKey id);

        void AddRange(IEnumerable<TEntity> entities);

        void Delete(TKey id);

        void Update(TEntity entity, UpdateDefinition<TEntity> updateDefinition);
    }
}