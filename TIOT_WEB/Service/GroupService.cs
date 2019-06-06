using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class GroupService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<GroupModel> GetGroup()
        {
            var url = "api/Group";
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<GroupModel> _groups = JsonConvert.DeserializeObject<List<GroupModel>>(result);
                return _groups;
            }
            else
            {
                return null;
            }

        }
        public GroupModel GetGroup(int groupID)
        {
            var url = "api/Group/" + groupID;
            string result = SC.Getcaller(url);
            GroupModel _group = JsonConvert.DeserializeObject<GroupModel>(result);
            return _group;
        }


        public List<GroupModel> GetGroupByClientId(int ClientId)
        {
            var url = "api/Group?ClientId=" + ClientId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<GroupModel> _groups = JsonConvert.DeserializeObject<List<GroupModel>>(result);
                return _groups;
            }
            else
            {
                return null;
            }

        }

        public int PostGroup(string clientId, string name, string comment)
        {
            var _object = new
                {
                    ClientID = Convert.ToInt32(clientId),
                    Name = name,
                    Comment = comment
                };
            var url = "api/Group";
            string result = SC.PostCaller(url, _object);
            int GroupId = Convert.ToInt32(result);
            return GroupId;
        }

        public bool PutGroup(int groupId, string clientId, string name, string comment)
        {
            var _object = new
            {
                ClientID = Convert.ToInt32(clientId),
                Name = name,
                Comment = comment
            };
            var url = "api/Group/" + groupId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteGroup(int GroupId)
        {
            var url = "api/Group/" + GroupId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }

}

