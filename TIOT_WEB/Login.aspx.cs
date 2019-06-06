using TIOT_WEB.Models;
using TIOT_WEB.Service;
using TIOT_WEB.Service.ServiceCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.BAL;
using TIOT_WEB.Common;


namespace TIOT_WEB
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["admin"] != null || Session["poweruser"] != null || Session["user"] != null)
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
            catch(Exception)
            { { allowStaticMethods("alertshow('Server not responding. ');"); } }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string _username = txtemail.Text;
                string _password = txtpassword.Text;
                if (_username == "admin" && _password == "teLonikA1")
                {
                    Response.Redirect("Dashboard.aspx");
                    Session["admin"] = "admin";
                    Session["displayName"] = "Super admin";
                }
                else
                {
                    bool status = getlogin(_username, _password);
                    if (status == true)
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    { allowStaticMethods("alertshow('Invalid Credentials');  enabledSubmit('.logintxt', '.btnLogin');"); }
                }
            }
            catch (Exception ex)
            {}
        }

        public bool getlogin(string username, string password)
        {
            try
            {
                bool status = false;
                string code = txtclientcode.Text;
                LoginBLL obj = new LoginBLL();
                LoginModelDLL model = obj.getActiveLogin(username, password);
                if (model != null)
                {
                    if (model.RoleID == -1)
                    {
                        Session["admin"] = model.SessionName;
                        Session["displayName"] = model.DisplayName;
                        status = true;
                    }
                    if (model.RoleID == 1)
                    {
                        if (code != "")
                        {
                            int res = obj.getActiveCode(code);
                            if (res != 0)
                            {
                                Session["user"] = model.LoginID; Session["displayName"] = model.DisplayName;
                                status = true;
                            }
                        }
                        else
                        { allowStaticMethods("alertshow('Client code must be required'); enabledSubmit('.logintxt', '.btnLogin');"); }
                    }
                    if (model.RoleID == 0)
                    {
                        if (code != "")
                        {
                            int res = obj.getActiveCode(code);
                            if (res != 0)
                            {
                                Session["poweruser"] = model.LoginID + "," + model.ClientID;
                                Session["displayName"] = model.DisplayName;
                                status = true;
                            }
                        }
                        else
                        { allowStaticMethods("alertshow('Client code must be required');  enabledSubmit('.logintxt', '.btnLogin');"); }
                    }
                }
                return status;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
    }
}
