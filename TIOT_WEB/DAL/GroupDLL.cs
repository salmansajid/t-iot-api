using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class GroupDLL
    {
        public List<GetGroupModel> getGroupList(int clientID)
        {
            List<GetGroupModel> list = new List<GetGroupModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_GroupByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        GetGroupModel model = new GetGroupModel();
                        model.GroupID = Convert.ToInt32(row["GroupID"]);
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.Name = row["Name"].ToString();
                        model.Comment = row["Comment"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public GetGroupModel getGroupByGroupID(int groupID)
        {
            GetGroupModel model = new GetGroupModel();
            string query = "select * from [Group] where GroupID = @GroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GroupID", groupID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.ClientID = Convert.ToInt32(row["ClientID"]);
                    model.Name = row["Name"].ToString();
                    model.Comment = row["Comment"].ToString();
                }
            }
            return model;
        }


        public bool postGroup(GetGroupModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@GroupID", _object.GroupID),
                new SqlParameter("@ClientID", _object.ClientID),
                new SqlParameter("@Name", _object.Name),
                new SqlParameter("@Comment", _object.Comment),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_Group", CommandType.StoredProcedure, parameters);
        }

        public bool disableGroup(int groupID)
        {
            string query = "Update [Group] set Deleted = 'True' where GroupID = @GroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@GroupID", groupID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool groupExist(string name)
        {
            string query = "select count(*) as [Status] from [Group] where [Name] = @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@Name", name)
            };
            DataTable dt = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters);
            if (dt.Rows.Count == 1)
            {
                return Convert.ToBoolean(dt.Rows[0]["Status"]);
            }
            return true;
        }
    }
}