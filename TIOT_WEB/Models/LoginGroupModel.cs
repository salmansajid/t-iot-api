using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class LoginGroupModel
    {
        public int LoginGroupID { get; set; }
        public string GroupName { get; set; }
        public string LoginName { get; set; }
        public int GroupID { get; set; }
        public int LoginID { get; set; }
    }
}