using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;

namespace TIOT_WEB.Service
{
    public class AttendanceService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<AttendanceModel> GetAttendanceList(int ClientId, string IP)
        {
            try
            {
                string url = "api/DailyAttendance/" + ClientId;
                string result = SC.GetcallerIPChange(url, IP);
                if (result.Contains("[]"))
                {
                    return null;
                }
                else
                {
                    List<AttendanceModel> Response = JsonConvert.DeserializeObject<List<AttendanceModel>>(result);
                    return Response;
                }
            }
            catch(Exception){}
            return null;
        }
    }
}