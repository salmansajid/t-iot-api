using Newtonsoft.Json;
using System;
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
    public partial class DeviceRelaysReport : System.Web.UI.Page
    {
        #region pageload & static variables
        CommonBLL cObj = new CommonBLL();
        ReportsBLL obj = new ReportsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    clearControls();
                    if (Session["admin"] != null)
                    { ddlClientbind(); }
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
        #endregion


        #region ddlSelection Methods
        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclient.SelectedValue != "0")
                {
                    int val = Convert.ToInt32(ddlclient.SelectedValue);
                    ddlGroupbind(val);
                }
                else
                { BindingClass.ClearDropDown(ddlgroup, "Select"); }
                hideControls();
                allowStaticMethods("staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }

        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlgroup.SelectedValue != "0")
                {
                    int val = Convert.ToInt32(ddlgroup.SelectedValue);
                    ddlObjectbind(val);
                }
                else
                { BindingClass.ClearDropDown(ddlobject, "Select"); }
                hideControls();
                allowStaticMethods("staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        #endregion

        #region button clicks
        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                string calender = txtdtrange.Text;
                string[] cal = calender.Split('-');
                string StrStartdate = cal[0]; string StrEnddate = cal[1];
                DateTime Startdate = Convert.ToDateTime(StrStartdate);
                DateTime Enddate = Convert.ToDateTime(StrEnddate);
                string rt = gvdBind(Convert.ToInt32(ddlobject.SelectedValue), Startdate, Enddate);
                allowStaticMethods("chartCRR('" + rt + "'); staticMethod();");

                //allowStaticMethods("staticMethod();gridtoJson('gvdReport');gridhtml('#gvdReport','" + ddlobjectSensor.SelectedItem.Text + " Current','" + ddlobject.SelectedItem.Text + "','" + Startdate + "','" + Enddate + "');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        #endregion


        #region Binding Methods
        public string gvdBind(int ObjectID, DateTime StartDate, DateTime EndDate)
        {
            try
            {
                var jsonC = "";
                var jsonV = "";
                List<ObjectSensorIDName> List_Current = cObj.getRelaySensorByObject(ObjectID, "amp");
                List<ObjectSensorIDName> List_Volt = cObj.getRelaySensorByObject(ObjectID, "volt");
                string[] itemListC = new string[List_Current.Count];
                string[] itemListV = new string[List_Current.Count];
                if (List_Current.Count > 0)
                {
                    for (int i = 0; i < List_Current.Count; i++)
                    {
                        object Name = i;
                        List<IndividualSensorModel> LiC = obj.getIndividualSensorReport(List_Current[i].ObjectSensorID, StartDate, EndDate, 0, 4);
                        List<IndividualSensorModel> LiV = obj.getIndividualSensorReport(List_Volt[i].ObjectSensorID, StartDate, EndDate, 150, 300);
                        jsonC = JsonConvert.SerializeObject(LiC);
                        itemListC[i] = (jsonC);
                        jsonV = JsonConvert.SerializeObject(LiV);
                        itemListV[i] = (jsonV);
                    }
                    string resultC = string.Join("&", itemListC);
                    string resultV = string.Join("&", itemListV);
                    string result = resultC + '*' + resultV;
                    return result;
                }

            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            return "";
        }
        #endregion


        #region static Methods
        public void clearControls()
        {
            BindingClass.ClearDropDown(ddlclient, "Select");
            BindingClass.ClearDropDown(ddlgroup, "Select");
            BindingClass.ClearDropDown(ddlobject, "Select");
            ddlYear.SelectedIndex = 0;
            hideControls();
            allowStaticMethods("staticMethod();");
        }
        public void hideControls()
        {
            //BindingClass.ClearGridView(gvdReport);
            //gvdReport.Visible = false;
            //btngraph.Visible = false;
        }
        public void allowStaticMethods(string jsFunction)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsFunction); }

        public void allowGridStaticMethods(string objectSensorName, string objectName, DateTime startdt, DateTime enddt)
        {
            //gvdReport.Visible = true;
            //BindingClass.CallScriptManager(this.Page, this.GetType(), "datetimepicker('#txtdtrange'); gridtoJson('gvdReport');gridhtml('#gvdReport','" + objectSensorName + " Current','" + objectName + "','" + startdt + "','" + enddt + "');");
        }
        #endregion
    }
}