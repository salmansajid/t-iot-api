using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class CommandHistoryModel
    {
        public long CommandHistoryID { get; set; }
        public int ObjectId { get; set; }
        public string Name { get; set; }
        public System.DateTime DateTimeStamp { get; set; }
        public string EmailNo_1 { get; set; }
        public int CommandsQueueID { get; set; }
        public int CommandID { get; set; }
        public string Response { get; set; }
        public Nullable<int> AlertState { get; set; }
        public string DeviceName { get; set; }
    }
}