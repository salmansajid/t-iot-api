using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ObjectGroupBLL
    {
        ObjectGroupDLL obj = new ObjectGroupDLL();

        public List<ObjectGroupModel> getObjectGroupByObject(int objectID)
        {return obj.getObjectGroupByObject(objectID);}

        public ObjectGroupModel getObjectGroupByID(int objectGroupID)
        {return obj.getObjectGroupByID(objectGroupID);}

        public bool postObjectGroup(ObjectGroupModel _object)
        { return obj.postObjectGroup(_object); }

        public bool deleteObjectGroup(int objectgroupID)
        { return obj.deleteObjectGroup(objectgroupID); }
    }
}