using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class GroupBLL
    {
        GroupDLL obj = new GroupDLL();

        public List<GetGroupModel> getGroupList(int clientID)
        { return obj.getGroupList(clientID); }

         public GetGroupModel getGroupByGroupID(int groupID)
        { return obj.getGroupByGroupID(groupID); }

        public bool postGroup(GetGroupModel _object)
        { return obj.postGroup(_object); }

        public bool disableGroup(int groupID)
        { return obj.disableGroup(groupID); }

        public bool groupExist(string name)
        {return obj.groupExist(name);}
    }
}