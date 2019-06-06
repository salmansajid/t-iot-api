using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.Models;
using TIOT_WEB.Service;

namespace TIOT_WEB
{
    public partial class TavlIntegration : System.Web.UI.Page
    {
        #region  ClassIntances and Varible Declerations
        public string Alert = "";
        ObjectService OBJ = new ObjectService();
        ClientService CS = new ClientService();
        GroupService GS = new GroupService();
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
            try
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
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlgroup(int ClientId)
        {
            try
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
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlobject(int GroupId)
        {
            try
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
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void Gridbind(int ObjectId)
        {
            try
            {
                List<TavlIntegrationModel> li = OBJ.GetTavlStatusByObjectId(ObjectId); BindingClass.GridViewBind(gvdTavlIntegration, li);
                gvdTavlIntegration.Visible = true;
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }



        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                { var _sltdval = ddlClient.SelectedValue; int val = Convert.ToInt32(_sltdval); ddlgroup(val); }
                else
                { BindingClass.ClearDropDown(ddlGroup, "Select Branch"); BindingClass.ClearDropDown(ddlObject, "Select Device"); }
                allowStaticMethods("staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedValue != "0")
                { var _sltdval = ddlGroup.SelectedValue; int val = Convert.ToInt32(_sltdval); ddlobject(val); }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
                allowStaticMethods("staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }

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
        #endregion

        #region LinkButton Clicks

        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                try
                {
                    int _cmdArg = Convert.ToInt32(e.CommandArgument); bool response = OBJ.PutTavlObjectStatus(_cmdArg);
                    if (response == true) { Alert = AlertsClass.SuccessRemove; }
                    else { Alert = AlertsClass.ErrorWentWrong; }
                    Gridbind(Convert.ToInt32(ddlObject.SelectedValue));
                    allowStaticMethods("ALerts('" + Alert + "'); staticMethod('Disable');");
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

            }
        }
        #endregion

        #region Button Clicks
        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTavlClientID.Text != null && txtTavlGroupID.Text != null && txtTavlIP.Text != "")
                {
                    if (btnadd.Text == "Integrated")
                    {
                        bool TavlStatus = chkEnable.Checked ? true : false;
                        int ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                        int TavlClient = Convert.ToInt32(txtTavlClientID.Text);
                        int TavlGroup = Convert.ToInt32(txtTavlGroupID.Text);
                        string TavlIP = txtTavlIP.Text;
                        bool response = OBJ.PutTavlObject(ObjectID, TavlClient, TavlGroup, TavlIP, TavlStatus);
                        if (response == true)
                        { Alert = AlertsClass.SuccessAdd; Gridbind(ObjectID); }
                        txtTavlClientID.Text = string.Empty; txtTavlGroupID.Text = string.Empty; txtTavlIP.Text = string.Empty;
                    }
                }
                else
                { Alert = AlertsClass.ErrorRequired; }
                allowStaticMethods("ALerts('" + Alert + "'); staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            ddlclient();
            allowStaticMethods("staticMethod('Disable');");
        }
        #endregion

        #region  Clear Methods
        public void clearControls()
        {
            BindingClass.ClearDropDown(ddlClient, "Select Client");
            BindingClass.ClearDropDown(ddlGroup, "Select Branch");
            BindingClass.ClearDropDown(ddlObject, "Select Device");
            txtTavlClientID.Text = string.Empty;
            txtTavlGroupID.Text = string.Empty;
            txtTavlIP.Text = string.Empty;
            gvdTavlIntegration.Visible = false;
        }
        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
        #endregion

    }
}