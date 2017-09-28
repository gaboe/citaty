using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Quotes.Data.Context;
using Quotes.Data.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypeExtensions = Quotes.Data.Utils.TypeExtensions;

namespace Quotes.Data.Repositories
{
    public abstract class BaseRepository<TEntity, TKey, TRepository> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        protected readonly ILogger Logger;
        protected readonly IMongoCollection<TEntity> Collection;

        protected BaseRepository(
            ILogger<TRepository> logger,
            IDbContextProvider<TEntity> contextProvider)
        {
            Logger = logger;
            Collection = contextProvider.GetContext();
        }

        public virtual Task<List<TEntity>> GetAll()
        {
            //Logger.LogInformation($"Get all ${nameof(TEntity)}");
            return Collection.AsQueryable().ToListAsync();
        }

        public virtual Task<TEntity> Get(string id)
        {
            //Logger.LogInformation($"Get ${nameof(TEntity)} with id = ${id}");
            var objectID = TypeExtensions.Parse<ObjectId>(id);
            return Collection.FindAsync(x => x.ID.Equals(objectID)).Result.SingleAsync();
        }

        public virtual Task<TEntity> Get(TKey id)
        {
            //Logger.LogInformation($"Get ${nameof(TEntity)} with id = ${id}");
            return Collection.FindAsync(x => x.ID.Equals(id)).Result.SingleAsync();
        }

        public virtual TEntity Add(TEntity entity)
        {
            Collection.InsertOne(entity);
            //Logger.LogInformation($"Add entity with id = ${entity.ID}");
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            var enumerable = entities as IList<TEntity> ?? entities.ToList();
            Collection.InsertMany(enumerable);
            //Logger.LogInformation($"Add range of entities with ids = ${enumerable.Select(x => x.ID)}");
        }

        public virtual void Delete(TKey id)
        {
            //Logger.LogInformation($"Delete entity with id = ${id}");
            Collection.DeleteOne(e => e.ID.Equals(id));
        }

        public virtual void Update(TEntity entity, UpdateDefinition<TEntity> updateDefinition)
        {
            //Logger.LogInformation($"Update entity fields ${updateDefinition}");
            Collection.UpdateOneAsync(x => x.ID.Equals(entity.ID), updateDefinition);
        }
    }
}