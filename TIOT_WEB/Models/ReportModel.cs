using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ReportModel
    {
        public string Name { get; set; }
        public Nullable<double> Value { get; set; }
        public Nullable<int> sensorId { get; set; }
    }
    

    public class CurrentDTReportModel
    {
        public string Name { get; set; }
        public Nullable<double> Current { get; set; }
        public Nullable<double> Voltage { get; set; }
        public Nullable<double> Power { get; set; }
        public Nullable<int> sensorId { get; set; }
    }

    #region Sensor VariationModel
    public class SensorVariationModel
    {
        public string Name { get; set; }
        public Nullable<double> Value { get; set; }
        public string Condition { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
    #endregion  

    #region  DigitalInputModel
    public class DigitalInputModel
    {
        public int ObjectSensorID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TotalTime { get; set; }
    }
    #endregion  

    #region  AnalogModel
    public class AnalogModel
    {
        public int ObjectSensorID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
    #endregion  
    #region  TemperatureValueModel
    public class TemperatureValueModel
    {
        public int ObjectSensorID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
    #endregion  

    #region  IndividualSensorModel
    public class IndividualSensorModel
    {
        public int ObjectSensorID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public DateTime DateTimeStamp { get; set; }
    }
    #endregion  

}