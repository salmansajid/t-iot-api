using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class FeatureModel
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoleID { get; set; }
        public int LoginID { get; set; }
        public string Link { get; set; }
        public int ParentId { get; set; }
        public string Class { get; set; }
    }
    public class FeatureIDName
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }
    }
}