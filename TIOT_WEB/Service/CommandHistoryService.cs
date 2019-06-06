using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class CommandHistoryService
    {

        ServiceStatistics SC = new ServiceStatistics();

        public List<CommandHistoryModel> GetNonAlerts()
        {
            var url = "api/CommandHistory";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<CommandHistoryModel> CHM = JsonConvert.DeserializeObject<List<CommandHistoryModel>>(result);
                return CHM;
            }
            else
            {
                return null;
            }
        }

        public bool PutAlertState(int CommandHistoryId)
        {
            var _object = new
            {
                AlertState = 1
            };
            var url = "api/CommandHistory/" + CommandHistoryId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }


    }

}