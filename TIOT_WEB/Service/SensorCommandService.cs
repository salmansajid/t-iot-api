using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class SensorCommandService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<SensorCommandModel> GetSensorCommand(int SensorId)
        {
            var url = "api/SensorCommand?SensorId=" + SensorId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SensorCommandModel> Features = JsonConvert.DeserializeObject<List<SensorCommandModel>>(result);
                return Features;
            }
            else
            {
                return null;
            }

        }


        public List<SensorCommandModel> GetSensorCommandbyCmdId(int CommandId)
        {            
            var url = "api/SensorCommand?CommandId=" + CommandId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<SensorCommandModel> Features = JsonConvert.DeserializeObject<List<SensorCommandModel>>(result);
                return Features;
            }
            else
            {
                return null;
            }

        }


        
    }
}