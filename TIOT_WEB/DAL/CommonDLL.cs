using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class CommonDLL
    {
        #region Client Group Object
        public List<ClientIDName> getClients()
        {
            List<ClientIDName> list = new  List<ClientIDName>();
            using (DataTable table = DBHelper.ExecuteSelectCommand("uspGET_ClientIDName", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ClientIDName model = new ClientIDName();
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<GroupIDName> getGroupByClient(int clientID)
        {
            List<GroupIDName> list = new List<GroupIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_GroupIDNameByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<GroupIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        GroupIDName model = new GroupIDName();
                        model.GroupID = Convert.ToInt32(row["GroupID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<LoginIDName> getLoginList(int clientID)
        {
            List<LoginIDName> list = new List<LoginIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_LoginsByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        LoginIDName model = new LoginIDName();
                        model.LoginID = Convert.ToInt32(row["LoginID"]);
                        model.Name = row["User"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectListDashboard> getObjectsByGroup(int groupID)
        {
            List<ObjectListDashboard> list = new List<ObjectListDashboard>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GroupID", groupID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectIDNameByGroup", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectListDashboard>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectListDashboard model = new ObjectListDashboard();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        model.LastRecordReceived = Convert.ToDateTime(row["LastRecordReceived"]);
                        model.RelayStatus = Convert.ToBoolean(row["RelayStatus"]);
                        model.TavlIP = row["TavlIP"].ToString();
                        model.TavlStatus = Convert.ToBoolean(row["TavlStatus"]);
                        model.AttendanceIP = row["AttendanceIP"].ToString();
                        model.AttendanceClient = row["AttendanceClient"].ToString();
                        model.AttendanceStatus = Convert.ToBoolean(row["AttendanceStatus"]);
                        if (model.LastRecordReceived < DateTime.Now.AddMinutes(-10))
                        { model.StatusClass = "fa fa-plug fa-fw faTopDate-red"; }
                        else
                        { model.StatusClass = "fa fa-plug fa-fw faTopDate-primary"; }
                        list.Add(model);
                    }
                }
            }
            return list;
        }


        public List<ObjectIDName> getObjectListByClient(int clientID)
        {
            List<ObjectIDName> list = new List<ObjectIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectIDName model = new ObjectIDName();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public ObjectStatus getObjectStatus(int objectID)
        {
            ObjectStatus li = new ObjectStatus();

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectStatus", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    li.Name = row["Name"].ToString();
                    li.SimNumber = row["SimNumber"].ToString();
                    li.LastRecordReceived = Convert.ToDateTime(row["LastRecordReceived"]);
                    li.RelayStatus = Convert.ToBoolean(row["RelayStatus"]);
                }
            }
            return li;
        }

        public int getGroupIDForUser(int loginID)
        {
            int res = 0;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@LoginID", loginID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("select [GroupID] from logingroup where LoginID = @LoginID", CommandType.Text, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    res = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        res = Convert.ToInt32(row["GroupID"]);
                    }
                }
            }
            return res;
        }



        
#       endregion

        #region Eventconfiguration
        public List<ObjectSensorIDName> getEventConfiguredSensor(int objectID)
        {
            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_EventConfiguredSensor", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Digital input
        public List<ObjectSensorIDName> getObjectSensorDinList(int objectID)
        {
            List<ObjectSensorIDName> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("usgGET_ObjectSensorDinList", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Analog Sensor
        public List<ObjectSensorIDName> getObjectSensorAinList(int objectID)
        {
            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("usgGET_ObjectSensorAinList", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Temperature Value
        public List<ObjectSensorIDName> getObjectSensorTempValList(int objectID)
        {
            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("usgGET_ObjectSensorTempValList", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region DIN & Serial Value
        public List<ObjectSensorIDName> getObjectSensorDinAndSerialSensor(int objectID)
        {
            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_DinAndSerialSensors", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion
        
        #region Basic Sensors
        public List<ObjectSensorIDName> getBasicSensorByObject(int objectID, int sensorID)
        {
            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            string query = "Select ObjectSensorID, Name from ObjectSensors where SensorID = "+ sensorID +" and ObjectId = "+objectID+"";
            using (DataTable table = DBHelper.ExecuteSelectCommand(query, CommandType.Text))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Relay Sensors
        public List<ObjectSensorIDName> getRelaySensorByObject(int objectID, string sensorUnit)
        {

            List<ObjectSensorIDName> list = new List<ObjectSensorIDName>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@SensorUnit", sensorUnit)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_RelaySensorByObject", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectSensorIDName>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectSensorIDName model = new ObjectSensorIDName();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Category
        public List<CategoryIDName> getCategoryIDName()
        {

            List<CategoryIDName> list = new List<CategoryIDName>();
            using (DataTable table = DBHelper.ExecuteSelectCommand("Select CategoryID,Name as [Category] from category where EnableORDisable = 'True' and Deleted = 'False'", CommandType.Text))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        CategoryIDName model = new CategoryIDName();
                        model.CategoryID = Convert.ToInt32(row["CategoryID"]);
                        model.Category = row["Category"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Category
        public List<SensorIDSourceID> getSensorIDSourceID()
        {

            List<SensorIDSourceID> list = new List<SensorIDSourceID>();
            using (DataTable table = DBHelper.ExecuteSelectCommand("Select SensorID , SourceID from Sensors where EnableORDisable = 'True' and Unit != '0'", CommandType.Text))
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
        #endregion

        

      

    }
}