using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class SchedulingModel
    {
        public int SchedulingID { get; set; }
        public string DaysName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int Days { get; set; }
        public string Name { get; set; }
        public int ObjectSensorId { get; set; }
        public Nullable<bool> EnableOrDisable { get; set; }
        public Nullable<int> sensorID { get; set; }
    }
    public class HolidaySchedulingModel
    {
        public int HolidaysID { get; set; }
        public string Holidays { get; set; }
        public Nullable<System.DateTime> FullDate { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public Nullable<int> GroupID { get; set; }
    }
}