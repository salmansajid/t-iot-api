using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class TAVLModel
    {
        public string gpsSortableTime { get; set; }
        public string number { get; set; }
        public string unitId { get; set; }
        public string gpsTime { get; set; }
        public string landMarksValue { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string angle { get; set; }
        public string speed { get; set; }
        public string _speed { get; set; }
        public string satellites { get; set; }
        public string numberStr { get; set; }
        public string clientName { get; set; }
        public string groupName { get; set; }
        public string gpsFormattedTime { get; set; }

        public string messageId { get; set; }
        public string date { get; set; }
        public string time { get; set; }

        public string updateColorStr { get; set; }
        public string engineStatusStr { get; set; }
        public string objectStatusStr { get; set; }

        public string isEngineOn { get; set; }

        public string comment { get; set; }

        public string imageCode { get; set; }
        public string simCardId { get; set; }
        public string imei { get; set; }
        public string phoneNumber { get; set; }
        public string id { get; set; }
        public string landMark { get; set; }
        public string altitude { get; set; }
        public string objectId { get; set; }
    }

    public class AttendanceModel
    {
        public int Object_ID { get; set; }
        public int Client_ID { get; set; }
        public string Name { get; set; }
        public string Department_Name { get; set; }
        public string Designation_Name { get; set; }
        public string Emp_image { get; set; }
        public Nullable<DateTime> Clock_Time { get; set; }
        public Nullable<DateTime> Clock_In { get; set; }
        
    }
}
