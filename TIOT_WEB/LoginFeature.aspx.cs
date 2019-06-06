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
    public partial class LoginFeature : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        string Alert = "";
        LoginFeatureService lfs = new LoginFeatureService();
        FeatureService fs = new FeatureService();
        LoginService ls = new LoginService();
        LoginFeatureBLL obj = new LoginFeatureBLL();
        CommonBLL cobj = new CommonBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    {
                        ddlClientBind();
                    }
                    else
                    {Response.Redirect("Login.aspx");}
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region Button Clicks
        protected void btnAddLoginfeature_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlLogin.SelectedValue != "0" && ddlFeature.SelectedValue != "0")
                {
                    LoginFeatureModel model = new LoginFeatureModel();
                    model.LoginFeatureID = 0;
                    model.LoginID = Convert.ToInt32(ddlLogin.SelectedValue);
                    model.FeatureID = Convert.ToInt32(ddlFeature.SelectedValue);
                    bool status = obj.postLoginFeature(model);
                    if (status == true)
                    {Alert = AlertsClass.SuccessAdd;}
                    else
                    {Alert = AlertsClass.ErrorWentWrong;}
                }
                else
                {Alert = AlertsClass.ErrorRequired;}
                gridBind(Convert.ToInt32(ddlLogin.SelectedValue));
                ddlFeatureBind(Convert.ToInt32(ddlLogin.SelectedValue));
                allowStaticMethods("ALerts('" + Alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlClientBind();
            allowStaticMethods("staticMethod();");
        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveID")
                {
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    bool status = obj.deleteLoginFeature(cmdArg);
                    if (status == true)
                    {
                        gridBind(Convert.ToInt32(ddlLogin.SelectedValue));
                        ddlFeatureBind(Convert.ToInt32(ddlLogin.SelectedValue));
                        Alert = AlertsClass.SuccessRemove;
                    }
                    else
                    {Alert = AlertsClass.ErrorWentWrong;}
                    allowStaticMethods("ALerts('" + Alert + "');staticMethod();");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    string Alert = "";
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        CheckBox gvdstatus = row.FindControl("gvdcheckEnable") as CheckBox;
                        bool Enable = gvdstatus.Checked ? true : false;
                        int _cmdArg = Convert.ToInt32(e.CommandArgument);
                        bool status = obj.E_D_LoginFeature(_cmdArg, Enable);
                        if (status == true)
                        {
                            gridBind(Convert.ToInt32(ddlLogin.SelectedValue));
                            ddlFeatureBind(Convert.ToInt32(ddlLogin.SelectedValue));                            
                            Alert = AlertsClass.SuccessUpdate;
                        }
                        else
                        {Alert = AlertsClass.ErrorWentWrong;}
                        allowStaticMethods("ALerts('" + Alert + "');staticMethod();");
                    }
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }            
        }
        #endregion

        #region Binding Controls
        public void gridBind(int LoginID)
        {
            try
            {
                List<LoginFeatureModel> list = obj.getFeatureByLogin(LoginID);
                BindingClass.GridViewBind(gvdLoginFeature, list);
                gvdLoginFeature.Visible = true;
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
                    BindingClass.ClearDropDown(ddlFeature, "Select Feature");
                    gvdLoginFeature.Visible = false;
                }
                else
                { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
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
                { BindingClass.BindDropDown(ddlLogin, list, "Name", "LoginID", "Select User"); }
                else
                { BindingClass.ClearDropDown(ddlLogin,"Select User");}
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlFeatureBind(int loginID)
        {
            try
            {
                List<FeatureIDName> list = obj.getNAFeatureByLogin(loginID);
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlFeature, list, "Name", "FeatureID", "Select Feature");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlLogin.SelectedValue != "0")
                {
                    ddlFeatureBind(Convert.ToInt32(ddlLogin.SelectedValue));
                    gridBind(Convert.ToInt32(ddlLogin.SelectedValue));
                    allowStaticMethods("staticMethod();");

                }
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
                    ddlLoginBind(Convert.ToInt32(ddlClient.SelectedValue));
                    gvdLoginFeature.Visible = false;
                    allowStaticMethods("staticMethod();");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
        #endregion

       
    }
}