using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;
using Newtonsoft.Json;
using TIOT_WEB.Service.ServiceCaller;


namespace TIOT_WEB.Service
{
    public class LoginService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<LoginModel> GetLogin()
        {
            var url = "api/login";
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<LoginModel> lg = JsonConvert.DeserializeObject<List<LoginModel>>(result);
                return lg;
            }
            else
            {
                return null;
            }
        }


        public List<LFLoginModel> GetNonAdminLogin()
        {
            var url = "api/Login?admins={admins}";
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<LFLoginModel> lg = JsonConvert.DeserializeObject<List<LFLoginModel>>(result);
                return lg;
            }
            else
            {
                return null;
            }
        }


        public LoginModel GetLogin(int LoginId)
        {
            var url = "api/login/"+ LoginId;
            string result = SC.Getcaller(url);
            LoginModel lg = JsonConvert.DeserializeObject<LoginModel>(result);
            return lg;
        }

        public LoginModel GetLogin(string username, string password)
        {
            try
            {
                var url = "api/login?User=" + username + "&Password=" + password;
                string result = SC.Getcaller(url);
                LoginModel lg = JsonConvert.DeserializeObject<LoginModel>(result);
                return lg;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return null;
            }
            
        }

        public LoginModelForUser GetLoginByCode(string code, string username, string password)
        {
            LoginModelForUser lg = new LoginModelForUser();
         var url = "api/Login?Code=" + code + "&User=" + username + "&Password=" + password;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                string content = result.Replace("[", "").Replace("]", "");
                lg = JsonConvert.DeserializeObject<LoginModelForUser>(content);
                return lg;
            }
            //else
            //{
            //    lg = JsonConvert.DeserializeObject<LoginModelForUser>(result);
            //}
            else
            {
                return null;
            }
        
            
        }

        public int PostLogin(string clientId, string roleId, string username, string comments, string password)
        {
            var _object = new
            {
                ClientID = Convert.ToInt32(clientId),
                RoleID = Convert.ToInt32(roleId),
                User = username,
                Comment = comments,
                Password = password
            };
            var url = "api/Login";
            string result = SC.PostCaller(url, _object);
            int PostId = Convert.ToInt32(result);
            return PostId;
        }
        public bool PutLogin(int loginId, string clientId, string roleId, string username, string comments, string password)
        {
            var _object = new
            {
                ClientID = Convert.ToInt32(clientId),
                RoleID = Convert.ToInt32(roleId),
                User = username,
                Comment = comments,
                Password = password
            };
            var url = "api/Login/"+ loginId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }


        public bool DeleteLogin(int LoginId)
        {
            var url = "api/Login/" + LoginId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }

}