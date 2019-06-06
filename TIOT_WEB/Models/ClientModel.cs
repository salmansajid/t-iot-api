using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ClientModel
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int OperatorID { get; set; }
        public string Contact { get; set; }
        public string Code { get; set; }
        public bool Enabled { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public bool Deleted { get; set; }
    }
    public class ClientModelTavl
    {
        public string clientId { get; set; }
        public string clientName { get; set; }
    }
}