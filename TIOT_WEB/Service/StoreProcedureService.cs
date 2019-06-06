using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class StoreProcedureService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<StoreProcedureModel> GetStoreProcedure(int ObjectID, int CurrentID, int VoltageID)
        {
            var url = "api/StoreProcedure?ObjectID=" + ObjectID + "&CurrentID=" + CurrentID + "&VoltageID=" + VoltageID;
            string result = SC.Getcaller(url);
            if (result.Contains("[]"))
            {
                return null;
            }
            else
            {
                List<StoreProcedureModel> _StoreProcedure = JsonConvert.DeserializeObject<List<StoreProcedureModel>>(result);
                return _StoreProcedure;
            }
        }



        public List<sp_SMSLOGTEMPModel> GetSMSLOGBy(int ObjectID, int ObjectSensorID, DateTime StartTime, DateTime EndTime)
        {
            var url = "api/StoreProcedure?ObjectID=" + ObjectID + "&ObjectSensorID=" + ObjectSensorID + "&StartTime=" + StartTime + "&EndTime=" + EndTime;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<sp_SMSLOGTEMPModel> _StoreProcedure = JsonConvert.DeserializeObject<List<sp_SMSLOGTEMPModel>>(result);
                return _StoreProcedure;
                
            }
            else
            {
                return null;
            }
        }
    }
}