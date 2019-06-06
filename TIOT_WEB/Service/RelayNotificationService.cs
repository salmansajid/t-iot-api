using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class RelayNotificationService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<RelayNotificationModel> GetByClientId(int ClientId, DateTime LastTime)
        {
            var url = "api/RelayNotification?ClientID=" + ClientId + "&lastTime=" + LastTime;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<RelayNotificationModel> NotificationModel = JsonConvert.DeserializeObject<List<RelayNotificationModel>>(result);
                return NotificationModel;
            }
            else
            {
                return null;
            }

        }

        public IEnumerable<RelayNotificationModel> GetByGroupId(int GroupId, DateTime LastTime)
        {
            var url = "api/RelayNotification?GroupID=" + GroupId + "&lastTime=" + LastTime;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<RelayNotificationModel> NotificationModel = JsonConvert.DeserializeObject<List<RelayNotificationModel>>(result);
                return NotificationModel;
            }
            else
            {
                return null;
            }

        }
    }
}