using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ConfigurationDLL
    {
        public List<ConfigurationModel> getConfigurationList(int parentID)
        {
            List<ConfigurationModel> list = new List<ConfigurationModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ParentID", parentID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_FeatureByParentID", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ConfigurationModel model = new ConfigurationModel();
                        model.FeatureID = Convert.ToInt32(row["FeatureID"]);
                        model.Name = row["Name"].ToString();
                        model.Description = row["Description"].ToString();
                        model.Class = row["Class"].ToString();
                        model.Link = row["Link"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        #region FeatureDLL


        public List<ConfigurationModel> getFeatureList(int parentID)
        {
            List<ConfigurationModel> list = new List<ConfigurationModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ParentID", parentID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("Select * from [Feature] Where ParentID = @ParentID", CommandType.Text, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ConfigurationModel model = new ConfigurationModel();
                        model.FeatureID = Convert.ToInt32(row["FeatureID"]);
                        model.Name = row["Name"].ToString();
                        model.Description = row["Description"].ToString();
                        model.Class = row["Class"].ToString();
                        model.Link = row["Link"].ToString();
                        model.EnableOrDisable = Convert.ToBoolean(row["EnableOrDisable"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<FeatureConfigurationModel> getFeatureListByLogin(int loginID, int parentID)
        {
            List<FeatureConfigurationModel> list = new List<FeatureConfigurationModel>();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", loginID),
                new SqlParameter("@ParentID", parentID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginFeatureByLoginID", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        FeatureConfigurationModel model = new FeatureConfigurationModel();
                        model.LoginFeatureID = Convert.ToInt32(row["LoginFeatureID"]);
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                        model.FeatureID = Convert.ToInt32(row["FeatureID"]);
                        model.Name = row["Name"].ToString();
                        model.Class = row["Class"].ToString();
                        model.Link = row["Link"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool putFeature(int featureID, string name, string cssclass, bool enable)
        {
            string query = "update [Feature] set EnableOrDisable = @EnableOrDisable ,Class = @Class, Name =@Name  where FeatureID = @FeatureID";
            SqlParameter[] parameters = new SqlParameter[]
            {    
                new SqlParameter("@FeatureID", featureID),
                new SqlParameter("@Name", name),
                new SqlParameter("@Class", cssclass),
                new SqlParameter("@EnableOrDisable", enable)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool disableFeature(int featureID)
        {
            string query = "Delete from  [Feature]  where FeatureID = @FeatureID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@FeatureID", featureID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool featureExist(string name)
        {
            string query = "select count(*) as [Status] from [Feature]  where Name = @Name";
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

        #endregion

    }
}