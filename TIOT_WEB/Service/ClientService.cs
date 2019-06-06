using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class ClientService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<ClientModel> GetClient()
        {
            var url = "api/Client";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ClientModel> clients = JsonConvert.DeserializeObject<List<ClientModel>>(result);
                return clients;
            }
            else
            {
                return null;
            }

        }

        public ClientModel GetClientById(int ClientId)
        {
            var url = "api/Client/" + ClientId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                ClientModel _client = JsonConvert.DeserializeObject<ClientModel>(result);
                return _client;
            }
            else
            {
                return null;
            }

        }
        public int PostClient(string name, string address, int operatorId, string contact, string code, bool enabled, string email, string expireDate, bool deleted)
        {
            var _object = new
                {
                    Name = name,
                    Address = address,
                    OperatorID = operatorId,
                    Contact = contact,
                    Code = code,
                    Enabled = enabled,
                    Email = email,
                    ExpireDate = expireDate,
                    Deleted = deleted
                };
            var url = "api/Client";
            string result = SC.PostCaller(url, _object);
            int clientId = Convert.ToInt32(result);
            return clientId;    
        }

        public bool PutClient(int ClientId, string name, string address, int operatorId, string contact, string code, bool enabled, string email, string expireDate, bool deleted)
        {
            var _object = new
            {
                Name = name,
                Address = address,
                OperatorID = operatorId,
                Contact = contact,
                Code = code,
                Enabled = enabled,
                Email = email,
                ExpireDate = expireDate,
                Deleted = deleted
            };
            var url = "api/Client/"+ ClientId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }
        
        public bool DeleteClient(int ClientId)
        {
            var url = "api/Client/"+ ClientId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }
    }
}
