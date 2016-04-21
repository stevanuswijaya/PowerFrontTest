using PowerFrontTestApplication.Interface;
using PowerFrontTestApplication.Models;
using System.Collections.Generic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace PowerFrontTestApplication.Dapper
{
    public class ObjectOperation : IObjectOperation
    {
        private IDbConnection _db = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\PowerFrontTestApplication\\PowerFrontTestApplication\\App_Data\\PowerFrontDatabase.mdf;Integrated Security=True");

        public ObjectCompleteDescription GetSpecificObjectData(int objectTypeId, int objectPropertyId, int objectId)
        {
            return _db.Query<ObjectCompleteDescription>("SELECT od.ObjectTypeId,od.ObjectId,od.ObjectPropertyId,od.ObjectName,ot.TypeName,op.ObjectPropertyName "
                + "FROM ObjectData od inner join ObjectTypes ot on od.ObjectTypeId=ot.ObjectTypeId "
                + "inner join ObjectProperties op on od.ObjectTypeId=op.ObjectTypeId "
                + "and od.ObjectPropertyId=op.ObjectPropertyId" 
                + "WHERE od.ObjectTypeId=" + objectTypeId + " and od.ObjectId=" + objectId +
                " and od.ObjectPropertyId=" + objectPropertyId).FirstOrDefault();
        }

        public List<ObjectCompleteDescription> GetAllObjectData()
        {
            return _db.Query<ObjectCompleteDescription>("SELECT od.ObjectTypeId,od.ObjectId,od.ObjectPropertyId,od.ObjectName,ot.TypeName,op.ObjectPropertyName "
                + "FROM ObjectData od inner join ObjectTypes ot on od.ObjectTypeId=ot.ObjectTypeId "
                + "inner join ObjectProperties op on od.ObjectTypeId=op.ObjectTypeId "
                + "and od.ObjectPropertyId=op.ObjectPropertyId").ToList();
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