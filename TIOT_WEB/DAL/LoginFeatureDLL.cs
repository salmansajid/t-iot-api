using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class LoginFeatureDLL
    {
        public List<FeatureIDName> getNAFeatureByLogin(int loginID)
        {
            List<FeatureIDName> list = new List<FeatureIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", loginID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_NAFeatureByLogin", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        FeatureIDName model = new FeatureIDName();
                        model.FeatureID = Convert.ToInt32(row["FeatureID"]);
                        model.Name = row["Name"].ToString();                        
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<LoginFeatureModel> getFeatureByLogin(int loginID)
        {
            List<LoginFeatureModel> list = new List<LoginFeatureModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", loginID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginFeatureByLogin", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        LoginFeatureModel model = new LoginFeatureModel();
                        model.LoginFeatureID = Convert.ToInt32(row["LoginFeatureID"]);
                        model.LoginName = row["User"].ToString();
                        model.FeatureName = row["FeatureName"].ToString();
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                        model.FeatureID = Convert.ToInt32(row["FeatureID"]);
                        model.Enable = Convert.ToBoolean(row["Enable"]); 
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postLoginFeature(LoginFeatureModel _object)
        {
            List<LoginFeatureModel> list = new List<LoginFeatureModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginFeatureID", _object.LoginFeatureID),
                new SqlParameter("@LoginID", _object.LoginID),
                new SqlParameter("@FeatureID", _object.FeatureID),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_LoginFeature", CommandType.StoredProcedure, parameters);
        }

        public bool E_D_LoginFeature(int loginFeatureID, bool enable)
        {
            string query = "Update [LoginFeature] set [Enable] = @Enable where [LoginFeatureID] = @LoginFeatureID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginFeatureID", loginFeatureID),
                new SqlParameter("@Enable", enable),
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
        public bool deleteLoginFeature(int loginFeatureID)
        {
            string query ="Delete  from  [LoginFeature] where [LoginFeatureID] = @LoginFeatureID";
            SqlParameter[] parameters = new SqlParameter[]
            {new SqlParameter("@LoginFeatureID", loginFeatureID)};
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
        
    }
}