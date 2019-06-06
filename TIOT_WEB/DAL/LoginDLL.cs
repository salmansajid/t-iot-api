using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class LoginDLL
    {
        public LoginModelDLL getActiveLogin(string username, string status)
        {
            LoginModelDLL model = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@User", username),
                new SqlParameter("@Status", status)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginStatus", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count == 1)
                {
                        model = new LoginModelDLL(); 
                        DataRow row = table.Rows[0];
                        model.DisplayName = row["DisplayName"].ToString();
                        model.SessionName = row["SessionName"].ToString();
                        model.ClientID = row["ClientID"].ToString();
                        model.RoleID = Convert.ToInt32(row["RoleID"]);
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                    
                }
            }
            return model;
        }

        public PostLoginModel getLoginByLoginID(int LoginID)
        {
            PostLoginModel model = new PostLoginModel();
            string query = "select * from login where LoginID = @LoginID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", LoginID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                        DataRow row = table.Rows[0];
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.RoleID = Convert.ToInt32(row["RoleID"]);
                        model.User = row["User"].ToString();
                        model.Comment = row["Comment"].ToString();
                        model.Password = row["Password"].ToString();
                        model.DisplayName = row["DisplayName"].ToString();
                }
            }
            return model;
        }

        public List<GetLoginModel> getLoginByClient(int clientID)
        {
            List<GetLoginModel> list = new List<GetLoginModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginsByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        GetLoginModel model = new GetLoginModel();
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.RoleID = Convert.ToInt32(row["RoleID"]);
                        model.User = row["User"].ToString();
                        model.Comment = row["Comment"].ToString();
                        model.ClientName = row["ClientName"].ToString();
                        model.Role = row["Role"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        
        public int getActiveCode(string code)
        {
            int res =  -1;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Code", code),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ClientCode", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        res = Convert.ToInt32(row["Status"]);
                    }
                }
            }
            return res;
        }

        public bool postLogin(PostLoginModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@LoginID", _object.LoginID),
                new SqlParameter("@ClientID", _object.ClientID),
                new SqlParameter("@RoleID", _object.RoleID),
                new SqlParameter("@User", _object.User),
                new SqlParameter("@Password", _object.Password),
                new SqlParameter("@Comment", _object.Comment),
                new SqlParameter("@DisplayName", _object.DisplayName),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_Login", CommandType.StoredProcedure, parameters);
        }

        public bool disableLogin(int loginID)
        {
            string query = "update [Login] set Enabled = 'False' where LoginID = @LoginID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@LoginID", loginID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool usernameExist(string username)
        {
            string query = "select count(*) as [Status] from login where [user] = @User";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@User", username)
            };
            DataTable dt =  DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters);
            if(dt.Rows.Count == 1)
            {
                return  Convert.ToBoolean(dt.Rows[0]["Status"]);
            }
            return true;
        }
    }
}