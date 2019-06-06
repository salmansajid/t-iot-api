using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class LoginFeatureService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<LoginFeatureModel> GetFeature()
        {
            var url = "api/LoginFeature";
            string result = SC.Getcaller(url);
            if(result!= null)
            {
                List<LoginFeatureModel> Features = JsonConvert.DeserializeObject<List<LoginFeatureModel>>(result);
                return Features;
            }
            {
                return null;
            }
        }

        public LoginFeatureModel GetFeature(int LoginFeatureID)
        {
            var url = "api/LoginFeature/"+ LoginFeatureID;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                LoginFeatureModel Feature = JsonConvert.DeserializeObject<LoginFeatureModel>(result);
                return Feature;
            }
            {
                return null;
            }
           
        }

        public List<LoginFeaturesUserEntity> GetFeatureByLoginId(int LoginID)
        {
            
            var url = "api/LoginFeature?LoginId=" + LoginID;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<LoginFeaturesUserEntity> Feature = JsonConvert.DeserializeObject<List<LoginFeaturesUserEntity>>(result);
                return Feature;
            }
            {
                return null;
            }

        }

        public List<LoginFeaturesUserEntity> Get_NA_LoginFeatureByLoginId(int LoginID)
        {

            var url = "api/LoginFeature?LogID=" + LoginID;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<LoginFeaturesUserEntity> Feature = JsonConvert.DeserializeObject<List<LoginFeaturesUserEntity>>(result);
                return Feature;
            }
            {
                return null;
            }

        }

        public int PostLoginFeture(int LoginID,int FeatureID, bool Enable)
        {
            var _object = new
            {
                LoginID = LoginID,
                FeatureID = FeatureID,
                Enable = Enable
            };
            var url = "api/LoginFeature";
            string result = SC.PostCaller(url, _object);
            int _FeatureID = Convert.ToInt32(result);
            return _FeatureID;
        }

        public bool PutFeature(int LoginFeatureID,bool Enable)
        {
            var _object = new
            {
                    Enable = Enable
            };
            var url = "api/LoginFeature/"+ LoginFeatureID;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteFeature(int LoginFeatureID)
        {
            var url = "api/LoginFeature/"+ LoginFeatureID;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}