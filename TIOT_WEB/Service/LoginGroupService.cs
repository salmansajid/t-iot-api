using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class LoginGroupService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<LoginGroupModel> GetLoginGroup()
        {
            var url = "api/LoginGroup";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<LoginGroupModel> LoginGroups = JsonConvert.DeserializeObject<List<LoginGroupModel>>(result);
                return LoginGroups;
            }
            else
            {
                return null;
            }

        }

        public LoginGroupModel GetLoginGroup(int LoginGroupId)
        {
            var url = "api/LoginGroup/" + LoginGroupId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                LoginGroupModel _client = JsonConvert.DeserializeObject<LoginGroupModel>(result);
                return _client;
            }
            else
            {
                return null;
            }
        }
        public LoginGroupModel GetLoginGroupByLogin(int LoginId)
        {
            var url = "api/LoginGroup?LoginId=" + LoginId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                if (result.Contains("["))
                {
                    string rpl = result.Replace("[", "").Replace("]", "");
                    LoginGroupModel _client = JsonConvert.DeserializeObject<LoginGroupModel>(rpl);
                    return _client;
                }
                else
                {
                    LoginGroupModel _client = JsonConvert.DeserializeObject<LoginGroupModel>(result);
                    return _client;
                }
               
            }
            else
            {
                return null;
            }
        }
        public int PostLoginGroup(string groupId, string LoginId)
        {
            var _object = new
            {
                GroupId = groupId,
                LoginId = LoginId
            };
            var url = "api/LoginGroup";
            string result = SC.PostCaller(url, _object);
            int clientId = Convert.ToInt32(result);
            return clientId;
        }

        public bool PutLoginGroup(int LoginGroupId, string groupId, string LoginId)
        {
            var _object = new
            {
                GroupId = groupId,
                LoginId = LoginId
            };
            var url = "api/LoginGroup/" + LoginGroupId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteLoginGroup(int LoginGroupId)
        {
            var url = "api/LoginGroup/" + LoginGroupId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}