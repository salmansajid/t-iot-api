using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class CommonBLL
    {
        CommonDLL obj = new CommonDLL();
        public List<ClientIDName> getClientList()
        {
            return obj.getClients();
        }
        public List<LoginIDName> getLoginList(int clientID)
        {
            return obj.getLoginList(clientID);
        }
        public List<GroupIDName> getGroupList(int clientID)
        {
            return obj.getGroupByClient(clientID);
        }
        public List<ObjectIDName> getObjectListByClient(int clientID)
        {
            return obj.getObjectListByClient(clientID);
        }
        public List<ObjectListDashboard> getObjectList(int groupID)
        {
            return obj.getObjectsByGroup(groupID);
        }
        public ObjectStatus getObjectStatus(int objectID)
        {
            return obj.getObjectStatus(objectID);
        }
        public List<ObjectSensorIDName> getEventConfiguredSensor(int objectID)
        {
            return obj.getEventConfiguredSensor(objectID);
        }
        public List<ObjectSensorIDName> getObjectSensorDinList(int objectID)
        {
            return obj.getObjectSensorDinList(objectID);
        }
        public List<ObjectSensorIDName> getObjectSensorAinList(int objectID)
        {
            return obj.getObjectSensorAinList(objectID);
        }
        public List<ObjectSensorIDName> getObjectSensorTempValList(int objectID)
        {
            return obj.getObjectSensorTempValList(objectID);
        }
        public List<ObjectSensorIDName> getObjectSensorDinAndSerialSensor(int objectID)
        {
            return obj.getObjectSensorDinAndSerialSensor(objectID);
        }

        public List<ObjectSensorIDName> getBasicSensorByObject(int objectID, int sensorID)
        {
            return obj.getBasicSensorByObject(objectID,sensorID);
        }
        public List<ObjectSensorIDName> getRelaySensorByObject(int objectID, string sensorUnit)
        {
            return obj.getRelaySensorByObject(objectID, sensorUnit);
        }

        public List<CategoryIDName> getCategoryIDName()
        {
            return obj.getCategoryIDName();
        }
        public List<SensorIDSourceID> getSensorIDSourceID()
        {
            return obj.getSensorIDSourceID();
        }
        public int getGroupIDForUser(int loginID)
        {
            return obj.getGroupIDForUser(loginID);
        }

        

    }
}