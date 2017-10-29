using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeExtensions = Quotes.Data.Utils.TypeExtensions;

namespace Quotes.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly IMongoCollection<TEntity> Collection;

        protected BaseRepository(
            IDbContextProvider<TEntity> contextProvider)
        {
            Collection = contextProvider.GetContext();
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            return Collection.AsQueryable().ToListAsync();
        }

        public virtual Task<TEntity> Get(string id)
        {
            var objectID = TypeExtensions.Parse<ObjectId>(id);
            return Collection.FindAsync(x => x.Id.Equals(objectID)).Result.SingleAsync();
        }

        public Task<List<TEntity>> GetMany(IEnumerable<TKey> ids)
        {
            var filter = Builders<TEntity>
                .Filter
                .In(x => x.Id, ids);

            return Collection.FindAsync(filter).Result.ToListAsync();
        }

        public virtual Task<TEntity> Get(TKey id)
        {
            return Collection.FindAsync(x => x.Id.Equals(id)).Result.SingleAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            entity.DateCreated = DateTime.Now;
            entity.DateUpdated = DateTime.Now;
            Collection.InsertOne(entity);
            return entity;
        }

        public Task<ReplaceOneResult> Replace(TEntity entity)
        {
            return Collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as IList<TEntity> ?? entities.ToList();
            foreach (var entity in enumerable)
            {
                entity.DateCreated = DateTime.Now;
                entity.DateUpdated = DateTime.Now;
            }
            Collection.InsertMany(enumerable);
        }

        public virtual void Delete(TKey id)
        {
            Collection.DeleteOne(e => e.Id.Equals(id));
        }

        public virtual void Update(TEntity entity, UpdateDefinition<TEntity> updateDefinition)
        {
            entity.DateUpdated = DateTime.Now;
            Collection.UpdateOneAsync(x => x.Id.Equals(entity.Id), updateDefinition);
        }
    }
}