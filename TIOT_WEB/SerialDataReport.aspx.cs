﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.BAL;
using TIOT_WEB.Common;
using TIOT_WEB.Models;

namespace TIOT_WEB
{
    public partial class SerialDataReport : System.Web.UI.Page
    {

        string alert = "";
        CommonBLL cObj = new CommonBLL();
        ReportsBLL obj = new ReportsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    { clearControls(); ddlClientbind(); }
                    if (Session["poweruser"] != null)
                    {
                        string ID = Session["poweruser"].ToString();
                        string[] powerSession = ID.Split(',');
                        if (powerSession[1] != "")
                        {
                            ddlclientdiv.Visible = false;
                            int clientID = Convert.ToInt32(powerSession[1]);
                            ddlGroupbind(clientID);
                        }
                    }
                    if (Session["user"] != null)
                    {
                        int loginID = Convert.ToInt32(Session["user"]);
                        if (loginID != 0)
                        {
                            ddlclientdiv.Visible = false;
                            ddlgroupdiv.Visible = false;
                            int groupID = cObj.getGroupIDForUser(loginID);
                            ddlObjectbind(groupID);
                        }
                    }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }

        #region button clicks
        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            string calender = txtdtrange.Text;
            string[] cal = calender.Split('-');
            string StrStartdate = cal[0]; string StrEnddate = cal[1];
            DateTime Startdate = Convert.ToDateTime(StrStartdate);
            DateTime Enddate = Convert.ToDateTime(StrEnddate);
            double min = 0.0;
            double max = 1000;
            gvdBind(Convert.ToInt32(ddlobjectSensor.SelectedValue), Startdate, Enddate, min, max); ;
            allowGridStaticMethods(ddlobjectSensor.SelectedItem.Text, ddlobject.SelectedItem.Text, Startdate, Enddate);
        }
        #endregion

        #region ddlBind
        public void ddlClientbind()
        {
            try
            {
                List<ClientIDName> list = cObj.getClientList();
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlclient, list, "Name", "ClientID", "Select"); }
                else
                { BindingClass.ClearDropDown(ddlclient, "Select"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlGroupbind(int clientId)
        {
            try
            {
                List<GroupIDName> list = cObj.getGroupList(clientId);
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlgroup, list, "Name", "GroupID", "Select"); }
                else
                { BindingClass.ClearDropDown(ddlgroup, "Select"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlObjectbind(int groupId)
        {
            try
            {
                List<ObjectListDashboard> list = cObj.getObjectList(groupId);
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlobject, list, "Name", "ObjectID", "Select"); }
                else
                { BindingClass.ClearDropDown(ddlobject, "Select"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlObjectSensorbind(int objectId)
        {
            try
            {
                List<ObjectSensorIDName> list = cObj.getRelaySensorByObject(objectId, "Serial");
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlobjectSensor, list, "Name", "ObjectSensorID", "Select"); }
                else
                { BindingClass.ClearDropDown(ddlobjectSensor, "Select"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion


        #region Binding Methods
        public void gvdBind(int ObjectSensorID, DateTime StartDate, DateTime EndDate, double min, double max)
        {
            List<IndividualSensorModel> Li = obj.getIndividualSensorReport(ObjectSensorID, StartDate, EndDate, min, max);
            if (Li.Count > 0)
            { BindingClass.GridViewBind(gvdReport, Li); }
            else { BindingClass.ClearGridView(gvdReport); }
        }
        #endregion

        #region ddlSelection Methods
        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclient.SelectedValue != "0")
            {
                int val = Convert.ToInt32(ddlclient.SelectedValue);
                ddlGroupbind(val);
            }
            else
            { BindingClass.ClearDropDown(ddlgroup, "Select"); }
        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlgroup.SelectedValue != "0")
            {
                int val = Convert.ToInt32(ddlgroup.SelectedValue);
                ddlObjectbind(val);
            }
            else
            { BindingClass.ClearDropDown(ddlobject, "Select"); }
        }

        protected void ddlobject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlobject.SelectedValue != "0")
            {
                int val = Convert.ToInt32(ddlobject.SelectedValue);
                ddlObjectSensorbind(val);
                BindingClass.CallScriptManager(this.Page, this.GetType(), "datetimepicker('#txtdtrange');");
            }
            else
            { BindingClass.ClearDropDown(ddlobjectSensor, "Select"); }
        }
        #endregion

        #region static Methods
        public void clearControls()
        {
            BindingClass.ClearDropDown(ddlclient, "Select");
            BindingClass.ClearDropDown(ddlgroup, "Select");
            BindingClass.ClearDropDown(ddlobject, "Select");
            BindingClass.ClearDropDown(ddlobjectSensor, "Select");
        }

        public void allowGridStaticMethods(string objectSensorName, string objectName, DateTime startdt, DateTime enddt)
        {
            gvdReport.Visible = true;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "datetimepicker('#txtdtrange'); gridtoJson('gvdReport');gridhtml('#gvdReport','" + objectSensorName + "','" + objectName + "','" + startdt + "','" + enddt + "');", true);
        }
        #endregion

    }
}
