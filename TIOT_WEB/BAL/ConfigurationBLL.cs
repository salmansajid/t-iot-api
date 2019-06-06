using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ConfigurationBLL
    {
        ConfigurationDLL obj = new ConfigurationDLL();

        public List<ConfigurationModel> getConfigurationList(int parentID)
        {
            return obj.getConfigurationList(parentID);
        }


        public List<FeatureConfigurationModel> getFeatureListByLogin(int LoginID, int parentID)
        {
            return obj.getFeatureListByLogin(LoginID, parentID);
        }

        #region Feature
        public List<ConfigurationModel> getFeatureList(int parentID)
        { return obj.getFeatureList(parentID); }

        public bool putFeature(int featureID, string name, string cssclass, bool enable)
        { return obj.putFeature(featureID, name, cssclass, enable); }

        public bool disableFeature(int featureID)
        { return obj.disableFeature(featureID); }

        public bool featureExist(string name)

        { return obj.featureExist(name) ; }
        #endregion
    }
}