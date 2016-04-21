using PowerFrontTestApplication.Interface;
using PowerFrontTestApplication.Models;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System;
using System.Configuration;

namespace PowerFrontTestApplication.Dapper
{
    public class ObjectOperation : IObjectOperation
    {
        private IDbConnection _db = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);

        public ObjectCompleteDescription GetSpecificObjectData(int objectTypeId, int objectPropertyId, int objectId)
        {
            string cacheKey = "ObjectDataCache_" + objectTypeId + "_" + objectPropertyId + "_" + objectId;

            ObjectCache cache = MemoryCache.Default;
            var resultFromCache = cache[cacheKey] as ObjectCompleteDescription;
            if (resultFromCache != null) return resultFromCache;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration =
                DateTimeOffset.Now.AddMinutes(10.0);

            var result = _db.Query<ObjectCompleteDescription>("SELECT od.ObjectTypeId,od.ObjectId,od.ObjectPropertyId,od.ObjectName,ot.TypeName,op.ObjectPropertyName "
                + "FROM ObjectData od inner join ObjectTypes ot on od.ObjectTypeId=ot.ObjectTypeId "
                + "inner join ObjectProperties op on od.ObjectTypeId=op.ObjectTypeId "
                + "and od.ObjectPropertyId=op.ObjectPropertyId"
                + "WHERE od.ObjectTypeId=" + objectTypeId + " and od.ObjectId=" + objectId +
                " and od.ObjectPropertyId=" + objectPropertyId).FirstOrDefault();

            cache.Set(cacheKey, result, policy);
            return result;
        }

        public List<ObjectCompleteDescription> GetAllObjectData()
        {
            string cacheKey = "ObjectDataCacheCollection";

            ObjectCache cache = MemoryCache.Default;
            var resultFromCache = cache[cacheKey] as List<ObjectCompleteDescription>;

            if (resultFromCache != null) return resultFromCache;

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration =
                DateTimeOffset.Now.AddMinutes(10.0);

            var result = _db.Query<ObjectCompleteDescription>("SELECT od.ObjectTypeId,od.ObjectId,od.ObjectPropertyId,od.ObjectName,ot.TypeName,op.ObjectPropertyName "
                + "FROM ObjectData od inner join ObjectTypes ot on od.ObjectTypeId=ot.ObjectTypeId "
                + "inner join ObjectProperties op on od.ObjectTypeId=op.ObjectTypeId "
                + "and od.ObjectPropertyId=op.ObjectPropertyId").ToList();

            cache.Set(cacheKey, result, policy);

            return result;
        }

        public void AddNewObjectData(ObjectCompleteDescription newData)
        {
            var sqlQuery = "INSERT INTO ObjectData (ObjectTypeId, ObjectId, ObjectPropertyId, ObjectName) "
                + "VALUES(@ObjectTypeId, @ObjectId, @ObjectPropertyId, @ObjectName)";
            _db.Execute(sqlQuery, newData);
        }

        public void UpdateExistingObjectData(ObjectCompleteDescription updatedData)
        {
            var sqlQuery =
                "UPDATE ObjectData " +
                "SET ObjectName = @ObjectName " +
                "WHERE ObjectTypeId = @ObjectTypeId" +
                "AND ObjectPropertyId = @ObjectPropertyId" +
                "AND ObjectId = @ObjectId";
            _db.Execute(sqlQuery, updatedData);
        }

        public void DeleteExistingObjectData(ObjectCompleteDescription deletedData)
        {
            var sqlQuery =
                "DELETE FROM ObjectData " +
                "WHERE ObjectTypeId = @ObjectTypeId" +
                "AND ObjectPropertyId = @ObjectPropertyId" +
                "AND ObjectId = @ObjectId";
            _db.Execute(sqlQuery, deletedData);
        }
    }
}