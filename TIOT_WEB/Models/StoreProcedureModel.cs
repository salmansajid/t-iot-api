using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class StoreProcedureModel
    {
        public string SourceName { get; set; }
        public int SEN { get; set; }
        public string value { get; set; }
        public System.DateTime ReceivedDateTimeStamp { get; set; }
    }


    public class sp_SMSLOGTEMPModel
    {
        public string Device { get; set; }
        public string Location { get; set; }
        public Nullable<double> Sensor { get; set; }
        public Nullable<System.DateTime> Variatoin_Time { get; set; }
        public Nullable<int> SensorID { get; set; }
        public string SensorName { get; set; }
    }
}
