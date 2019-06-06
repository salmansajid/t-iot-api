using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.BAL;
using TIOT_WEB.Common;
using TIOT_WEB.Models;

namespace TIOT_WEB
{
    public partial class ReportConfiguration : System.Web.UI.Page
    {
        ConfigurationBLL obj = new ConfigurationBLL();
        //getConfigurationList
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Session["admin"] != null)
                    {
                        bindRepeater();
                    }
                    else if (Session["poweruser"] != null)
                    {
                        string ID = Session["poweruser"].ToString();
                        string[] powerSession = ID.Split(',');
                        int loginID = Convert.ToInt32(powerSession[0]);
                        if (loginID != 0)
                        {
                            bindRepeaterByLoginID(loginID);
                        }
                    }
                    else if (Session["user"] != null)
                    {
                        int loginID = Convert.ToInt32(Session["user"]);
                        if (loginID != 0)
                        {
                            bindRepeaterByLoginID(loginID);
                        }
                    }
                    else
                    {Response.Redirect("Login.aspx");}
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void bindRepeater()
        {
            try
            {
                List<ConfigurationModel> list = obj.getConfigurationList(-1);
                if (list.Count > 0)
                {
                    rptReports.DataSource = list;
                    rptReports.DataBind();
                }
                else
                {
                    BindingClass.ClearRepeaterView(rptReports);
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "toastr.error('No Report assigned to " + Session["DisplayName"] + "!', 'N/A',{positionClass:'toast-bottom-right'});");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void bindRepeaterByLoginID(int LoginID)
        {
              try
            {
                List<FeatureConfigurationModel> list = obj.getFeatureListByLogin(LoginID, -1);
                if (list.Count > 0)
                {
                    BindingClass.RepeaterViewBind(rptReports, list);
                }
                else
                {
                    BindingClass.ClearRepeaterView(rptReports);
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "toastr.error('No Report assigned to " + Session["DisplayName"] + "!', 'N/A',{positionClass:'toast-bottom-right'});");
                }
            }
              catch (Exception)
              { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void rptReports_ItemCommand(object source, RepeaterCommandEventArgs e)
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