using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class ObjectGroupService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<ObjectGroupModel> GetObjectGroup()
        {
            var url = "api/ObjectGroup";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectGroupModel> ObjectGroups = JsonConvert.DeserializeObject<List<ObjectGroupModel>>(result);
                return ObjectGroups;
            }
            else
            {
                return null;
            }

        }

        public ObjectGroupModel GetObjectGroup(int ObjectGroupId)
        {
            var url = "api/ObjectGroup/"+ ObjectGroupId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                ObjectGroupModel _client = JsonConvert.DeserializeObject<ObjectGroupModel>(result);
                return _client;
            }
            else
            {
                return null;
            }

        }
        //GetObjectGroupById

        public List<ObjectGroupModel> GetObjectGroupById(int Id)
        {
            var url = "api/ObjectGroup?ObjectId=" + Id;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectGroupModel> _client = JsonConvert.DeserializeObject<List<ObjectGroupModel>>(result);
                return _client;
            }
            else
            {
                return null;
            }

        }
        public int PostObjectGroup(string ObjectId, string groupId)
        {
            var _object = new
            {
                ObjectId = ObjectId,
                GroupId = groupId

            };
            var url = "api/ObjectGroup";
            string result = SC.PostCaller(url, _object);
            int clientId = Convert.ToInt32(result);
            return clientId;
        }

        public bool PutObjectGroup(int ObjectGroupId, string ObjectId, string groupId)
        {
            var _object = new
            {
                ObjectId = ObjectId,
                GroupId = groupId
            };
            var url = "api/ObjectGroup/" + ObjectGroupId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteObjectGroup(int ObjectGroupId)
        {
            var url = "api/ObjectGroup/" + ObjectGroupId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}