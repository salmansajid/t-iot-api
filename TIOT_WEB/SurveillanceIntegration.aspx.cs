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
    public partial class SurveillanceIntegration : System.Web.UI.Page
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
                {
                    Alert = AlertsClass.ErrorWentWrong;
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ALerts('" + Alert + "')", true);
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
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGroup.SelectedValue != "0")
            { var _sltdval = ddlGroup.SelectedValue; int val = Convert.ToInt32(_sltdval); ddlobject(val); }
            else
            { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
        }
        #endregion 
    }
}