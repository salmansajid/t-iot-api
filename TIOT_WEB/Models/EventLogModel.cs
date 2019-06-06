using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class EventLogModel
    {
        public Nullable<int> SensorID { get; set; }
        public long EventLogID { get; set; }
        public Nullable<long> PMID { get; set; }
        public Nullable<long> PMDATAID { get; set; }
        public Nullable<int> ObjectID { get; set; }
        public Nullable<System.DateTime> DeviceDateTime { get; set; }
        public Nullable<long> ObjectSensorId { get; set; }
        public string VALUE { get; set; }
        public Nullable<bool> Valid { get; set; }
        public Nullable<System.DateTime> DateTimeStamp { get; set; }
    }
}