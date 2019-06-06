using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.BAL;
using TIOT_WEB.Common;
using TIOT_WEB.Models;
using TIOT_WEB.Service;

namespace TIOT_WEB
{
    public partial class AttendanceIntegration : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string Alert = "";
        ObjectService OBJ = new ObjectService();
        ClientService CS = new ClientService();
        GroupService GS = new GroupService();
        DashboardBLL DashBLL = new DashboardBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    { ddlclient(); BindingClass.ClearDropDown(ddlGroup, "Select Branch"); BindingClass.ClearDropDown(ddlObject, "Select Device"); }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

            }
        }
        #endregion

        #region Binding Controls
        public void ddlclient()
        {
            List<ClientModel> li = CS.GetClient();
            if (li != null)
            {
                var list = li.Select(o => new { o.Name, o.ClientID });
                BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
            }
            else
            { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
        }
        public void ddlgroup(int ClientId)
        {
            List<GroupModel> li = GS.GetGroupByClientId(ClientId);
            if (li != null)
            {
                var list = li.Select(o => new { o.Name, o.GroupID });
                BindingClass.BindDropDown(ddlGroup, list, "Name", "GroupID", "Select Branch");
            }
            else
            { BindingClass.ClearDropDown(ddlGroup, "Select Branch"); }
        }
        public void ddlobject(int GroupId)
        {
            List<ObjectModel> li = OBJ.GetObjectsByGroupId(GroupId);
            if (li != null)
            {
                var list = li.Select(o => new { o.Name, o.ObjectID });
                BindingClass.BindDropDown(ddlObject, list, "Name", "ObjectID", "Select Device");
            }
            else
            { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlClient.SelectedValue != "0")
            {  var _sltdval = ddlClient.SelectedValue; int val = Convert.ToInt32(_sltdval); ddlgroup(val); }
            else
            { BindingClass.ClearDropDown(ddlGroup, "Select Branch"); BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            allowStaticMethods("staticMethod('Disable');");
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue != "0")
            { var _sltdval = ddlGroup.SelectedValue; int val = Convert.ToInt32(_sltdval); ddlobject(val); }
            else
            { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            allowStaticMethods("staticMethod('Disable');");

        }
        #endregion

        #region Button Clicks
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            ddlclient();
            //allowStaticMethods("staticMethod('Disable');");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAttendanceClientID.Text != null && txtAttendanceIP.Text != "")
                {
                    if (btnadd.Text == "Integrated")
                    {
                        bool attendanceStatus = chkEnable.Checked ? true : false;
                        int ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                        int attendanceClient = Convert.ToInt32(txtAttendanceClientID.Text);
                        string attendanceIP = txtAttendanceIP.Text;
                        bool response = OBJ.PutAttendanceObject(ObjectID, attendanceClient, attendanceIP, attendanceStatus);
                        if (response == true)
                        { Alert = AlertsClass.SuccessAdd; Gridbind(ObjectID); }
                        txtAttendanceClientID.Text = string.Empty; txtAttendanceIP.Text = string.Empty;
                    }
                }
                else
                { Alert = AlertsClass.ErrorRequired; }
                allowStaticMethods("ALerts('" + Alert + "'); staticMethod(); ");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        } 
        #endregion

        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlObject.SelectedValue != "0")
                { Gridbind(Convert.ToInt32(ddlObject.SelectedValue)); }
                allowStaticMethods("staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void Gridbind(int ObjectId)
        {
            try
            {
                List<AttendanceIntegrationModel> li = DashBLL.getActiveAttendanceByObj(ObjectId); BindingClass.GridViewBind(gvdAttendanceIntegration, li);
                gvdAttendanceIntegration.Visible = true;
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }

        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
       
            if (e.CommandName == "Remove")
            {
                try
                {
                    int _cmdArg = Convert.ToInt32(e.CommandArgument); bool response = OBJ.PutAttendanceObjectStatus(_cmdArg);
                    if (response == true) { Alert = AlertsClass.SuccessRemove; }
                    else { Alert = AlertsClass.ErrorWentWrong; }
                    Gridbind(Convert.ToInt32(ddlObject.SelectedValue));
                    allowStaticMethods("ALerts('" + Alert + "'); staticMethod();");
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }

        public void clearControls()
        {
            BindingClass.ClearDropDown(ddlClient, "Select Client");
            BindingClass.ClearDropDown(ddlGroup, "Select Branch");
            BindingClass.ClearDropDown(ddlObject, "Select Device");
            txtAttendanceIP.Text = string.Empty;
            txtAttendanceClientID.Text = string.Empty;
            gvdAttendanceIntegration.Visible = false;
            allowStaticMethods("staticMethod(); ");
        }
    }
}