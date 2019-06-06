using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ControlingReportModel
    {
        public string SourceName { get; set; }
        public Nullable<int> ObjectID { get; set; }
        public string VALUE { get; set; }
        public Nullable<System.DateTime> DateTimeStamp { get; set; }
    }
}