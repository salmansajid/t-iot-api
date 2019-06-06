
using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class LoginGroup : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string Alert = "";
        LoginGroupService LGS = new LoginGroupService();
        LoginService LS = new LoginService();
        GroupService GS = new GroupService();
        CommonBLL cobj = new CommonBLL();
        LoginGroupBLL obj = new LoginGroupBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    {ddlClientBind();}
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region Button Clicks
        protected void btnAddLoginGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedValue != "0" && ddlLogin.SelectedValue != "0")
                {
                    LoginGroupModel model = new LoginGroupModel();
                    model.LoginGroupID = 0;
                    model.LoginID = Convert.ToInt32(ddlLogin.SelectedValue);
                    model.GroupID = Convert.ToInt32(ddlGroup.SelectedValue);
                    if (btnAddLoginGroup.Text == "Save")
                    {
                        bool status = obj.postLoginGroup(model);
                        if (status == true)
                        {Alert = AlertsClass.SuccessAdd;}
                        else
                        {Alert = AlertsClass.ErrorWentWrong;}
                    }
                    if (btnAddLoginGroup.Text == "Update")
                    {

                        model.LoginGroupID = Convert.ToInt32(Session["loginGroupId"]);
                        bool status = obj.postLoginGroup(model);
                        if (status == true)
                        {
                            btnAddLoginGroup.Text = "Save";
                            Session.Remove("loginGroupId");
                            Alert = AlertsClass.SuccessUpdate;
                        }
                        else
                        { Alert = AlertsClass.ErrorWentWrong; }
                    }
                    gridBind();
                    ddlLogin.SelectedValue = "0";
                }
                else
                {Alert = AlertsClass.ErrorRequired;}
                allowStaticMethods("ALerts('" + Alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            allowStaticMethods("staticMethod();");
        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    allowStaticMethods("staticMethod();");
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    LoginGroupModel list = obj.getLoginGroupByID(cmdArg);
                    ddlGroup.SelectedValue = list.GroupID.ToString();
                    ddlLogin.SelectedValue = list.LoginID.ToString();
                    Session["loginGroupId"] = cmdArg.ToString();
                    btnAddLoginGroup.Text = "Update";
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
     
        }
        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveID")
                {
                    string user = Convert.ToString(e.CommandArgument);
                    Session["loginGroupId"] = Convert.ToInt32(user);
                    allowStaticMethods("openDeleteModal();staticMethod();");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int ID = Convert.ToInt32(Session["loginGroupId"]);
                    bool status = LGS.DeleteLoginGroup(ID);
                    if (status == true)
                    { Alert = AlertsClass.SuccessRemove; }
                    else
                    { Alert = AlertsClass.ErrorWentWrong; }
                    gridBind();
                    allowStaticMethods("ALerts('" + Alert + "');staticMethod();");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Binding Controls
        public void gridBind()
        {
            try
            {
                if (ddlLogin.SelectedValue != "0")
                {
                    List<LoginGroupModel> list = obj.getLoginGroupByLogin(Convert.ToInt32(ddlLogin.SelectedValue));
                    BindingClass.GridViewBind(gvdLoginGroup, list);
                    gvdLoginGroup.Visible = true;
                }
                else
                { gvdLoginGroup.Visible = false; }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void ddlClientBind()
        {
            try
            {
                List<ClientIDName> list = cobj.getClientList();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
                    BindingClass.ClearDropDown(ddlLogin, "Select User");
                    BindingClass.ClearDropDown(ddlGroup, "Select Branch");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlGroupBind(int clientID)
        {
            try
            {
                List<GroupIDName> list = cobj.getGroupList(clientID);
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlGroup, list, "Name", "GroupID", "Select Branch");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlLoginBind(int clientID)
        {
            try
            {
                List<LoginIDName> list = cobj.getLoginList(clientID);
                if (list.Count > 0)
                {BindingClass.BindDropDown(ddlLogin, list, "Name", "LoginID", "Select User");}
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion

        #region ddl Selection change
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                {
                    ddlLoginBind(Convert.ToInt32(ddlClient.SelectedValue));
                    ddlGroupBind(Convert.ToInt32(ddlClient.SelectedValue));
                    gvdLoginGroup.Visible = false;
                    allowStaticMethods("staticMethod();");
                }
            }
            catch (Exception)
            {BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }

        protected void ddlLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlLogin.SelectedValue != "0")
                {gridBind();}
                else
                { gvdLoginGroup.Visible = false; }
                allowStaticMethods("staticMethod();");
            }
            catch (Exception)
            {BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType());}
        }
        #endregion

        #region  Clear Methods
        public void clearControls()
        {
            ddlLogin.SelectedIndex = 0;
            ddlGroup.SelectedIndex = 0;
            gvdLoginGroup.Visible = false;
            btnAddLoginGroup.Text = "Save";
        }
        public void allowStaticMethods(string jsfunctions)
        {BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);}
        #endregion

    }

}
