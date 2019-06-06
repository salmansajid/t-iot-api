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
    public partial class User : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string alert = "";
        LoginService LS = new LoginService();
        ClientService CS = new ClientService();

        CommonBLL cobj = new CommonBLL();
        LoginBLL obj = new LoginBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                clearControls();
                try
                {
                    if (Session["admin"] != null)
                    { ddlClientBind(); }
                    else { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }

        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    allowStaticMethods("applyDatatable('.gvduserclass');staticMethod('Enable');");
                    int ID = Convert.ToInt32(e.CommandArgument);
                    PostLoginModel li = obj.getLoginByLoginID(ID);
                    txtUser.Text = li.User;
                    txtPassword.Attributes.Add("value", li.Password);
                    txtConfirmPassword.Attributes.Add("value", li.Password);
                    txtComments.Text = li.Comment;
                    txtdisplayName.Text = li.DisplayName;
                    ddlUserType.SelectedValue = li.RoleID.ToString();
                    ddlClient.SelectedValue = li.ClientID.ToString();
                    Session["loginId"] = ID.ToString();
                    btnAddUser.Text = "Update";
                    txtUser.Enabled = false;
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
                    int ID = Convert.ToInt32(e.CommandArgument);
                    Session["LoginID"] = ID;
                    allowStaticMethods("openDeleteModal();applyDatatable('.gvduserclass');  staticMethod('Disable');");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                try
                {
                    int ID = Convert.ToInt32(Session["LoginID"]);
                    bool status = obj.disableLogin(ID);
                    if (status == true) { alert = AlertsClass.SuccessRemove; }
                    else { alert = AlertsClass.ErrorWentWrong; }
                    gridBind(Convert.ToInt32(ddlClient.SelectedValue));
                    Session.Remove("LoginID");
                    allowStaticMethods("ALerts('" + alert + "');closeDeleteModal();applyDatatable('.gvduserclass'); staticMethod('Disable');");
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }

        #endregion

        #region Button Clicks
        //Button clicks
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0" && txtUser.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "")
                {
                    PostLoginModel model = new PostLoginModel();
                    model.ClientID = Convert.ToInt32(ddlClient.SelectedValue);
                    model.RoleID = Convert.ToInt32(ddlUserType.SelectedValue);
                    model.User = txtUser.Text;
                    model.Password = txtPassword.Text;
                    model.Comment = txtComments.Text;
                    model.DisplayName = txtdisplayName.Text;

                    if (txtPassword.Text == txtConfirmPassword.Text)
                    {
                        if (btnAddUser.Text == "Save")
                        {
                            model.LoginID = 0;
                            bool exist = obj.usernameExist(model.User);
                            if (exist == false)
                            {
                                bool status = obj.postLogin(model);
                                if (status == true)
                                { alert = AlertsClass.SuccessAdd; }
                                else
                                { alert = AlertsClass.ErrorPasswordMatch; }
                            }
                            else
                            { alert = AlertsClass.ErrorExist("Username"); }
                        }
                        if (btnAddUser.Text == "Update")
                        {
                            model.LoginID = Convert.ToInt32(Session["LoginId"]);
                            bool status = obj.postLogin(model);
                            if (status == true)
                            { alert = AlertsClass.SuccessUpdate; }
                        }
                    }
                    else
                    { alert = AlertsClass.ErrorPasswordMatch; }
                    clearControls();
                    gridBind(Convert.ToInt32(ddlClient.SelectedValue));
                    allowStaticMethods(" ALerts('" + alert + "'); applyDatatable('.gvduserclass'); staticMethod('Disable');");
                }
                else
                { alert = AlertsClass.ErrorRequired; }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            ddlClientBind();
            allowStaticMethods("staticMethod('Disable');");
        }
        #endregion

        #region Binding Controls

        public void gridBind(int ClientID)
        {
            try
            {
                List<GetLoginModel> list = obj.getLoginByClient(ClientID);
                BindingClass.GridViewBind(GvdUser, list);
                GvdUser.Visible = true;
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
                { BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client"); }
                else
                { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                {
                    clearControls();
                    gridBind(Convert.ToInt32(ddlClient.SelectedValue));
                }
                else
                { clearControls(); }
                allowStaticMethods(" applyDatatable('.gvduserclass'); staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Text and Dropdown Clear Method

        public void clearControls()
        {
            ddlUserType.SelectedIndex = -1;
            txtUser.Text = string.Empty;
            txtComments.Text = string.Empty;
            txtPassword.Attributes.Add("value", "");
            txtConfirmPassword.Attributes.Add("value", "");
            txtdisplayName.Text = string.Empty;
            btnAddUser.Text = "Save";
            txtUser.Enabled = true;
            Session.Remove("LoginId");
            GvdUser.Visible = false;

        }

        public void allowStaticMethods(string jsFunction)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsFunction); }
        #endregion



    }
}

