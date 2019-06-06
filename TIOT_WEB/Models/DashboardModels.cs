using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class DashboardModels
    {
    }
    public class ObjectInfo
    {
        public string ObjectName { get; set; }
        public string SimNumber { get; set; }
        public string Sensor { get; set; }
        public double Value { get; set; }
        public int SensorId { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public bool RelayStatus { get; set; }
        
    }
    public class ObjectRelayStatus
    {
        public int ObjectID { get; set; }
        public string Device { get; set; }
        public string Name { get; set; }
        public double Current { get; set; }
        public double Voltage { get; set; }
        public double Power { get; set; }
        public bool Fault { get; set; }
        public string Status { get; set; }
        public DateTime DateTimeStamp{ get; set; }
        public string Category { get; set; }
        public int SensorId { get; set; }
        public string StatusClass { get; set; }
        public string StatusIOTClass { get; set; }
    }
    public class ObjectRelayCurrent
    {
        public double Current { get; set; }
    }
    public class ObjectRelayVoltage
    {
        public double Voltage { get; set; }
    }

    public class ObjectAnalog
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int SensorId { get; set; }
        public string Category { get; set; }
    }
    public class ObjectDigital
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int SensorId { get; set; }
        public string Category { get; set; }
        public string CategoryClass { get; set; }
    }
    public class ObjectTemperature
    {
        public int ObjectID { get; set; }
        public string Name { get; set; }
        public double Value { get; set; }
        public int SensorId { get; set; }
        public string Category { get; set; }
    }
    public class CustomNotification
    {
        public int NotificationID { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
        public int ClientID { get; set; }
        public int ObjectID { get; set; }
        public string CategoryName { get; set; }
        public bool State { get; set; }
    }

    public class ObjectRelaysByGroup
    {
        public string Device { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public int ObjectID { get; set; }
        public int RelayStatus { get; set; }
        public string StatusClass { get; set; }
    }

    public class ObjectRelaysByGroupDT
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Category { get; set; }
        public string StatusClass { get; set; }
    }

    public class AttendanceModelDashboard
    {
        public int Emp_ID { get; set; }
        public string EmpName { get; set; }
        public string Emp_Image { get; set; }
        public string DeptName { get; set; }
        public DateTime ClockTime { get; set; }
        public string Designation { get; set; }
        public string cssClass { get; set; }
        public string ClockTimeStr { get; set; }
    }


    public class CommandLogUserModel
    {
        public int UserID { get; set; }
        public int ObjectID { get; set; }
        public int SensorID { get; set; }
        public int CommandID { get; set; }        
    }
}
