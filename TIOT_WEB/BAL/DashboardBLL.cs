using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class DashboardBLL
    {
        DashboardDLL obj = new DashboardDLL();
        public List<ObjectInfo> getObjectInfo(int objectID)
        {
            return obj.getObjectInformation(objectID);
        }
        public List<ObjectRelayStatus> getObjectRelays(int objectID, bool RelayStatus)
        {
            return obj.getObjectRelays(objectID, RelayStatus);
        }
        public List<ObjectAnalog> getObjectAin(int objectID)
        {
            return obj.getObjectAnalog(objectID);
        }
        public List<ObjectDigital> getObjectDin(int objectID)
        {
            return obj.getObjectDigital(objectID);
        }
        public List<ObjectTemperature> getObjectTemp(int objectID)
        {
            return obj.getObjectTempOneWire(objectID);
        }

        public List<AttendanceModelDashboard> getAttendance(int clId)
        {
            return obj.getAttendance(clId);
        }
        public List<AttendanceIntegrationModel> getActiveAttendanceByObj(int objectId)
        {
            return obj.getActiveAttendanceByObj(objectId);
        }
        
        public int getRelayCommand(int objectID, int sensorID, string description)
        {
            return obj.getActiveCommand(objectID, sensorID, description);
        }
        public List<CustomNotification> getCustomNotificationbyClient(int clientID, DateTime startdate, DateTime endDate)
        {
            return obj.getCustomNotificationbyClient(clientID, startdate, endDate);
        }
        public List<CustomNotification> getCustomNotificationbyGroup(int groupID, DateTime startdate, DateTime endDate)
        {
            return obj.getCustomNotificationbyGroup(groupID, startdate, endDate);
        }

        public bool postCommandLogUser(CommandLogUserModel _object)
        { return obj.postCommandLogUser(_object); }

    }
}