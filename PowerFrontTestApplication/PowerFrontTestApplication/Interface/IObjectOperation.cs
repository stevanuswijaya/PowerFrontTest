using PowerFrontTestApplication.Models;
using System.Collections.Generic;

namespace PowerFrontTestApplication.Interface
{
    public interface IObjectOperation
    {
        ObjectCompleteDescription GetSpecificObjectData(int objectTypeId, int objectPropertyId, int objectId);
        List<ObjectCompleteDescription> GetAllObjectData();
        void AddNewObjectData(ObjectCompleteDescription newData);
        void UpdateExistingObjectData(ObjectCompleteDescription updatedData);
        void DeleteExistingObjectData(ObjectCompleteDescription deletedData);
    }
}
