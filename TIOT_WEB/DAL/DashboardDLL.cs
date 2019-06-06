using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class DashboardDLL
    {

        public List<ObjectRelayStatus> getObjectRelayStatus(int objectID)
        {
            List<ObjectRelayStatus> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectRelayStatus", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectRelayStatus>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectRelayStatus model = new ObjectRelayStatus();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Device = row["Device"].ToString();
                        model.Name = row["Name"].ToString();
                        model.Current = Convert.ToDouble(row["Current"]);
                        model.Voltage = Convert.ToDouble(row["Voltage"]);
                        model.Fault = Convert.ToBoolean(row["Fault"]);
                        model.Status = row["Status"].ToString();
                        model.Category = row["Category"].ToString();
                        model.SensorId = Convert.ToInt32(row["SensorId"]);
                        model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model); 
                    }
                }
            }
            return list;
        }

        public List<ObjectRelayCurrent> getObjectCurrent(int objectID)
        {
            List<ObjectRelayCurrent> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectRelayCurrent", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectRelayCurrent>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectRelayCurrent model = new ObjectRelayCurrent();
                        model.Current = Convert.ToDouble(row["Current"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectRelayVoltage> getObjectVoltage(int objectID)
        {
            List<ObjectRelayVoltage> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectRelayVoltage", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectRelayVoltage>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectRelayVoltage model = new ObjectRelayVoltage();
                        model.Voltage = Convert.ToDouble(row["Voltage"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectInfo> getObjectInformation(int objectID)
        {
            List<ObjectInfo> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectInfo", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectInfo>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectInfo model = new ObjectInfo();
                        model.ObjectName = row["ObjectName"].ToString();
                        model.SimNumber = row["SimNumber"].ToString();
                        model.Sensor = row["Sensor"].ToString();
                        model.Value = Convert.ToDouble(row["Value"]);
                        model.SensorId = Convert.ToInt32(row["SensorId"]);
                        model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectRelayStatus> getObjectRelays(int objectID, bool relayStatus)
        {
            List<ObjectRelayStatus> liStatus =  getObjectRelayStatus(objectID);
            List<ObjectRelayCurrent> liCurrent = getObjectCurrent(objectID);
            List<ObjectRelayVoltage> liVoltage = getObjectVoltage(objectID);
            if(liStatus != null)
            {
                for (int i = 0; i < liStatus.Count; i++)
                {
                    liStatus[i].Category = "Images/categoryIcons/" + liStatus[i].Category + ".png";
                    liStatus[i].Current = Convert.ToDouble(liCurrent[i].Current);
                    liStatus[i].Voltage = Convert.ToDouble(liVoltage[i].Voltage);
                    liStatus[i].Power = (liVoltage[i].Voltage * liCurrent[i].Current)/1000;
                    if (liStatus[i].Status == "1")
                    {
                        if (relayStatus == true)
                        {
                            liStatus[i].Status = "ON";
                            liStatus[i].StatusClass = "btn  btn-xs btn-primary";
                            liStatus[i].StatusIOTClass = "btn  btn-xs ONindication";
                        }
                        if (relayStatus == false)
                        {
                            liStatus[i].Status = "OFF";
                            liStatus[i].StatusClass = "btn  btn-xs btn-danger";
                            liStatus[i].StatusIOTClass = "btn  btn-xs OFFindication";
                        }
                    }
                    if (liStatus[i].Status == "0")
                    {
                        if (relayStatus == true)
                        {
                            liStatus[i].Status = "OFF";
                            liStatus[i].StatusClass = "btn  btn-xs btn-danger";
                            liStatus[i].StatusIOTClass = "btn  btn-xs OFFindication";
                        }
                        if (relayStatus == false)
                        {
                            liStatus[i].Status = "ON";
                            liStatus[i].StatusClass = "btn  btn-xs btn-primary";
                            liStatus[i].StatusIOTClass = "btn  btn-xs ONindication";
                        }
                    }
                }   
            }
            return liStatus;
        }

        public List<ObjectAnalog> getObjectAnalog(int objectID)
        {
            List<ObjectAnalog> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectAnalogSensors", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectAnalog>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectAnalog model = new ObjectAnalog();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        model.Value = Convert.ToDouble(row["Value"]);
                        model.SensorId = Convert.ToInt32(row["SensorId"]);
                        model.Category = "Images/categoryIcons/" + row["Category"] + ".png"; 
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectDigital> getObjectDigital(int objectID)
        {
            List<ObjectDigital> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectDigitalSensors", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectDigital>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectDigital model = new ObjectDigital();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        model.Value = row["Value"].ToString();
                        model.Category = row["Category"].ToString(); 
                        if(model.Value == "1")
                        {
                              if(model.Category == "Doors")
                              {
                                  model.Value = "Closed";
                                  model.CategoryClass = "btn  btn-xs btn-danger";
                              }
                            else
                              {
                                  model.Value = "ON";
                                  model.CategoryClass = "btn  btn-xs btn-primary";
                              }
                        }
                        if (model.Value == "0") 
                        {
                            if (model.Category == "Doors")
                            {
                                model.Value = "Opned";
                                model.CategoryClass = "btn  btn-xs btn-primary";
                            }
                            else
                            {
                                model.Value = "OFF";
                                model.CategoryClass = "btn  btn-xs btn-danger";
                                
                            }
                            
                        }
                        model.SensorId = Convert.ToInt32(row["SensorId"]);
                        model.Category = "Images/categoryIcons/" + model.Category + ".png"; 
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<ObjectTemperature> getObjectTempOneWire(int objectID)
        {
            List<ObjectTemperature> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectTempInfoSensors", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<ObjectTemperature>();
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectTemperature model = new ObjectTemperature();
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.Name = row["Name"].ToString();
                        model.Value = Convert.ToDouble(row["Value"]);
                        model.Category = "Images/categoryIcons/" + row["Category"] + ".png";
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<AttendanceModelDashboard> getAttendance(int attendanceCLId)
        {
            if (attendanceCLId == 2)
            {
                List<AttendanceModelDashboard> list = null;
                using (DataTable table = DBHelper.ExecuteSelectCommand("SELECT * FROM  [crmDB].[dbo].[TodayAttendanceReportNew] order by clock_time asc", CommandType.Text))
                {
                    if (table.Rows.Count > 0)
                    {
                        list = new List<AttendanceModelDashboard>();
                        foreach (DataRow row in table.Rows)
                        {
                            AttendanceModelDashboard model = new AttendanceModelDashboard();
                            model.Emp_ID = Convert.ToInt32(row["Emp_No"]);
                            model.EmpName = row["Name"].ToString();
                            string tempImage = row["Emp_Image"].ToString();
                            model.Emp_Image = "http://124.29.205.149/hrsystem" + tempImage.Replace("~", string.Empty);
                            model.DeptName = row["Department_Name"].ToString();
                            model.Designation = row["Designation_Name"].ToString();
                            string tempClockTime = row["Clock_Time"].ToString();
                            if (tempClockTime == "")
                            {
                                model.cssClass = "panel panel-red panelAtt";
                                model.ClockTime = DateTime.Now.AddHours(12);
                                model.ClockTimeStr = "";
                            }
                            else
                            {
                                TimeSpan start = new TimeSpan(09, 15, 0);
                                model.ClockTime = Convert.ToDateTime(tempClockTime);
                                if (model.ClockTime.TimeOfDay > start)
                                {
                                    model.cssClass = "panel panel-yellow panelAtt";
                                    model.ClockTimeStr = tempClockTime;
                                }
                                else
                                {
                                    model.cssClass = "panel panel-green panelAtt";
                                    model.ClockTimeStr = tempClockTime;
                                }
                            }
                            list.Add(model);
                        }
                    }
                }
                return list;
            }
            else
            {
                List<AttendanceModelDashboard> list = null;
                using (DataTable table = DBHelper.ExecuteSelectCommand("SELECT  [Object_ID],[Client_ID],[Name],[Department_Name],[Designation_Name],[Emp_image],[Clock_Time],[Clock_In]FROM [SmartSchool].[dbo].[DailyAttendance] where [Object_ID] = '" + attendanceCLId + "' order by clock_time asc", CommandType.Text))
                {
                    if (table.Rows.Count > 0)
                    {
                        list = new List<AttendanceModelDashboard>();
                        foreach (DataRow row in table.Rows)
                        {
                            AttendanceModelDashboard model = new AttendanceModelDashboard();
                            model.Emp_ID = Convert.ToInt32(row["Client_ID"]);
                            model.EmpName = row["Name"].ToString();
                            string tempImage = row["Emp_Image"].ToString();
                            //model.Emp_Image = "http://124.29.205.149/hrsystem" + tempImage.Replace("~", string.Empty);
                            model.Emp_Image = "https://meu.edu.jo/uploads/1/59007ab8c6a3e_1.png";
                            model.DeptName = row["Department_Name"].ToString();
                            model.Designation = row["Designation_Name"].ToString();
                            string tempClockTime = row["Clock_Time"].ToString();
                            if (tempClockTime == "")
                            {
                                model.cssClass = "panel panel-red panelAtt";
                                model.ClockTime = DateTime.Now.AddHours(12);
                                model.ClockTimeStr = "";
                            }
                            else
                            {
                                TimeSpan start = new TimeSpan(09, 15, 0);
                                model.ClockTime = Convert.ToDateTime(tempClockTime);
                                if (model.ClockTime.TimeOfDay > start)
                                {
                                    model.cssClass = "panel panel-yellow panelAtt";
                                    model.ClockTimeStr = tempClockTime;
                                }
                                else
                                {
                                    model.cssClass = "panel panel-green panelAtt";
                                    model.ClockTimeStr = tempClockTime;
                                }
                            }
                            list.Add(model);
                        }
                    }
                }
                return list;
            }
        }

        public List<AttendanceIntegrationModel> getActiveAttendanceByObj(int objectId)
        {

            List<AttendanceIntegrationModel> list = null;
                using (DataTable table = DBHelper.ExecuteSelectCommand("select objectID, AttendanceClient,AttendanceIP, AttendanceStatus from objects where AttendanceStatus = 'true' and ObjectId  = '" + objectId + "'", CommandType.Text))
                {
                    if (table.Rows.Count > 0)
                    {
                        list = new List<AttendanceIntegrationModel>();
                        foreach (DataRow row in table.Rows)
                        {
                            AttendanceIntegrationModel model = new AttendanceIntegrationModel();
                            model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                            model.AttendanceClient = Convert.ToInt32(row["AttendanceClient"]);
                            model.AttendanceIP = row["AttendanceIP"].ToString();
                            model.AttendanceStatus = Convert.ToBoolean(row["AttendanceStatus"]);
                            list.Add(model);
                        }
                    }
                }
                return list;
            
            
        }

        public int getActiveCommand(int objectID, int sensorID, string description)
        {
            int res =  0;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@SensorID", sensorID),
                new SqlParameter("@Description", description)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_RelayCommand", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    res = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        res = Convert.ToInt32(row["CommandID"]);
                    }
                }
            }
            return res;
        }

        public List<CustomNotification> getCustomNotificationbyClient(int clientID, DateTime startdate, DateTime endDate)
        {
            List<CustomNotification> list = new List<CustomNotification>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID),
                new SqlParameter("@Startdate", startdate),
                new SqlParameter("@EndDate", endDate)
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_CustomNotificationByClient", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        CustomNotification model = new CustomNotification();
                        model.NotificationID = Convert.ToInt32(row["NotificationID"]);
                        model.Message = row["Message"].ToString();
                        model.DateTime = Convert.ToDateTime(row["DateTime"]);
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.CategoryName = "Images/categoryIcons/" + row["CategoryName"] + ".png";
                        model.State = Convert.ToBoolean(row["State"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public List<CustomNotification> getCustomNotificationbyGroup(int groupID, DateTime startdate, DateTime endDate)
        {
            List<CustomNotification> list = new List<CustomNotification>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@GroupID", groupID),
                new SqlParameter("@Startdate", startdate),
                new SqlParameter("@EndDate", endDate)
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_CustomNotificationByGroup", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        CustomNotification model = new CustomNotification();
                        model.NotificationID = Convert.ToInt32(row["NotificationID"]);
                        model.Message = row["Message"].ToString();
                        model.DateTime = Convert.ToDateTime(row["DateTime"]);
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.CategoryName = "Images/categoryIcons/" + row["CategoryName"] + ".png";
                        model.State = Convert.ToBoolean(row["State"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postCommandLogUser(CommandLogUserModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@UserID", _object.UserID),
                new SqlParameter("@ObjectID", _object.ObjectID),
                new SqlParameter("@SensorID", _object.SensorID),
                new SqlParameter("@CommandID", _object.CommandID),
            };
            return DBHelper.ExecuteNonQuery("uspPOST_CommandUserLog", CommandType.StoredProcedure, parameters);
        }

    }
}