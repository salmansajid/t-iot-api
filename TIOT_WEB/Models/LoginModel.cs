using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TIOT_WEB.Models
{
    public class LoginModel
    {
        public int LoginID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
    public class LoginModelForUser
    {
        public int LoginID { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> RoleID { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public string Password { get; set; }
    }
    public class LFLoginModel
    {
        public int LoginID { get; set; }
        public int ClientID { get; set; }
        public int RoleID { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
    public class LoginModelDLL
    {
        public int LoginID { get; set; }
        public string ClientID { get; set; }
        public int RoleID { get; set; }
        public string SessionName { get; set; }
        public string DisplayName { get; set; }
    }


    public class GetLoginModel
    {
        public int LoginID { get; set; }
        public int ClientID { get; set; }
        public int RoleID { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public string ClientName { get; set; }
        public string Role { get; set; }
    }


    public class PostLoginModel
    {
        public int LoginID { get; set; }
        public int ClientID { get; set; }
        public int RoleID { get; set; }
        public string User { get; set; }
        public string DisplayName { get; set; }
        public string Comment { get; set; }
        public string Password { get; set; }
    }

}