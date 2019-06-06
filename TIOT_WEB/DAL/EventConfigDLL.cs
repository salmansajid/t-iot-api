using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class EventConfigDLL
    {
        public List<EventConfigurationModel> getEventConfigByObject(int objectID)
        {
            List<EventConfigurationModel> list = new List<EventConfigurationModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_EventConfigurationByObject", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        EventConfigurationModel model = new EventConfigurationModel();
                        model.EventConfigID = Convert.ToInt32(row["EventConfigID"]);
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.ObjectSensorID = Convert.ToInt64(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        model.Min = Convert.ToDouble(row["Min"]);
                        model.MAX = Convert.ToDouble(row["MAX"]);
                        model.a0 = Convert.ToDouble(row["a0"]);
                        model.a1 = Convert.ToDouble(row["a1"]);
                        model.Condition = Convert.ToInt32(row["Condition"]);
                        model.Contact = row["Contact"].ToString();
                        model.Units = row["Units"].ToString();
                        model.Format = row["Format"].ToString();
                        model.EnableOrDisable = Convert.ToBoolean(row["EnableOrDisable"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postEventConfig(EventConfigurationModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@EventConfigID",_object.EventConfigID),
                new SqlParameter("@ObjectSensorID",_object.ObjectSensorID),
                new SqlParameter("@ObjectID",_object.ObjectID),
                new SqlParameter("@Min", _object.Min),
                new SqlParameter("@MAX", _object.MAX),
                new SqlParameter("@a0", _object.a0),
                new SqlParameter("@a1", _object.a1),
                new SqlParameter("@Contact", _object.Contact),
                new SqlParameter("@Units", _object.Units),
                new SqlParameter("@Format", _object.Format),
                new SqlParameter("@Condition", _object.Condition),
                new SqlParameter("@EnableOrDisable", _object.EnableOrDisable),
            };

            return DBHelper.ExecuteNonQuery("uspPOST_EventConfiguration", CommandType.StoredProcedure, parameters);
        }

        public EventConfigurationModel getEventConfigByID(int eventConfigID)
        {
            EventConfigurationModel model = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@EventConfigID", eventConfigID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("Select * from [EventConfiguration] Where EventConfigID = @EventConfigID", CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                        DataRow row = table.Rows[0];
                        model = new EventConfigurationModel();
                        model.EventConfigID = Convert.ToInt32(row["EventConfigID"]);
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.ObjectSensorID = Convert.ToInt64(row["ObjectSensorID"]);
                        model.Min = Convert.ToDouble(row["Min"]);
                        model.MAX = Convert.ToDouble(row["MAX"]);
                        model.a0 = Convert.ToDouble(row["a0"]);
                        model.a1 = Convert.ToDouble(row["a1"]);
                        model.Condition = Convert.ToInt32(row["Condition"]);
                        model.Contact = row["Contact"].ToString();
                        model.Units = row["Units"].ToString();
                        model.Format = row["Format"].ToString();
                        model.EnableOrDisable = Convert.ToBoolean(row["EnableOrDisable"]);
                }
            }
            return model;
        }
    }
}