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
    public partial class ObjectMaintenance : System.Web.UI.Page
    {

        #region ClassIntances and Varible Declerations
        public string Alert = "";
        CommonBLL cobj = new CommonBLL();
        ObjectMaintenanceBLL obj = new ObjectMaintenanceBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //clearControls();
                    if (Session["admin"] != null)
                    {
                        ddlClientBind();
                    }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }

        }
        #endregion


        #region ddlbind
        public void ddlClientBind()
        {
            try
            {
                List<ClientIDName> list = cobj.getClientList();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                }
                else
                { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlObjectBind()
        {
            try
            {
                List<ObjectIDName> list = cobj.getObjectListByClient(Convert.ToInt32(ddlClient.SelectedValue));
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlObject, list, "Name", "ObjectID", "Select Device");
                }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }

        public void gridBind()
        {
            try
            {
                List<ObjectMaintenanceModel> list = obj.getObjectMaintenanceByObject(Convert.ToInt32(ddlObject.SelectedValue));
                BindingClass.GridViewBind(gvdObjectMnt, list);
                gvdObjectMnt.Visible = true;
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        protected void btnAddObjectMnt_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectMaintenanceModel model = new ObjectMaintenanceModel();

                model.MainId = 0;
                model.ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                model.IssueComments = txtIssueComment.Text;
                model.IssueDateTime = Convert.ToDateTime(txtIssuedt.Text);
                model.IssueAuthor = txtIssueAuthor.Text;
                model.ResolvedComments = "";
                model.ResolvedDateTime = DateTime.Now;
                model.ResolvedPerson = "";
                bool status = obj.postObjectMaintenance(model);
                if (status == true)
                { Alert = AlertsClass.SuccessAdd; }
                else
                { Alert = AlertsClass.ErrorWentWrong; }
                clearControls();
                gridBind();
                allowStaticMethods("staticMethod();ALerts('" + Alert + "');applyDatatable('.gvdObjectMntClass');");

            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlClientBind();
            clearControls();
            allowStaticMethods("staticMethod();");
        }

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                {
                    gvdObjectMnt.Visible = false;
                    ddlObjectBind();

                }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
                allowStaticMethods("staticMethod();");
            }

            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlObject.SelectedValue != "0")
                {gridBind();}
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
                allowStaticMethods("staticMethod();applyDatatable('.gvdObjectMntClass');");
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
                    int ID = Convert.ToInt32(e.CommandArgument);
                    ObjectMaintenanceModel model = obj.getObjectMaintenanceByID(ID);
                    if (model.isActive == true)
                    {
                        txtResolveId.Text = e.CommandArgument.ToString();
                        txtResolveId.Enabled = false;
                        allowStaticMethods("openResolveModal();staticMethod();applyDatatable('.gvdObjectMntClass');");
                    }
                    if (model.isActive == false)
                    {
                        lblResolvedComments.Text = model.ResolvedComments;
                        lblResolvedPerson.Text = model.ResolvedPerson;
                        lblResolvedDateTime.Text = Convert.ToString(model.ResolvedDateTime);
                        allowStaticMethods("openResolvedModal();staticMethod();applyDatatable('.gvdObjectMntClass');");
                    }


                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #region ClearControls
        public void clearControls()
        {
            txtIssueComment.Text = string.Empty;
            txtIssueAuthor.Text = string.Empty;
            txtIssuedt.Text = string.Empty;
            gvdObjectMnt.Visible = false;

            txtResolvePerson.Text = string.Empty;
            txtResolveId.Text = string.Empty;
            txtResolveComments.Text = string.Empty;
            lblResolvedComments.Text = "N/A";
            lblResolvedDateTime.Text = "N/A";
            lblResolvedPerson.Text = "N/A";

        }
        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
        #endregion


        protected void btnResolved_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectMaintenanceModel model = new ObjectMaintenanceModel();
                model.ObjectID = 0;
                model.IssueComments = "";
                model.IssueDateTime = DateTime.Now;
                model.IssueAuthor = "";
                model.MainId = Convert.ToInt32(txtResolveId.Text);
                model.IssueDateTime = DateTime.Now;
                model.ResolvedComments = txtResolveComments.Text;
                model.ResolvedDateTime = DateTime.Now;
                model.ResolvedPerson = txtResolvePerson.Text;
                bool status = obj.postObjectMaintenance(model);
                if (status == true)
                { Alert = AlertsClass.SuccessAdd; }
                else
                { Alert = AlertsClass.ErrorWentWrong; }
                clearControls();
                gridBind();
                allowStaticMethods("staticMethod();ALerts('" + Alert + "');applyDatatable('.gvdObjectMntClass');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
            
    }
}