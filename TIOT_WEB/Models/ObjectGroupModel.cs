using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ObjectGroupModel
    {
        public int ObjectGroupID { get; set; }
        public string ObjectName { get; set; }
        public string GroupName { get; set; }
        public int ObjectID { get; set; }
        public int GroupID { get; set; }
    }
}