using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ObjectDLL
    {
        public List<ObjectModelDLL> getObjectList(int clientID)
        {
            List<ObjectModelDLL> list = new List<ObjectModelDLL>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectListsByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectModelDLL model = new ObjectModelDLL();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        model.Address = row["Address"].ToString();
                        model.LAT = Convert.ToDouble(row["LAT"]);
                        model.LONG = Convert.ToDouble(row["LONG"]);
                        model.IMEI = Convert.ToInt64(row["IMEI"]);
                        model.SimNumber = Convert.ToInt64(row["SimNumber"]);
                        model.FirmWareVersion = row["FirmWareVersion"].ToString();
                        model.HardwareVersion = row["HardwareVersion"].ToString();
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.Contact = row["Contact"].ToString();
                        model.ObjectType = row["ObjectType"].ToString();
                        model.RelayStatus = Convert.ToBoolean(row["RelayStatus"]);
                        model.CreatedDateTime = Convert.ToDateTime(row["CreatedDateTime"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postObject(ObjectModelDLL _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectID",_object.ObjectID),
                new SqlParameter("@Name", _object.Name),
                new SqlParameter("@Address", _object.Address),
                new SqlParameter("@LAT", _object.LAT),
                new SqlParameter("@LONG", _object.LONG),
                new SqlParameter("@IMEI", _object.IMEI),
                new SqlParameter("@SimNumber", _object.SimNumber),
                new SqlParameter("@FirmWareVersion", _object.FirmWareVersion),
                new SqlParameter("@HardwareVersion", _object.HardwareVersion),
                new SqlParameter("@ClientID", _object.ClientID),
                new SqlParameter("@Contact", _object.Contact),
                new SqlParameter("@ObjectType", _object.ObjectType),
                new SqlParameter("@RelayStatus", _object.RelayStatus),
            };

            return DBHelper.ExecuteNonQuery("uspPOST_Object", CommandType.StoredProcedure, parameters);
        }

        public ObjectModelDLL getObjectByObjectID(int objectID)
        {
            ObjectModelDLL model = null;
            string query = "Select * from [Objects] where ObjectID = @ObjectID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model = new ObjectModelDLL();
                    model.ClientID = Convert.ToInt32(row["ClientID"]);
                    model.Name = row["Name"].ToString();
                    model.Address = row["Address"].ToString();
                    model.LAT = Convert.ToDouble(row["LAT"]);
                    model.LONG = Convert.ToDouble(row["LONG"]);
                    model.IMEI = Convert.ToInt64(row["IMEI"]);
                    model.SimNumber = Convert.ToInt64(row["SimNumber"]);
                    model.FirmWareVersion = row["FirmWareVersion"].ToString();
                    model.HardwareVersion = row["HardwareVersion"].ToString();
                    model.Contact = row["Contact"].ToString();
                    model.ObjectType = row["ObjectType"].ToString();
                    model.RelayStatus = Convert.ToBoolean(row["RelayStatus"]);
                }
            }
            return model;
        }

        public bool disableObject(int objectID)
        {
            
            string query = "Update [Objects] set Deleted = 'True', DeleteDateTime = getdate() where ObjectID = @ObjectID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ObjectID", objectID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool objectExist(string imei)
        {
            string query = "select count(*) as [Status] from [Objects] where [IMEI] = @IMEI";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@IMEI", imei)
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