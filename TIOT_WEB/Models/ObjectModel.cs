using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ObjectModelDLL
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double LAT { get; set; }
        public double LONG { get; set; }
        public long IMEI { get; set; }
        public long SimNumber { get; set; }
        public string FirmWareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Contact { get; set; }
        public string ObjectType { get; set; }
        public bool RelayStatus { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }


    public class ObjectModel
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double LAT { get; set; }
        public double LONG { get; set; }
        public long IMEI { get; set; }
        public long SimNumber { get; set; }
        public string FirmWareVersion { get; set; }
        public string HardwareVersion { get; set; }
        public bool EnableOrDisable { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public Nullable<System.DateTime> DeleteDateTime { get; set; }
        public string Contact { get; set; }
        public string ObjectType { get; set; }
        public bool RelayStatus { get; set; }
        public Nullable<int> TavlClient { get; set; }
        public Nullable<int> TavlGroup { get; set; }
        public string TavlIP { get; set; }
        public Nullable<bool> TavlStatus { get; set; }
        public Nullable<int> AttendanceClient { get; set; }
        public string AttendanceIP { get; set; }
        public Nullable<bool> AttendanceStatus { get; set; }
        public string SurveillanceIP { get; set; }
        public Nullable<bool> SurveillanceStatus { get; set; }
    }

    public class Objectdetails
    {
 
        public Nullable<int> ObjectID { get; set; }
        public long pmid { get; set; }
        public System.DateTime ReceivedDateTimeStamp { get; set; }
        public System.DateTime DeviceDateTime { get; set; }
        public string VALUE { get; set; }
        public string SensorName { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
    }
    public class ObjectdetailsNew
    {
        public Nullable<int> ObjectID { get; set; }
        public long pmid { get; set; }
        public System.DateTime ReceivedDateTimeStamp { get; set; }
        public System.DateTime DeviceDateTime { get; set; }
        public string VALUE { get; set; }
        public string SensorName { get; set; }
        public string SourceName { get; set; }
        public string Unit { get; set; }
        public string CategoryName { get; set; }
        public string Address { get; set; }
        public Nullable<int> SensorID { get; set; }
        public Nullable<int> TavlClient { get; set; }
        public Nullable<int> TavlGroup { get; set; }
        public Nullable<bool> TavlStatus { get; set; }
        public bool RelayStatus { get; set; }
    }
    public class ObjectdetailsByInfo
    {
        public string Category { get; set; }
        public string Area { get; set; }
        public string Voltage { get; set; }
        public string Current { get; set; }
        public string Power { get; set; }
        public string Status { get; set; }
        public string StatusClass { get; set; }
        public int ObjectID { get; set; }
        public Nullable<int> SensorID { get; set; }
        public string Fault { get; set; }   
    }
  
    public class AnalogInfo
    {
        public string AIN { get; set; }
        public string SensorName { get; set; }
        public string ImageUrl { get; set; }
       
    }
    public class DINInfo
    {
        public string DIN { get; set; }
        public string SensorName { get; set; }
        public string ImageUrl { get; set; }

    }
    public class OneWireTemp  {
        public string SensorName { get; set; }
        public string Value { get; set; }
        public string ImageUrl { get; set; }
    }
    public class contacts
    {
        public string Contact { get; set; }
    }
    public class TavlIntegrationModel
    {
        public int ObjectID { get; set; }
        public Nullable<int> TavlClient { get; set; }
        public Nullable<int> TavlGroup { get; set; }
        public string TavlIP { get; set; }
        public Nullable<bool> TavlStatus { get; set; }
    }
    public class AttendanceIntegrationModel
    {
        public int ObjectID { get; set; }
        public Nullable<int> AttendanceClient { get; set; }
        public string AttendanceIP { get; set; }
        public Nullable<bool> AttendanceStatus { get; set; }

    }
    public class SurveillanceIntegrationModel
    {
        public int ObjectID { get; set; }
        public string SurveillanceIP { get; set; }
        public Nullable<bool> SurveillanceStatus { get; set; }

    }

}