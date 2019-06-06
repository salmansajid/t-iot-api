using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class LoginFeatureBLL
    {
        LoginFeatureDLL obj = new LoginFeatureDLL();
        
        public List<FeatureIDName> getNAFeatureByLogin(int loginID)
        {return obj.getNAFeatureByLogin(loginID);}
        public List<LoginFeatureModel> getFeatureByLogin(int loginID)
        { return obj.getFeatureByLogin(loginID); }
        public bool postLoginFeature(LoginFeatureModel _object)
        {return obj.postLoginFeature(_object);}

        public bool E_D_LoginFeature(int loginFeatureID, bool enable)
        { return obj.E_D_LoginFeature(loginFeatureID, enable); }
        public bool deleteLoginFeature(int loginFeatureID)
        { return obj.deleteLoginFeature(loginFeatureID); }
    }
}