using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ObjectMaintenanceDLL
    {
        public List<ObjectMaintenanceModel> getObjectMaintenanceByObject(int objectID)
        {
            List<ObjectMaintenanceModel> list = new List<ObjectMaintenanceModel>();
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ObjectID", objectID),
            };
            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand("uspGET_ObjectMaintenanceLog", CommandType.StoredProcedure, parameters))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ObjectMaintenanceModel model = new ObjectMaintenanceModel();
                        model.MainId = Convert.ToInt32(row["MainId"]);
                        model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                        model.IssueComments = row["IssueComments"].ToString();
                        model.IssueDateTime = Convert.ToDateTime(row["IssueDateTime"]);
                        model.IssueAuthor = row["IssueAuthor"].ToString();
                        model.ResolvedComments = row["ResolvedComments"].ToString();
                        model.ResolvedDateTime = Convert.ToDateTime(row["ResolvedDateTime"]);
                        model.ResolvedPerson = row["ResolvedPerson"].ToString();
                        model.isActive = Convert.ToBoolean(row["isActive"]);
                        if(model.isActive == true)
                        {
                            model.cssClass = "btn btn-danger btn-xs";
                            model.linkbtnText = "Resolve";
                        }
                        else
                        {
                            model.cssClass = "btn btn-success btn-xs";
                            model.linkbtnText = "Resolved";
                        }

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public bool postObjectMaintenance(ObjectMaintenanceModel _object)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@MainId", _object.MainId),
                new SqlParameter("@ObjectID", _object.ObjectID),
                new SqlParameter("@IssueComments", _object.IssueComments),
                new SqlParameter("@IssueDateTime", _object.IssueDateTime),
                new SqlParameter("@IssueAuthor", _object.IssueAuthor),
                new SqlParameter("@ResolvedComments", _object.ResolvedComments),
                new SqlParameter("@ResolvedDateTime", _object.ResolvedDateTime),
                new SqlParameter("@ResolvedPerson", _object.ResolvedPerson),
            };
            return DBHelper.ExecuteNonQuery("uspPost_objectMaintenanceLog", CommandType.StoredProcedure, parameters);
        }

        public ObjectMaintenanceModel getObjectMaintenanceByID(int mainId)
        {
            ObjectMaintenanceModel model = new ObjectMaintenanceModel();
            string query = "select * from [objectMaintenanceLog] where MainId = @MainId";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MainId", mainId),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.MainId = Convert.ToInt32(row["MainId"]);
                    model.ObjectID = Convert.ToInt32(row["ObjectID"]);
                    model.IssueComments = row["IssueComments"].ToString();
                    model.IssueDateTime = Convert.ToDateTime(row["IssueDateTime"]);
                    model.IssueAuthor = row["IssueAuthor"].ToString();
                    model.ResolvedComments = row["ResolvedComments"].ToString();
                    model.ResolvedDateTime = Convert.ToDateTime(row["ResolvedDateTime"]);
                    model.ResolvedPerson = row["ResolvedPerson"].ToString();
                    model.isActive = Convert.ToBoolean(row["isActive"]);
                }
            }
            return model;
        }

        public bool deleteObjectMaintenance(int mainId)
        {
            string query = "delete from [objectMaintenanceLog] where MainId = @MainId";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@MainId", mainId),
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
    }
}