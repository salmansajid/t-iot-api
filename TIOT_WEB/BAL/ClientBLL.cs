using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ClientBLL
    {
        ClientDLL obj = new ClientDLL();

        public List<ClientModel> getClient()
        {return obj.getClient();}

        public bool postClient(ClientModel model)
        {return obj.postClient(model);}

         public ClientModel getClientByClientID(int clientID)
        { return obj.getClientByClientID(clientID); }

         public bool disableClient(int clientID)
         { return obj.disableClient(clientID); }

         public bool codeExist(string code,string name)
         { return obj.codeExist(code,name); }

    }
}