using TIOT_WEB.Service.ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class CommandQueueService
    {
        ServiceStatistics SC = new ServiceStatistics();
        public int PostCommand(int ObjectId, int CommandId)
        {
            var _object = new
            {
                CommandId = Convert.ToInt32(CommandId),
                ObjectId = Convert.ToInt32(ObjectId),
            };
            var url = "api/CommandQueue";
            string result = SC.PostCaller(url, _object);
            int GroupId = Convert.ToInt32(result);
            return GroupId;
        }
    }
}
