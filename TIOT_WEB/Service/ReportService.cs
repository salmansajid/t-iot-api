using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class ReportService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<ReportModel> GetConsumptionReport(int objectId, string sensor)
        {
            var url = "api/SwitchesReport?ObjectID=" + objectId +"&sensor="+sensor;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ReportModel> rep = JsonConvert.DeserializeObject<List<ReportModel>>(result);
                return rep;
            }
            else
            {
                return null;
            }
        }
        public List<ControlingReportModel> GetControlingReport(int objectId)
        {
            var url = "api/OneDayReport?ObjectID=" + objectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ControlingReportModel> res = JsonConvert.DeserializeObject<List<ControlingReportModel>>(result);
                return res;
            }
            else
            {
                return null;
            }

        }
    }
}