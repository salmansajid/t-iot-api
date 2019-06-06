using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class ObjectGroup : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string Alert = "";
        ObjectGroupService OBJGS = new ObjectGroupService();
        ObjectService OBJS = new ObjectService();
        GroupService GS = new GroupService();
        ClientService CS = new ClientService();
        CommonBLL cobj = new CommonBLL();
        ObjectGroupBLL obj = new ObjectGroupBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    clearControls();
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

        #region Button Clicks
        protected void btnAddObjectGroup_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0" && ddlGroup.SelectedValue != "0" && ddlObject.SelectedValue != "0")
                {
                    ObjectGroupModel model = new ObjectGroupModel();
                    model.ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                    model.GroupID = Convert.ToInt32(ddlGroup.SelectedValue);

                    if (btnAddObjectGroup.Text == "Save")
                    {
                        model.ObjectGroupID = 0;
                        bool status = obj.postObjectGroup(model);
                        if (status == true)
                        { Alert = AlertsClass.SuccessAdd; }
                        else
                        { Alert = AlertsClass.ErrorWentWrong; }
                    }

                    if (btnAddObjectGroup.Text == "Update")
                    {
                        model.ObjectGroupID = Convert.ToInt32(Session["objectGroupId"]);
                        bool status = obj.postObjectGroup(model);
                        if (status == true)
                        { Alert = AlertsClass.SuccessAdd; }
                        else
                        { Alert = AlertsClass.ErrorWentWrong; }
                    }
                }
                else
                {Alert = AlertsClass.ErrorRequired;}
                BindingClass.CallScriptManager(this.Page, this.GetType(), "ALerts('" + Alert + "'); applyDatatable('.gvdObjectGroupClass');staticMethod('Disable');");

            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    BindingClass.CallScriptManager(this, this.GetType(), "applyDatatable('.gvdObjectGroupClass'); staticMethod('Enable');");
                    int ID = Convert.ToInt32(e.CommandArgument);
                    ObjectGroupModel li = obj.getObjectGroupByID(ID);
                    ddlObject.SelectedValue = li.ObjectID.ToString();
                    ddlGroup.SelectedValue = li.GroupID.ToString();
                    Session["objectGroupId"] = ID.ToString();
                    btnAddObjectGroup.Text = "Update";
                    gridBind();
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
           
            
        }
        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RemoveID")
            {
                string ID = Convert.ToString(e.CommandArgument);
                Session["objectGroupId"] = Convert.ToInt32(ID);
                BindingClass.CallScriptManager(this, this.GetType(), "openDeleteModal();  applyDatatable('.gvdObjectGroupClass'); staticMethod('Disable');");
            }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int ID = Convert.ToInt32(Session["objectGroupId"]);
                    bool status = obj.deleteObjectGroup(ID);
                    clearControls();
                    gridBind();
                    BindingClass.CallScriptManager(this, this.GetType(), "ALerts('" + Alert + "'); closeDeleteModal();  applyDatatable('.gvdObjectGroupClass'); staticMethod('Disable');");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion


        #region  Binding Controls
        public void ddlClientBind()
        {
            try
            {
                List<ClientIDName> list = cobj.getClientList();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
                    BindingClass.ClearDropDown(ddlGroup, "Select Branch");
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void ddlObjectBind(int clientID)
        {
            try
            {
                List<ObjectIDName> list = cobj.getObjectListByClient(clientID);
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlObject, list, "Name", "ObjectID", "Select Device");
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

        public void gridBind()
        {
            try
            {
                List<ObjectGroupModel> li = obj.getObjectGroupByObject(Convert.ToInt32(ddlObject.SelectedValue));
                BindingClass.GridViewBind(gvdObjectGroup, li);
                gvdObjectGroup.Visible = true;
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
                    ddlObjectBind(Convert.ToInt32(ddlClient.SelectedValue));
                    ddlGroupBind(Convert.ToInt32(ddlClient.SelectedValue));
                    BindingClass.CallScriptManager(this, this.GetType(), "staticMethod('Disable');");
                }
                gvdObjectGroup.Visible = false;
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlObject.SelectedValue != "0")
                {
                    gridBind();
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "applyDatatable('.gvdObjectGroupClass'); staticMethod('Disable');"); 
                      
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }


        #endregion

        #region clear Method

        public void clearControls()
        {
            ddlObject.SelectedValue = "0";            
            ddlGroup.SelectedValue = "0";            
            btnAddObjectGroup.Text = "Save";
            gvdObjectGroup.Visible = false;
            BindingClass.CallScriptManager(this, this.GetType(), "applyDatatable('.gvdObjectGroupClass'); staticMethod('Disable');");
        }
        #endregion

    }
}