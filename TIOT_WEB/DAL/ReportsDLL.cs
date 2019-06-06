using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ReportsDLL
    {
        #region Consumption Report
        public List<SwitchesReportDayModel> getConsumptionToday(int objectID, string sensor)
        {
            List<SwitchesReportDayModel> list = new List<SwitchesReportDayModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@Sensor", sensor),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_TodayAVGConsumption", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<SwitchesReportDayModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        SwitchesReportDayModel model = new SwitchesReportDayModel();
                        model.Name = row["Name"].ToString();
                        model.Value = Convert.ToDouble(row["Value"]);
                        model.sensorId = Convert.ToInt32(row["Value"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        public List<SwitchesReportConsumptionModel> getConsumptionByDT(int objectID, DateTime startDate, DateTime endDate)
        {
            List<SwitchesReportConsumptionModel> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGet_EquipmentConsumptionByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<SwitchesReportConsumptionModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        SwitchesReportConsumptionModel model = new SwitchesReportConsumptionModel();
                        model.Name = row["Name"].ToString();
                        model.Current = row["Current"].ToString();
                        model.Voltage = row["Voltage"].ToString();
                        model.Power =  row["Power"].ToString();
                        model.Unit = string.Format("{0:0.00}", row["Unit"]);
                        //model.Status = row["Status"].ToString();
                        model.TotalTime = row["TotalTime"].ToString();
                        model.StartTime = Convert.ToDateTime(row["StartTime"]);
                        model.EndTime = Convert.ToDateTime(row["EndTime"]);
                        //model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Controlling Report
        public List<SwitchesReportControllingModel> getControllingByDT(int objectID, DateTime startDate, DateTime endDate)
        {
            List<SwitchesReportControllingModel> list = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGet_EquipmentControllingByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<SwitchesReportControllingModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        SwitchesReportControllingModel model = new SwitchesReportControllingModel();
                        model.Name = row["Name"].ToString();
                        model.Status = row["Status"].ToString();
                        model.TotalTime = row["TotalTime"].ToString();
                        model.StartTime = Convert.ToDateTime(row["StartTime"]);
                        model.EndTime = Convert.ToDateTime(row["EndTime"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        public List<SwitchesReportControllingModel> getControllingToday(int objectID)
        {
            List<SwitchesReportControllingModel> list = new List<SwitchesReportControllingModel>();
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@ObjectID", objectID), };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_TodayControlling", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<SwitchesReportControllingModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        SwitchesReportControllingModel model = new SwitchesReportControllingModel();
                        model.Name = row["Name"].ToString();
                        model.Status = row["Status"].ToString();
                        if(model.Status == "0")
                        {
                            model.Status = "OFF";
                        }
                        if (model.Status == "1")
                        {
                            model.Status = "ON";
                        }
                        model.TotalTime = row["TotalTime"].ToString();
                        model.StartTime = Convert.ToDateTime(row["StartTime"]);
                        model.EndTime = Convert.ToDateTime(row["EndTime"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region Sensor Variation Report
        public List<SensorVariationModel> getSensorVariationReport(int objectSensorID, DateTime StartDate, DateTime EndDate)
        {
            List<SensorVariationModel> list = new List<SensorVariationModel>();
            SqlParameter[] parameters = new SqlParameter[]
            { 
                new SqlParameter("@ObjectSensorID", objectSensorID),new SqlParameter("@StartDate", StartDate),new SqlParameter("@EndDate", EndDate),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGet_EventConfigLogByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<SensorVariationModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        SensorVariationModel model = new SensorVariationModel();
                        model.Name = row["Name"].ToString();
                        model.Condition = row["Condition"].ToString();
                        model.Value = Convert.ToDouble(row["Value"]);
                        model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region  DigitalInput Report
        public List<DigitalInputModel> getDigitalInputReport(int objectSensorID, DateTime StartDate, DateTime EndDate)
        {
            List<DigitalInputModel> list = new List<DigitalInputModel>();
            SqlParameter[] parameters = new SqlParameter[]
            { 
                new SqlParameter("@ObjectSensorID", objectSensorID),new SqlParameter("@StartDate", StartDate),new SqlParameter("@EndDate", EndDate),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGet_DigitalStateByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<DigitalInputModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        DigitalInputModel model = new DigitalInputModel();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        model.Status = row["Status"].ToString();
                        if(model.Status =="1")
                        {
                            model.Status = "ON";
                        }
                        if (model.Status == "0")
                        {
                            model.Status = "OFF";
                        }
                        
                        model.StartTime = Convert.ToDateTime(row["StartTime"]);
                        model.EndTime = Convert.ToDateTime(row["EndTime"]);
                        model.TotalTime = row["TotalTime"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region  IndividualSensor Report
        public List<IndividualSensorModel> getIndividualSensorReport(int objectSensorID, DateTime StartDate, DateTime EndDate, double min, double max)
        {
            List<IndividualSensorModel> list = new List<IndividualSensorModel>();
            SqlParameter[] parameters = new SqlParameter[]
            { 
                new SqlParameter("@ObjectSensorID", objectSensorID),
                new SqlParameter("@StartDate", StartDate),
                new SqlParameter("@EndDate", EndDate),
                new SqlParameter("@Min", min),
                new SqlParameter("@Max", max),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGet_IndividualSensorByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<IndividualSensorModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        IndividualSensorModel model = new IndividualSensorModel();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        model.Value = row["Value"].ToString();
                        model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion

        #region  Analog Report
        public List<IndividualSensorModel> getEventLogReport(int objectSensorID, DateTime StartDate, DateTime EndDate)
        {
            List<IndividualSensorModel> list = new List<IndividualSensorModel>();
            SqlParameter[] parameters = new SqlParameter[]
            { 
                new SqlParameter("@ObjectSensorID", objectSensorID),new SqlParameter("@StartDate", StartDate),new SqlParameter("@EndDate", EndDate),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_EventLogDataByDT", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    list = new List<IndividualSensorModel>();
                    foreach (DataRow row in table.Rows)
                    {
                        IndividualSensorModel model = new IndividualSensorModel();
                        model.ObjectSensorID = Convert.ToInt32(row["ObjectSensorID"]);
                        model.Name = row["Name"].ToString();
                        model.Value = row["Value"].ToString();
                        model.DateTimeStamp = Convert.ToDateTime(row["DateTimeStamp"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        #endregion


        
    }
}