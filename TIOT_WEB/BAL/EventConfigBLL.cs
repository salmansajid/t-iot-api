using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class EventConfigBLL
    {
        EventConfigDLL obj = new EventConfigDLL();
        public List<EventConfigurationModel> getEventConfigByObject(int objectID)
        {return obj.getEventConfigByObject(objectID);}
        public bool postEventConfig(EventConfigurationModel model)
        { return obj.postEventConfig(model); }
        public EventConfigurationModel getEventConfigByID(int eventConfigID)
        { return obj.getEventConfigByID(eventConfigID); }
        
    }
}