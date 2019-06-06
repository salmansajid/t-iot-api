using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class EventConfigurationService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<EventConfigurationModel> GetEventConfiguration()
        {
            var url = "api/EventConfiguration";
            string result = SC.Getcaller(url);
            if (result.Contains("[]"))
            {
                return null;
            }
            else
            {
                List<EventConfigurationModel> _EventConfigurationModel = JsonConvert.DeserializeObject<List<EventConfigurationModel>>(result);
                return _EventConfigurationModel;
            }

        }

        public List<EventConfigurationModel> GetEventConfigurationByObjectId(int ObjectId)
        {
            var url = "api/EventConfiguration?ObjectId=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                string _re = JsonConvert.SerializeObject(result);
                List<EventConfigurationModel> _EventConfigurationModel = JsonConvert.DeserializeObject<List<EventConfigurationModel>>(result);
                return _EventConfigurationModel;

            }
            else
            {
                return null;
            }
        }
        public EventConfigurationModel GetEventConfigurationById(int ECId)
        {
            var url = "api/EventConfiguration/" + ECId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                EventConfigurationModel _EventConfigurationModel = JsonConvert.DeserializeObject<EventConfigurationModel>(result);
                return _EventConfigurationModel;
            }
            else
            {
                return null;
            }
        }

        public List<EventConfigurationLocationModel> GetObjectSensorsByObjId(int ObjectId)
        {
            var url = "api/EventConfiguration?ObjId=" + ObjectId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<EventConfigurationLocationModel> _objectsensors = JsonConvert.DeserializeObject<List<EventConfigurationLocationModel>>(result);
                return _objectsensors;
            }

            else
            {
                return null;
            }

        }

        public int PostEventConfiguration(int ObjectSensorID, int ObjectID, double Min, double Max, int Condition, string Source, string Source2, string Source3, bool EnableOrDisable)
        {
            var _object = new
            {
                ObjectSensorID = ObjectSensorID,
                ObjectID = ObjectID,
                Min = Min,
                MAX = Max,
                Condition = Condition,                
                Source = Source,
                Source2 = Source2,
                Source3 = Source3,                
                EnableOrDisable = EnableOrDisable
            };
            var url = "api/EventConfiguration";
            string result = SC.PostCaller(url, _object);
            int ECId = Convert.ToInt32(result);
            return ECId;
        }

        public bool PutEventConfiguration(int ECId, int ObjectSensorID, int ObjectID, double Min, double Max, int Condition, string Source, string Source2, string Source3, bool EnableOrDisable)
        {
            var _object = new
            {
                ObjectSensorID = ObjectSensorID,
                ObjectID = ObjectID,
                Min = Min,
                MAX = Max,
                Condition = Condition,
                Source = Source,
                Source2 = Source2,
                Source3 = Source3,
                EnableOrDisable = EnableOrDisable
            };
            var url = "api/EventConfiguration/" + ECId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteEventConfiguration(int ECId)
        {
            var url = "api/EventConfiguration/" + ECId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}