
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Models;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class Feature : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string alert = "";
        ConfigurationBLL obj = new ConfigurationBLL();
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

                    }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

            }
        }
        #endregion

        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RemoveID")
            {
                int cmdArg = Convert.ToInt32(e.CommandArgument);
                bool status = obj.disableFeature(cmdArg);
                if(status == true)
                { alert = AlertsClass.SuccessRemove;}
                else
                { alert = AlertsClass.ErrorWentWrong;}
                gridBind(ddlType.SelectedItem.Text);
                allowStaticMethods("ALerts('"+ alert +"');applyDatatable('.gvdFeatureClass')");
            }
        }

        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "UpdateID")
            {
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        CheckBox checkstatus = row.FindControl("chkstatus") as CheckBox;
                        bool cbstatus = checkstatus.Checked ? true : false;
                        TextBox name = row.FindControl("txtName") as TextBox;
                        TextBox cssclass = row.FindControl("txtcssclass") as TextBox;
                        int cmdArg = Convert.ToInt32(e.CommandArgument);
                         bool status = obj.putFeature(cmdArg, name.Text,cssclass.Text, cbstatus);
                         if (status == true)
                        {alert = AlertsClass.SuccessUpdate;}
                        else
                        {alert = AlertsClass.ErrorWentWrong;}
                         gridBind(ddlType.SelectedItem.Text);
                         allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdFeatureClass')");
                    }
                
                
            }
        } 

        #region Binding Controls
        public void gridBind(string type)
        {
            try
            {
                List<ConfigurationModel> list = new List<ConfigurationModel>();
                if (type == "Reports")
                { list = obj.getFeatureList(-1); }
                if (type == "Configuration")
                { list = obj.getFeatureList(0); }
                BindingClass.GridViewBind(gvdFeatures, list);
                gvdFeatures.Visible = true;
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlType.SelectedItem.Text != "")
                {
                    gridBind(ddlType.SelectedItem.Text);
                    allowStaticMethods("applyDatatable('.gvdFeatureClass')");
                }
                else
                {gvdFeatures.Visible = false;}

            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
    }
}