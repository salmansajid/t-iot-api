using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ObjectMaintenanceBLL
    {
        ObjectMaintenanceDLL obj = new ObjectMaintenanceDLL();

        public List<ObjectMaintenanceModel> getObjectMaintenanceByObject(int objectID)
        {return obj.getObjectMaintenanceByObject(objectID);}

        public bool postObjectMaintenance(ObjectMaintenanceModel _object)
        { return obj.postObjectMaintenance(_object); }

        public ObjectMaintenanceModel getObjectMaintenanceByID(int mainId)
        { return obj.getObjectMaintenanceByID(mainId); }

        public bool deleteObjectMaintenance(int mainId)
        { return obj.deleteObjectMaintenance(mainId); }
    }
}