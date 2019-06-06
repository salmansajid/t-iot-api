using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class LoginGroupBLL
    {
        LoginGroupDLL obj = new LoginGroupDLL();
        public List<LoginGroupModel> getLoginGroupByLogin(int loginID)
        {
            return obj.getLoginGroupByLogin(loginID);
        }

        public bool postLoginGroup(LoginGroupModel _object)
        {
            return obj.postLoginGroup(_object);
        }

        public LoginGroupModel getLoginGroupByID(int loginGroupID)
        {
            return obj.getLoginGroupByID(loginGroupID);
        }


        public bool deleteLoginGroup(int logingroupID)
        {
            return obj.deleteLoginGroup(logingroupID);
        }
    }
}