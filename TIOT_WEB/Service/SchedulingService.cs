using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class SchedulingService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public int PostScheduling(int ObjectId, string ScheduleTime, int CommandID, bool status, int DayID)
        {
            var _object = new
            {
                ObjectID = ObjectId,
                ScheduleTime = ScheduleTime,
                CommandID = CommandID,
                EnableOrDisable = status,
                Days = DayID
            };
            var url = "api/Scheduling";
            string result = SC.PostCaller(url, _object);
            int SchedulingID = Convert.ToInt32(result);
            return SchedulingID;
        }

        public bool PutScheduling(int scheduleId, int ObjectId, string ScheduleTime, int CommandID)
        {
            var _object = new
            {
                ObjectId = ObjectId,
                ScheduleTime = ScheduleTime,
                CommandID = CommandID
            };
            var url = "api/Scheduling/" + scheduleId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public List<SchedulingModel> GetScheduling()
        {
            var url = "api/Scheduling";
            string result = SC.Getcaller(url);
            if (result != null)
            {

                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        public SchedulingModel GetSchedulingById(int ScheduleID)
        {
            var url = "api/EquipmentScheduling/" + ScheduleID;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                SchedulingModel scheduling = JsonConvert.DeserializeObject<SchedulingModel>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        //GetSchedulingByObjectId
        public List<SchedulingModel> GetSchedulingByObjectId(int objectId, int DayID)
        {
            var url = "api/EquipmentScheduling?ObjectId=" + objectId + "&DayId=" + DayID;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        public List<SchedulingModel> GetSchedulingByObject_Day_OBSID(int objectId, int DayID, int ObjectSensorId)
        {
            var url = "api/EquipmentScheduling?ObjectId=" + objectId + "&DayId=" + DayID + "&ObjectSensorId=" + ObjectSensorId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        public List<SchedulingModel> GetSchedulingByObjectAndOBSIDweekly(int objectId, int ObjectSensorId)
        {
            var url = "api/EquipmentScheduling?ObjId=" + objectId + "&ObjSensorId=" + ObjectSensorId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteScheduling(int SchedulingId)
        {
            var url = "api/EquipmentScheduling/" + SchedulingId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }

        public List<SchedulingModel> GetSchedulingBySensorId(int ObjectId, int DayID, int SensorId)
        {
            var url = "api/Scheduling?ObjID=" + ObjectId + "&dayID=" + DayID + "&sensorId=" + SensorId;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }
        public List<SchedulingModel> GetSchedulingByDayID(int ObjectId, int DayID)
        {
            var url = "api/Scheduling?ObjID=" + ObjectId + "&dayID=" + DayID;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                List<SchedulingModel> scheduling = JsonConvert.DeserializeObject<List<SchedulingModel>>(result);
                return scheduling;
            }
            else
            {
                return null;
            }
        }

        public int PostEquipmentScheduling(int ObjectId, string StartTime, string EndTime, int Days, int ObjectSensorId, bool EnableOrDisable)
        {
            var _object = new
            {
                ObjectID = ObjectId,
                StartTime = StartTime,
                EndTime = EndTime,
                Days = Days,
                ObjectSensorId = ObjectSensorId,
                EnableOrDisable = EnableOrDisable
            };
            var url = "api/EquipmentScheduling";
            string result = SC.PostCaller(url, _object);
            int SchedulingID = Convert.ToInt32(result);
            return SchedulingID;
        }

        public bool PutEquipmentScheduling(int scheduleId, int ObjectId, string StartTime, string EndTime, int Days, int ObjectSensorId, bool EnableOrDisable)
        {
            var _object = new
            {
                ObjectId = ObjectId,
                StartTime = StartTime,
                EndTime = EndTime,
                Days = Days,
                ObjectSensorId = ObjectSensorId,
                EnableOrDisable = EnableOrDisable
            };
            var url = "api/EquipmentScheduling/" + scheduleId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

    }
}