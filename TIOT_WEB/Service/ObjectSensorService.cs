using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class ObjectSensorService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<ObjectSensorModel> GetObjectSensors()
        {
            var url = "api/ObjectSensors";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectSensorModel> _objectsensors = JsonConvert.DeserializeObject<List<ObjectSensorModel>>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }


        }

        public ObjectSensorModel GetObjectSensorByID(int ObjSenID)
        {
            var url = "api/ObjectSensors/"+ ObjSenID;
            string result = SC.Getcaller(url);
            ObjectSensorModel _objectsensor = JsonConvert.DeserializeObject<ObjectSensorModel>(result);
            return _objectsensor;

        }

        public List<ObjectSensorModel> GetObjectSensorsByObjId(int ObjectId)
        {
            var url = "api/ObjectSensors?ObjectId=" + ObjectId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<ObjectSensorModel> _objectsensors = JsonConvert.DeserializeObject<List<ObjectSensorModel>>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }
        }        

        
        public List<SensorIDObjSenorName> GetObjectSensorsNameByObjId(int ObjectId)
        {
            var url = "api/ObjectSensors?ObjId=" + ObjectId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<SensorIDObjSenorName> _objectsensors = JsonConvert.DeserializeObject<List<SensorIDObjSenorName>>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }
        }

        public List<SensorIDSourceID> GetObjectSensorsByObjIdForEventConfig(int ObjectId, int SensorId)
        {
            var url = "api/ObjectSensors?ObjectId=" + ObjectId +"&SensorId="+SensorId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<SensorIDSourceID> _objectsensors = JsonConvert.DeserializeObject <List<SensorIDSourceID>>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }
        }

        public ObjNameObjSNameCateNameModel GetObjectSensorsByObjIdForNotify(int ObjectId, int SensorId)
        {
            var url = "api/ObjectSensors?Obj=" + ObjectId + "&Sen=" + SensorId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                ObjNameObjSNameCateNameModel _objectsensors = JsonConvert.DeserializeObject<ObjNameObjSNameCateNameModel>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }
        }

    
        public int PostObjectSensor(string sensorsId, string objectId, string name, bool enabled, bool smsalert, bool emailalert, string max, string min, string categoryID)
        {
            var _object = new
            {
                SensorID = Convert.ToInt32(sensorsId),
                ObjectID = Convert.ToInt32(objectId),
                Name = name,
                Enabled = Convert.ToBoolean(enabled),
                SMSAlert = Convert.ToBoolean(smsalert),
                EmailAlert = Convert.ToBoolean(emailalert),
                MAX = Convert.ToInt32(max),
                MIN = Convert.ToInt32(min),
                CategoryID = Convert.ToInt32(categoryID)
            };
            var url = "api/ObjectSensors";
            string result = SC.PostCaller(url, _object);
            int ObjectSensorId = Convert.ToInt32(result);
            return ObjectSensorId;
        }

        public bool PutObjectSensor(int ObjectSensorId, string sensorsId, string objectId, string name, bool enabled, bool smsalert, bool emailalert, string max, string min, string categoryID)
        {
            var _object = new
            {
                SensorID = Convert.ToInt32(sensorsId),
                ObjectID = Convert.ToInt32(objectId),
                Name = name,
                Enabled = Convert.ToBoolean(enabled),
                SMSAlert = Convert.ToBoolean(smsalert),
                EmailAlert = Convert.ToBoolean(emailalert),
                MAX = Convert.ToInt32(max),
                MIN = Convert.ToInt32(min),
                CategoryID = Convert.ToInt32(categoryID)
            };
            var url = "api/ObjectSensors/" + ObjectSensorId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteObjectSensor(int ObjectSensorId)
        {
            var url = "api/ObjectSensors/" + ObjectSensorId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}