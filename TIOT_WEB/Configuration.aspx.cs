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
    public partial class Configuration : System.Web.UI.Page
    {
        FeatureService FS = new FeatureService();
        LoginFeatureService LFS = new LoginFeatureService();
        ConfigurationBLL obj = new ConfigurationBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["admin"] != null)
                    {
                        configRptBind();
                    }
                    else if (Session["poweruser"] != null)
                    {
                        string ID = Session["poweruser"].ToString();
                        string[] powerSession = ID.Split(',');
                        int loginID = Convert.ToInt32(powerSession[0]);
                        if (loginID != 0)
                        {
                            configRptBindByLoginID(loginID);
                        }
                    }
                    else if (Session["user"] != null)
                    {
                        int loginID = Convert.ToInt32(Session["user"]);
                        if (loginID != 0)
                        {
                            configRptBindByLoginID(loginID);
                        }
                    }
                    else
                    {
                        Response.Redirect("Login.aspx");

                    }
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void configRptBind()
        {
            try
            {
                List<ConfigurationModel> list = obj.getConfigurationList(0);
                if (list.Count > 0)
                {
                    BindingClass.RepeaterViewBind(rptConfiguration, list);
                }
                else
                {
                    BindingClass.ClearRepeaterView(rptConfiguration);
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "toastr.error('No Configuration panel Found!', 'N/A',{positionClass:'toast-bottom-right'});");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void configRptBindByLoginID(int LoginID)
        {
            try
            {
                List<FeatureConfigurationModel> list = obj.getFeatureListByLogin(LoginID, 0);
                if (list.Count > 0)
                {
                    BindingClass.RepeaterViewBind(rptConfiguration, list);
                }
                else
                {
                    BindingClass.ClearRepeaterView(rptConfiguration);
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "toastr.error('No Configuration panel assigned to " + Session["DisplayName"] + "!', 'N/A',{positionClass:'toast-bottom-right'});");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void rptConfiguration_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("lnkbtnviewAddress"))
                {
                    string val = Convert.ToString(e.CommandArgument);
                    Response.Redirect(val);
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

    }
}