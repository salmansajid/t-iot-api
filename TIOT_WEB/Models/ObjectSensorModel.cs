using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ObjectSensorModel
    {
        public long ObjectSensorId { get; set; }
        public string Name { get; set; }
        public string SourceID { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public double A1 { get; set; }
        public double A0 { get; set; }
        public string Contact { get; set; }
        public string Category { get; set; }
        public bool SMSAlert { get; set; }
    }

    public class ObjectSensorModelDLL
    {
        public long ObjectSensorId { get; set; }
        public int SensorID { get; set; }
        public int ObjectID { get; set; }
        public string Contact { get; set; }
        public string Name { get; set; }
        public bool SMSAlert { get; set; }
        public bool EmailAlert { get; set; }
        public int Max { get; set; }
        public int Min { get; set; }
        public double A1 { get; set; }
        public double A0 { get; set; }
        public int CategoryID { get; set; }
    }

  
    public class SensorIDObjSenorName
    {
        public int SensorID { get; set; }
        public string Name { get; set; }
    }

    public class ObjNameObjSNameCateNameModel
    {

        public string ObjectName { get; set; }
        public string ObjectSensorName { get; set; }
        public string CategoryName { get; set; }
    }

   
}