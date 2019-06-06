using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.BAL;
using TIOT_WEB.Common;
using TIOT_WEB.Models;

namespace TIOT_WEB
{
    public partial class ConsumptionReport : System.Web.UI.Page
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

       

        #region ddlSelectionchange
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
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            allowStaticMethods("staticMethod();");
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
                
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            allowStaticMethods("staticMethod();");
        }
        #endregion  

        #region gridbind
        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            string calender = txtdtrange.Text;
            string[] cal = calender.Split('-');
            string StrStartdate = cal[0]; string StrEnddate = cal[1];
            DateTime Startdate = Convert.ToDateTime(StrStartdate);
            DateTime Enddate = Convert.ToDateTime(StrEnddate);
            gvdBind_Consumption(Convert.ToInt32(ddlobject.SelectedValue), Startdate, Enddate);
            allowStaticMethods("staticMethod();gridhtml('#Gvdconsumptionreport','Consumption Report','" + ddlobject.SelectedItem.Text + "', '" + Startdate + "', '" + Enddate + "');");
        }

        public void gvdBind_Consumption(int ObjectId, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate.Date == DateTime.Now.Date)
            {
                List<DTReportModel> li = new List<DTReportModel>();
                List<SwitchesReportDayModel> li_current = obj.getConsumptionToday(ObjectId, "Current");
                if (li_current.Count > 0)
                {
                    List<SwitchesReportDayModel> li_voltage = obj.getConsumptionToday(ObjectId, "Voltage");
                    for (int i = 0; i < li_current.Count; i++)
                    {
                        DTReportModel model = new DTReportModel();
                        model.Name = li_current[i].Name;
                        model.Current = li_current[i].Value;
                        model.Voltage = li_voltage[i].Value;
                        model.Power = string.Format("{0:0.00}", (li_current[i].Value * li_voltage[i].Value) / 1000);
                        li.Add(model);
                    }
                }
                BindingClass.GridViewBind(Gvdconsumptionreport, li);
            }
            else
            {
                List<SwitchesReportConsumptionModel> li = obj.getConsumptionByDT(ObjectId, StartDate, EndDate);
                BindingClass.GridViewBind(Gvdconsumptionreport, li);
            }
            Gvdconsumptionreport.Visible = true;
        }

        protected void Gvdconsumptionreport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                string calender = txtdtrange.Text;
                string[] cal = calender.Split('-');
                string StrStartdate = cal[0]; string StrEnddate = cal[1];
                DateTime Startdate = Convert.ToDateTime(StrStartdate);
                DateTime Enddate = Convert.ToDateTime(StrEnddate);
                if (Startdate == DateTime.Now.Date && Enddate == DateTime.Now.Date.AddDays(1))
                {
                    e.Row.Cells[1].Text = "Average Current (A)";
                    e.Row.Cells[2].Text = "Average Voltage (V)";
                    e.Row.Cells[3].Text = "Average Power (KW)";
                }
                else
                {
                    e.Row.Cells[1].Text = "Current (A)";
                    e.Row.Cells[2].Text = "Voltage (V)";
                    e.Row.Cells[3].Text = "Power (KW)";
                }
            }

        }



        #endregion


        #region static Methods
        public void clearControls()
        {
            BindingClass.ClearDropDown(ddlclient, "Select");
            BindingClass.ClearDropDown(ddlgroup, "Select");
            BindingClass.ClearDropDown(ddlobject, "Select");
            hideControls();
            allowStaticMethods("staticMethod();");
        }
        public void hideControls()
        {
            BindingClass.ClearGridView(Gvdconsumptionreport);
            Gvdconsumptionreport.Visible = false;
            //btngraph.Visible = false;
        }
        public void allowStaticMethods(string jsFunction)
        {BindingClass.CallScriptManager(this.Page, this.GetType(), jsFunction);}
        #endregion

    }
}