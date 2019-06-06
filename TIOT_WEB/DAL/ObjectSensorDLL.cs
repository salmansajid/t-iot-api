using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ObjectSensorDLL
    {
        public List<ObjectSensorModel> getObjectSensorList(int objectID)
        {
            List<ObjectSensorModel> list = new List<ObjectSensorModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectSensorListsByObject", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorModel model = new ObjectSensorModel();
                        model.ObjectSensorId = Convert.ToInt32(row["ObjectSensorId"]);
                        model.Name = row["Name"].ToString();
                        model.SourceID = row["SourceID"].ToString();
                        model.Min = Convert.ToInt32(row["Min"]);
                        model.Max = Convert.ToInt32(row["Max"]);
                        model.A0 = Convert.ToDouble(row["A0"]);
                        model.A1 = Convert.ToDouble(row["A1"]);
                        model.Contact = row["Contact"].ToString();
                        model.Category = row["Category"].ToString();
                        model.SMSAlert = Convert.ToBoolean(row["SMSAlert"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<SensorIDSourceID> getNASensorListByObject(int objectID)
        {
            List<SensorIDSourceID> list = new List<SensorIDSourceID>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_NASensorsByObject", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        SensorIDSourceID model = new SensorIDSourceID();
                        model.SensorID = Convert.ToInt32(row["SensorID"]);
                        model.SourceID = row["SourceID"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        

        public bool postObjectSensor(ObjectSensorModelDLL _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectSensorId",_object.ObjectSensorId),
                new SqlParameter("@SensorID",_object.SensorID),
                new SqlParameter("@ObjectID",_object.ObjectID),
                new SqlParameter("@Name", _object.Name),
                new SqlParameter("@SMSAlert", _object.SMSAlert),
                new SqlParameter("@EmailAlert", _object.EmailAlert),
                new SqlParameter("@A1", _object.A1),
                new SqlParameter("@A0", _object.A0),
                new SqlParameter("@Contact", _object.Contact),
                new SqlParameter("@Min", _object.Min),
                new SqlParameter("@Max", _object.Max),
                new SqlParameter("@CategoryID", _object.CategoryID),
                
            };

            return DBHelper.ExecuteNonQuery("uspPOST_ObjectSensor", CommandType.StoredProcedure, parameters);
        }

        public ObjectSensorModelDLL getObjectSensorByID(int objectSensorID)
        {
            ObjectSensorModelDLL model = new ObjectSensorModelDLL();
            string query = "select * from [ObjectSensors] where ObjectSensorID = @ObjectSensorID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectSensorID", objectSensorID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.SensorID = Convert.ToInt32(row["SensorID"]);
                    model.ObjectID =  Convert.ToInt32(row["ObjectID"]);
                    model.Name = row["Name"].ToString();
                    model.SMSAlert = Convert.ToBoolean(row["SMSAlert"]);
                    model.EmailAlert = Convert.ToBoolean(row["EmailAlert"]);
                    model.A0 = Convert.ToDouble(row["A0"]);
                    model.A1 = Convert.ToDouble(row["A1"]);
                    model.Contact = row["Contact"].ToString();
                    model.Min = Convert.ToInt32(row["Min"]);
                    model.Max = Convert.ToInt32(row["Max"]);
                    model.CategoryID = Convert.ToInt32(row["CategoryID"]);
                }
            }
            return model;
        }

        public bool disableObjectSensor(int objectSensorID)
        {

            string query = "Update [ObjectSensors] set [Enabled] = 'False' where ObjectSensorID = @ObjectSensorID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectSensorID", objectSensorID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool objectSensorExist(int objectID, int sensorID)
        {
            string query = "Select count(*) as [Status] from [ObjectSensors] where ObjectID = @ObjectID and SensorID = @SensorID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@SensorID", sensorID),
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