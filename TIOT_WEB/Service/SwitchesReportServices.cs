using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;

namespace TIOT_WEB.Service
{
   

    public class SwitchesReportServices
    {
        ServiceStatistics SC = new ServiceStatistics();
        //public List<SwitchesReportModel> GetSwitchesReportObject(int ObjectId, DateTime StartDateTime, DateTime EndDateTime)
        //{
        //    var url = "api/SwitchesReport?ObjectId="+ObjectId+"&StartDateTime="+StartDateTime+"&EndDateTime="+EndDateTime;
        //    string result = SC.Getcaller(url);
        //    if (result != null)
        //    {
                
        //        List<SwitchesReportModel> _sensor = JsonConvert.DeserializeObject<List<SwitchesReportModel>>(result);
        //        List<SwitchesReportModel> _sensor2 = _sensor.OrderBy(x => x.LocationID).ToList();
                
        //        return _sensor2;
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //}
        public List<SwitchesReportControllingModel> GetCurrentdateSwitchesReportControling(int ObjectId)
        {
            var url = "api/SwitchesReport?ObjectID=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SwitchesReportControllingModel> _sensor = JsonConvert.DeserializeObject<List<SwitchesReportControllingModel>>(result);
                return _sensor;
            }
            else
            {
                return null;
            }

        }
    }
}