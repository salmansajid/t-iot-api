using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class FeatureConfigurationModel
    {

        public int LoginFeatureID { get; set; }
        public int ClientID { get; set; }
        public int LoginID { get; set; }
        public int FeatureID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
    }
    public class ConfigurationModel
    {
        public int FeatureID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Class { get; set; }
        public bool EnableOrDisable { get; set; }

    }
}