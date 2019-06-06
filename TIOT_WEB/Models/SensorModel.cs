using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class SensorModel
    {
        public int SensorID { get; set; }
        public string SourceID { get; set; }
        public string SourceName { get; set; }
        public string Unit { get; set; }
        public bool EnableOrDisable { get; set; }
    }
}