using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class LoginBLL
    {
        LoginDLL obj = new LoginDLL();

        public LoginModelDLL getActiveLogin(string username, string status)
        {return obj.getActiveLogin(username, status);}

        public List<GetLoginModel> getLoginByClient(int clientID)
        { return obj.getLoginByClient(clientID); }

        public PostLoginModel getLoginByLoginID(int LoginID)
        {return obj.getLoginByLoginID(LoginID); }
 
        public int getActiveCode(string code)
        {return obj.getActiveCode(code);}

        public bool postLogin(PostLoginModel _object)
        {return obj.postLogin(_object);}

        public bool disableLogin(int loginID)
        {return obj.disableLogin(loginID);}

        public bool usernameExist(string username)
        { return obj.usernameExist(username); }

        
        
    }
}