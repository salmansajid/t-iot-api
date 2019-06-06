using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class EventConfigurationModel
    {
        public int EventConfigID { get; set; }
        public int ObjectID { get; set; }
        public long ObjectSensorID { get; set; }
        public string Name { get; set; }
        public double Min { get; set; }
        public double MAX { get; set; }
        public double a0 { get; set; }
        public double a1 { get; set; }
        public int Condition { get; set; }
        public string Contact { get; set; }
        public string Units { get; set; }
        public string Format { get; set; }
        public bool EnableOrDisable { get; set; }
    }

    public class EventConfigurationLocationModel
    {
        public long objectsensorId { get; set; }
        public string Location { get; set; }
        public int sensorId { get; set; }
    }
}