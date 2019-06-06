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
    public partial class Group : System.Web.UI.Page
    {
        #region  ClassIntances and Varible Declerations
        public string alert = "";
        //GroupService GS = new GroupService();
        //ClientService CS = new ClientService();
        CommonBLL cobj = new CommonBLL();
        GroupBLL obj = new GroupBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    clearControls();
                    if (Session["admin"] != null)
                    { ddlClientBind(); }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }

        }
        #endregion

        #region Buttons Clicks
        protected void btnGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0" && txtGroupName.Text != "" && txtComments.Text != "")
                {
                    GetGroupModel model = new GetGroupModel();
                    model.ClientID = Convert.ToInt32(ddlClient.SelectedValue);
                    model.Name = txtGroupName.Text;
                    model.Comment = txtComments.Text;

                    if (btnGroup.Text == "Save")
                    {
                        model.GroupID = 0;
                        bool exist = obj.groupExist(model.Name);
                        if (exist == false)
                        {
                            bool status = obj.postGroup(model);
                            if (status == true)
                            { alert = AlertsClass.SuccessAdd; }
                            else
                            { alert = AlertsClass.ErrorWentWrong; }
                        }
                        else
                        { alert = AlertsClass.ErrorExist("Branch"); }
                    }
                    if (btnGroup.Text == "Update")
                    {
                        model.GroupID = Convert.ToInt32(Session["GroupId"]);
                        bool status = obj.postGroup(model);
                        if (status == true)
                        {alert = AlertsClass.SuccessUpdate;}
                        else
                        {alert = AlertsClass.ErrorWentWrong;}
                    }
                }
                else
                {alert = AlertsClass.ErrorRequired;}
                clearControls();
                gridBind();
                allowStaticMethods("ALerts('" + alert + "'); ApplyDatatable('.gvdGroupClass'); staticMethod('Disable');");
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

        #region LinkButton Clicks

        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RemoveID")
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["GroupID"] = Convert.ToInt32(ID);
                 allowStaticMethods("openDeleteModal();applyDatatable('.gvdGroupClass'); staticMethod('Disable');");
            }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int ID = Convert.ToInt32(Session["GroupID"]);
                    bool status = obj.disableGroup(ID);
                    if (status == true)
                    {alert = AlertsClass.SuccessRemove;}
                    else
                    {alert = AlertsClass.ErrorWentWrong;}
                    gridBind();
                    Session.Remove("GroupID");
                    allowStaticMethods("ALerts('" + alert + "');CloseDeleteModal();applyDatatable('.gvdGroupClass'); staticMethod('Disable');");
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
                    allowStaticMethods("applyDatatable('.gvdGroupClass');  staticMethod('Enable');");
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    GetGroupModel li = obj.getGroupByGroupID(cmdArg);
                    txtComments.Text = li.Comment;
                    txtGroupName.Text = li.Name;
                    ddlClient.SelectedValue = li.ClientID.ToString();
                    gridBind();
                    Session["GroupId"] = cmdArg.ToString();
                    btnGroup.Text = "Update";
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            
        }
        #endregion

        #region Binding Controls
        public void ddlClientBind()
        {
            try
            {
                List<ClientIDName> list = cobj.getClientList();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
                }
                else
                { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void gridBind()
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                {
                    List<GetGroupModel> list = obj.getGroupList(Convert.ToInt32(ddlClient.SelectedValue));
                    BindingClass.GridViewBind(gvdGroup, list);
                    gvdGroup.Visible = true;
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
                    gridBind();
                }
                else
                { clearControls(); }
                allowStaticMethods("applyDatatable('.gvdGroupClass');  staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Dropdowns and textboxes Empty
        public void clearControls()
        {
            txtGroupName.Text = string.Empty;
            txtComments.Text = string.Empty;
            gvdGroup.Visible = false;
            Session.Remove("GroupId");
        }
        public void allowStaticMethods(string jsFunction)
        {BindingClass.CallScriptManager(this.Page, this.GetType(), jsFunction);}
        #endregion


    }
}