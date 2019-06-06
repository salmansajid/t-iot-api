using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class GroupModel
    {
        public int GroupID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string ClientName { get; set; }
    }

    public class GetGroupModel
    {
        public int GroupID { get; set; }
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
    }


    public class GroupModeltavl
    {
        public string groupId { get; set; }
        public string groupName { get; set; }
    }
}