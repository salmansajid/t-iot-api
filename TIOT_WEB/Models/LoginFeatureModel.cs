using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class LoginFeatureModel
    {
        public int LoginFeatureID { get; set; }
        public int LoginID { get; set; }
        public int FeatureID { get; set; }
        public bool Enable { get; set; }
        public string LoginName { get; set; }
        public string FeatureName { get; set; }
    }

    public class LoginFeaturesUserEntity
    {
        public int LoginFeatureID { get; set; }
        public int LoginID { get; set; }
        public int FeatureID { get; set; }
        public string User { get; set; }
        public Nullable<bool> Enable { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
    }
}