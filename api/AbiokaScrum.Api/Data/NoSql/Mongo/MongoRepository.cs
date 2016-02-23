using AbiokaScrum.Api.Data.NoSql.Mongo.Map;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace AbiokaScrum.Api.Data.NoSql.Mongo
{
    public class MongoRepository : IRepository
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;

        static MongoRepository() {
            var client = ConfigurationManager.AppSettings["MongoDbClient"];
            if (string.IsNullOrWhiteSpace(client))
                throw new ArgumentNullException("MongoDbClient");

            var mongoDb = ConfigurationManager.AppSettings["MongoDb"];
            if (string.IsNullOrWhiteSpace(mongoDb))
                throw new ArgumentNullException("MongoDb");

            _client = new MongoClient(client);
            _database = _client.GetDatabase(mongoDb);
        }

        public virtual void Add<T>(T entity) where T : class, new() {
            if (typeof(Entities.IDeletableEntity).IsAssignableFrom(typeof(T)))
            {
                ((Entities.IDeletableEntity)entity).IsDeleted = false;
            }
            if (typeof(Entities.IBaseEntity).IsAssignableFrom(typeof(T)))
            {
                ((Entities.IBaseEntity)entity).CreateDate = DateTime.Now;
            }

            var collection = _database.GetCollection<T>(Mapper.GetCollectionName<T>());
            collection.InsertOne(entity);
        }

        public IEnumerable<T> Get<T>() where T : class, new() {
            var collection = _database.GetCollection<T>(Mapper.GetCollectionName<T>());
            return collection.AsQueryable().ToList();
        }

        public IEnumerable<T> GetBy<T>(Expression<Func<T, bool>> predicate) where T : class, new() {
            var collection = _database.GetCollection<T>(Mapper.GetCollectionName<T>());
            return collection.Find(predicate).ToList();
        }

        public IEnumerable<T> GetBy<T>(FilterDefinition<T> filter) where T : class, new() {
            var collection = _database.GetCollection<T>(Mapper.GetCollectionName<T>());
            return collection.Find(filter).ToList();
        }

        public IQueryable<T> Queryable<T>() where T : class, new() {
            var collection = _database.GetCollection<T>(Mapper.GetCollectionName<T>());
            return collection.AsQueryable();
        }

        public T GetByKey<T>(object key) where T : class, new() {
            throw new NotImplementedException();
        }

        public bool Remove<T>(T entity) where T : class, new() {
            throw new NotImplementedException();
        }

        public bool Update<T>(T entity) where T : class, new() {
            throw new NotImplementedException();
        }
    }
}