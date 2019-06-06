using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class LoginGroupDLL
    {

        public List<LoginGroupModel> getLoginGroupByLogin(int loginID)
        {
            List<LoginGroupModel> list = new List<LoginGroupModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", loginID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginGroupByLogin", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        LoginGroupModel model = new LoginGroupModel();
                        model.LoginGroupID = Convert.ToInt32(row["LoginGroupID"]);
                        model.LoginName = row["User"].ToString();
                        model.GroupName = row["GroupName"].ToString();
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                        model.GroupID = Convert.ToInt32(row["GroupID"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postLoginGroup(LoginGroupModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@LoginGroupID", _object.LoginGroupID),
                new SqlParameter("@LoginID", _object.LoginID),
                new SqlParameter("@GroupID", _object.GroupID),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_LoginGroup", CommandType.StoredProcedure, parameters);
        }

        public LoginGroupModel getLoginGroupByID(int loginGroupID)
        {
            LoginGroupModel model = new LoginGroupModel();
            string query = "select * from [LoginGroup] where LoginGroupID = @LoginGroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginGroupID", loginGroupID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.LoginGroupID = Convert.ToInt32(row["LoginGroupID"]);
                    model.LoginID = Convert.ToInt32(row["LoginID"]);
                    model.GroupID = Convert.ToInt32(row["GroupID"]);
                }
            }
            return model;
        }


        public bool deleteLoginGroup(int logingroupID)
        {
            string query = "delete from [LoginGroup] where LoginGroupID = @LoginGroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@LoginGroupID", logingroupID),
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
    }
}