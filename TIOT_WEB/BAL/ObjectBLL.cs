using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ObjectBLL
    {
        ObjectDLL obj = new ObjectDLL();
        public List<ObjectModelDLL> getObjectList(int clientID)
        {
            return obj.getObjectList(clientID);
        }

        public bool postObject(ObjectModelDLL _object)
        {
            return obj.postObject(_object);
        }
        public ObjectModelDLL getObjectByObjectID(int objectID)
        {
            return obj.getObjectByObjectID(objectID);
        }

        public bool disableObject(int objectID)
        {
            return obj.disableObject(objectID);
        }

        public bool objectExist(string imei)
        {
            return obj.objectExist(imei);
        }
    }
}