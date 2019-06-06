using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
 
        public class ClientIDName
        {
            public int ClientID { get; set; }
            public string Name { get; set; }
        }
        public class GroupIDName
        {
            public int GroupID { get; set; }
            public string Name { get; set; }
        }
        public class ObjectIDName
        {
            public int ObjectID { get; set; }
            public string Name { get; set; }
        }
        public class LoginIDName
        {
            public int LoginID { get; set; }
            public string Name { get; set; }
        }
        public class ObjectListDashboard
        {
            public int ObjectID { get; set; }
            public string Name { get; set; }
            public DateTime LastRecordReceived { get; set; }
            public bool RelayStatus { get; set; }
            public string TavlIP { get; set; }
            public bool TavlStatus { get; set; }
            public string AttendanceClient { get; set; }
            public string AttendanceIP { get; set; }
            public bool AttendanceStatus { get; set; }
            public string StatusClass { get; set; }
        }
        public class ObjectStatus
        {
            public string Name { get; set; }
            public string SimNumber { get; set; }
            public DateTime LastRecordReceived { get; set; }
            public bool RelayStatus { get; set; }

        }
        public class ObjectSensorIDName
        {
            public int ObjectSensorID { get; set; }
            public string Name { get; set; }
        }
        public class SensorIDSourceID
        {
            public int SensorID { get; set; }
            public string SourceID { get; set; }
        }
        public class CategoryIDName
        {
            public int CategoryID { get; set; }
            public string Category { get; set; }
        }
    
   
}