using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class EventLogService
    {
        ServiceStatistics SC = new ServiceStatistics();


        public EventLogModel GetFeulByObject(int ObjectId)
        {
            var url = "api/EventLog?ObjectId=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                EventLogModel _event = JsonConvert.DeserializeObject<EventLogModel>(result);
                return _event;
            }
            else
            {
                return null;
            }

        }
  

     
    }
}
 