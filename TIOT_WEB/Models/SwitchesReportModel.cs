using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class SwitchesReportDayModel
    {
        public string Name { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> sensorId { get; set; }
    }

    public class DTReportModel
    {
        public string Name { get; set; }
        public Nullable<double> Current { get; set; }
        public Nullable<double> Voltage { get; set; }
        //public string Current { get; set; }
        //public string Voltage { get; set; }
        public string Power { get; set; }

    }
    
    public class SwitchesReportConsumptionModel
    {
        public string Name { get; set; }
        public string Current { get; set; }
        public string Voltage { get; set; }
        public string Power { get; set; }
        public string Unit { get; set; }
        //public string Status { get; set; }
        public string TotalTime { get; set; }       
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        //public DateTime DateTimeStamp { get; set; }
    }

    public class SwitchesReportControllingModel
    {
        
        public string Name { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public string TotalTime { get; set; }
    }
}
