using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ObjectSensorBLL
    {
        ObjectSensorDLL obj = new ObjectSensorDLL();

        public List<ObjectSensorModel> getObjectSensorList(int objectID)
        { return obj.getObjectSensorList(objectID);}

        public List<SensorIDSourceID> getNASensorListByObject(int objectID)
        { return obj.getNASensorListByObject(objectID); }        


        public bool postObjectSensor(ObjectSensorModelDLL _object)
        {return obj.postObjectSensor(_object);}

        public ObjectSensorModelDLL getObjectSensorByID(int objectSensorID)
        { return obj.getObjectSensorByID(objectSensorID); }
        
        public bool disableObjectSensor(int objectSensorID)
        { return obj.disableObjectSensor(objectSensorID); }

        public bool objectSensorExist(int objectID, int sensorID)
        { return obj.objectSensorExist(objectID, sensorID); }
    }
}