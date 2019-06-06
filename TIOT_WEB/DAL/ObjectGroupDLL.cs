using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ObjectGroupDLL
    {
       
        public List<ObjectGroupModel> getObjectGroupByObject(int objectID)
        {
            List<ObjectGroupModel> list = new List<ObjectGroupModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectGroupByObject", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectGroupModel model = new ObjectGroupModel();
                        model.ObjectGroupID = Convert.ToInt32(row["ObjectGroupID"]);
                        model.ObjectName = row["ObjectName"].ToString();
                        model.GroupName = row["GroupName"].ToString();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.GroupID = Convert.ToInt32(row["GroupID"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postObjectGroup(ObjectGroupModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectGroupID", _object.ObjectGroupID),
                new SqlParameter("@ObjectID", _object.ObjectID),
                new SqlParameter("@GroupID", _object.GroupID),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_ObjectGroup", CommandType.StoredProcedure, parameters);
        }

        public ObjectGroupModel getObjectGroupByID(int objectGroupID)
        {
            ObjectGroupModel model = new ObjectGroupModel();
            string query = "select * from [ObjectGroup] where ObjectGroupID = @ObjectGroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectGroupID", objectGroupID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.ObjectGroupID = Convert.ToInt32(row["ObjectGroupID"]);
                    model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                    model.GroupID = Convert.ToInt32(row["GroupID"]);
                }
            }
            return model;
        }


        public bool deleteObjectGroup(int objectgroupID)
        {
            string query = "delete from [ObjectGroup] where ObjectGroupID = @ObjectGroupID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectGroupID", objectgroupID),
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
    }
}