using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class SensorService
    {
        ServiceStatistics SC = new ServiceStatistics();
        
        public List<SensorModel> GetSensors()
        {
            var url = "api/Sensor";
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<SensorModel> _sensors = JsonConvert.DeserializeObject<List<SensorModel>>(result);
                return _sensors;
            }
            else
            {
                return null;
            }
        }

        public SensorModel GetSensor(int SensorID)
        {
            var url = "api/Sensor/" + SensorID;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                SensorModel _sensor = JsonConvert.DeserializeObject<SensorModel>(result);
                return _sensor;
            }
            else
            {
                return null;
            }

        }

        public List<SensorModel> GetSensorByObjectId(int ObjectId)
        {
            var url = "api/Sensor?ObjectId=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SensorModel> _sensor = JsonConvert.DeserializeObject<List<SensorModel>>(result);
                return _sensor;
            }
            else
            {
                return null;
            }

        }

        public List<SensorModel> GetSensorByObjectIdForEventConfig(int ObjectId)
        {
            var url = "api/Sensor?_objectID=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SensorModel> _sensor = JsonConvert.DeserializeObject<List<SensorModel>>(result);
                return _sensor;
            }
            else
            {
                return null;
            }

        }

        //public List<ObjectSensorModel> GetObjectSensorsByObjId(int ObjectId)
        //{
        //    var url = "api/ObjectSensors?ObjectId=" + ObjectId;
        //    string result = SC.Getcaller(url);
        //    List<ObjectSensorModel> _objectsensors = JsonConvert.DeserializeObject<List<ObjectSensorModel>>(result);
        //    return _objectsensors;
        //}

        public int PostSensor(string SourceID, string SourceName, string Unit , bool EnableOrDisable )
        {
            var _object = new
            {
                    SourceID = SourceID,
                    SourceName = SourceName,
                    Unit = Unit,
                    EnableOrDisable =EnableOrDisable
            };
            var url = "api/Sensor";
            string result = SC.PostCaller(url, _object);
            int SensorId = Convert.ToInt32(result);
            return SensorId;
        }

        public bool PutSensor(int SensorId, string SourceID, string SourceName, string Unit, bool EnableOrDisable)
        {
            var _object = new
            {
                SourceID = SourceID,
                SourceName = SourceName,
                Unit = Unit,
                EnableOrDisable = EnableOrDisable
            };
            var url = "api/Sensor/" + SensorId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteSensor(int SensorId)
        {
            var url = "api/Sensor/" + SensorId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}