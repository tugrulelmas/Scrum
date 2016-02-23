using AbiokaScrum.Api.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.Data.NoSql.Mongo.Map
{
    public class Mapper
    {
        private static readonly IDictionary<RuntimeTypeHandle, string> collectionNames;

        static Mapper() {
            collectionNames = new Dictionary<RuntimeTypeHandle, string>();
            collectionNames.Add(typeof(Label).TypeHandle, "label");
            collectionNames.Add(typeof(Board).TypeHandle, "board");
        }

        public static void Map() {
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIdGenerator(GuidGenerator.Instance);
            });

            BsonClassMap.RegisterClassMap<Label>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<Board>(cm =>
            {
                cm.AutoMap();
            });
        }

        public static string GetCollectionName<T>() {
            var typeHandle = typeof(T).TypeHandle;
            if (!collectionNames.ContainsKey(typeHandle))
                throw new NotSupportedException(string.Format("{0} is not registered type in collection names.", typeof(T).Name));

            return collectionNames[typeHandle];
        }
    }
}