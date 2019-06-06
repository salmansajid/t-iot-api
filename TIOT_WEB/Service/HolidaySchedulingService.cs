using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.Service
{
    public class HolidaySchedulingService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public int PostHolidayScheduling(string Holidays, string FullDate, bool Enabled, int GroupID)
        {
            var _object = new
            {
                Holidays = Holidays,
                FullDate = FullDate,
                Enabled = Enabled,
                GroupID = GroupID
            };
            var url = "api/HolidayScheduling";
            string result = SC.PostCaller(url, _object);
            int HolidayID = Convert.ToInt32(result);
            return HolidayID;
        }

        public bool PutHolidayScheduling(int HolidayID, bool Enabled)
        {
            var _object = new
            {
                Enabled = Enabled
            };
            var url = "api/HolidayScheduling/" + HolidayID;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public List<HolidaySchedulingModel> GetHolidayScheduling()
        {
            var url = "api/HolidayScheduling";
            string result = SC.Getcaller(url);
            if (result != null)
            {

                List<HolidaySchedulingModel> HolidayScheduling = JsonConvert.DeserializeObject<List<HolidaySchedulingModel>>(result);
                return HolidayScheduling;
            }
            else
            {
                return null;
            }
        }

        public HolidaySchedulingModel GetHolidaySchedulingById(int HolidayID)
        {
            var url = "api/HolidayScheduling/" + HolidayID;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                HolidaySchedulingModel HolidayScheduling = JsonConvert.DeserializeObject<HolidaySchedulingModel>(result);
                return HolidayScheduling;
            }
            else
            {
                return null;
            }
        }

        //GetSchedulingByObjectId
        public List<HolidaySchedulingModel> GetHolidaySchedulingByGroupId(int GroupId)
        {
            var url = "api/HolidayScheduling?GroupID=" + GroupId;
            string result = SC.Getcaller(url);
            if (result != null)
            {

                List<HolidaySchedulingModel> HolidayScheduling = JsonConvert.DeserializeObject<List<HolidaySchedulingModel>>(result);
                return HolidayScheduling;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteHolidayScheduling(int HolidayID)
        {
            var url = "api/HolidayScheduling/" + HolidayID;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}