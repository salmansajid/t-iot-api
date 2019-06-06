using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.Service
{

    public class FeatureService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<FeatureModel> GetFeature()
        {
            var url = "api/Feature";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<FeatureModel> Features = JsonConvert.DeserializeObject<List<FeatureModel>>(result);
                return Features;
            }
            else
            {
                return null;
            }

        }

        public FeatureModel GetFeature(int FeatureID)
        {
            var url = "api/Feature?featureId="+ FeatureID;
            string result = SC.Getcaller(url);
            FeatureModel Feature = JsonConvert.DeserializeObject<FeatureModel>(result);
            return Feature;
        }
        public List<FeatureModel> GetFeatureByLoginId(int LoginID)
        {
            var url = "api/Feature?LoginId=" + LoginID;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<FeatureModel> Feature = JsonConvert.DeserializeObject<List<FeatureModel>>(result);
                return Feature;
            }
            {
                return null;
            }

        }
        public int PostFeature(string Name, string Description)
        {
            var _object = new
                {

                    Name = Name,
                    Description = Description
                };
            var url = "api/Feature";
            string result = SC.PostCaller(url, _object);
            int FeatureID = Convert.ToInt32(result);
            return FeatureID;
        }

        public bool PutFeature(int FeatureID, string Name, string Description)
        {
            var _object = new
            {
                    Name = Name,
                    Description = Description
            };
            var url = "api/Feature/"+ FeatureID;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteFeature(int FeatureID)
        {
            var url = "api/Feature/"+ FeatureID;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}