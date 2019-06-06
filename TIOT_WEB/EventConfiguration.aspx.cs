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
    public partial class EventConfiguration : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string alert = "";
        LoginGroupService LGS = new LoginGroupService();
        GroupService GS = new GroupService();
        ObjectSensorService OBJSEN = new ObjectSensorService();
        ObjectService OBJ = new ObjectService();
        ClientService CS = new ClientService();
        SensorService Sensors = new SensorService();
        CategoryService CTS = new CategoryService();
        SensorCommandService SMS = new SensorCommandService();
        EventConfigurationService ECS = new EventConfigurationService();
        CommonBLL cobj = new CommonBLL();
        EventConfigBLL obj = new EventConfigBLL();
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
                        ddlConditionBind();
                    }
                    else if (Session["poweruser"] != null)
                    {
                        divClient.Visible = false;
                        var list = (LoginModelForUser)Session["poweruser"];
                        int _clId = list.ClientID;
                        ddlGroupbind(_clId);
                        ddlGroup.Visible = true;
                        BindingClass.ClearDropDown(ddlGroup, "Select Branch");
                        BindingClass.ClearDropDown(ddlObject, "Select Device");
                        BindingClass.ClearDropDown(ddlObjSensor, "Select Location");
                        ddlConditionBind();
                    }
                    else if (Session["user"] != null)
                    {
                        divClient.Visible = false;
                        divGroup.Visible = false;
                        int loginID = Convert.ToInt32(Session["user"]);
                        LoginGroupModel li = LGS.GetLoginGroupByLogin(loginID);
                        if (li != null)
                        {
                            ddlObjectbind(li.GroupID);
                            ddlObject.Visible = true;
                            BindingClass.ClearDropDown(ddlObjSensor, "Select Location");
                            ddlConditionBind();
                        }
                    }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region  Button Clicks
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlObject.SelectedValue != "0" && ddlObjSensor.SelectedValue != "0" && ddlCondition.SelectedValue != "0")
                {
                    EventConfigurationModel model = new EventConfigurationModel();
                    model.EnableOrDisable = cbEnabled.Checked ? true : false;
                    model.ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                    model.ObjectSensorID = Convert.ToInt32(ddlObjSensor.SelectedValue);
                    model.Min = Convert.ToDouble(txtMin.Text);
                    model.MAX = Convert.ToDouble(txtMax.Text);
                    model.Condition = Convert.ToInt32(ddlCondition.SelectedValue);
                    model.a0 = Convert.ToDouble(txta0.Text);
                    model.a1 = Convert.ToDouble(txta1.Text);
                    model.Contact = txtContact.Text+ ",";
                    model.Format = txtFormat.Text;
                    model.Units = txtUnit.Text;
                    if (btnSave.Text == "Save")
                    {
                        model.EventConfigID = 0;
                        bool status = obj.postEventConfig(model);
                        if (status == true)
                        { alert = AlertsClass.SuccessUpdate; }
                        else
                        { alert = AlertsClass.ErrorWentWrong; }
                    }
                    if (btnSave.Text == "Update")
                    {
                        model.EventConfigID = Convert.ToInt32(Session["eventConfigId"]);
                        bool status = obj.postEventConfig(model);
                        if (status == true)
                        { alert = AlertsClass.SuccessUpdate; }
                        else
                        { alert = AlertsClass.ErrorWentWrong; }
                    }
                }
                else
                { alert = AlertsClass.ErrorRequired; }
                gridBind(Convert.ToInt32(ddlObject.SelectedValue));
                allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        { 
            clearControls();
            allowStaticMethods("staticMethod('Disable');");
        }
        #endregion

        #region  LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {

                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    EventConfigurationModel li = obj.getEventConfigByID(cmdArg);
                    if (li != null)
                    {
                        ddlObject.SelectedValue = li.ObjectID.ToString();
                        ddlObjsensorbind(Convert.ToInt32(li.ObjectID));
                        ddlObjSensor.SelectedValue = li.ObjectSensorID.ToString();
                        txtMin.Text = li.Min.ToString();
                        txtMax.Text = li.MAX.ToString();
                        ddlCondition.SelectedValue = li.Condition.ToString();
                        cbEnabled.Checked = Convert.ToBoolean(li.EnableOrDisable);
                        Session["eventConfigId"] = cmdArg.ToString();
                        btnSave.Text = "Update";
                    }
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RemoveID")
            {
                string user = Convert.ToString(e.CommandArgument);
                Session["eventConfigId"] = Convert.ToInt32(user);
                allowStaticMethods("openDeleteModal();applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");
            }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int cmdArg = Convert.ToInt32(Session["eventConfigId"]);
                    bool status = ECS.DeleteEventConfiguration(cmdArg);
                    if (status == true)
                    {
                        alert = AlertsClass.SuccessRemove;
                        gridBind(Convert.ToInt32(ddlObject.SelectedValue));
                    }
                    else
                    { alert = AlertsClass.ErrorWentWrong; }
                    allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");

                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region  Binding Controls
        public void gridBind(int objectId)
        {
            try
            {
                List<EventConfigurationModel> list = obj.getEventConfigByObject(Convert.ToInt32(ddlObject.SelectedValue));
                BindingClass.GridViewBind(gvdEventConfig, list);
                gvdEventConfig.Visible = true;
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
                    BindingClass.ClearDropDown(ddlGroup, "Select Branch");
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                    BindingClass.ClearDropDown(ddlObjSensor, "Select Location");
                    gvdEventConfig.Visible = false;
                }
                else
                { BindingClass.ClearDropDown(ddlClient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlGroupbind(int ClientId)
        {
            try
            {
                List<GroupIDName> list = cobj.getGroupList(ClientId);
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlGroup, list, "Name", "GroupID", "Select Branch"); }
                else
                { BindingClass.ClearDropDown(ddlGroup, "Select Branch"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlObjectbind(int GroupId)
        {
            try
            {
                List<ObjectListDashboard> list = cobj.getObjectList(GroupId);
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

        public void ddlObjsensorbind(int ObjectId)
        {

            List<EventConfigurationLocationModel> li = ECS.GetObjectSensorsByObjId(ObjectId);
            if (li != null)
            {
                var list = li.Select(o => new { o.Location, o.objectsensorId });
                BindingClass.BindDropDown(ddlObjSensor, list, "Location", "objectsensorId", "Select Location");
            }
            else
            {
                BindingClass.ClearDropDown(ddlObjSensor, "Select Location");
            }

        }
        public void ddlConditionBind()
        {
            List<string> li = new List<string>();
            li.Add("Select Case"); li.Add("On Min"); li.Add("On Max"); li.Add("On Both");
            BindingClass.StaticListBindDropDown(ddlCondition, 4, li);

        }
        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                { ddlGroupbind(Convert.ToInt32(ddlClient.SelectedValue)); }
                gvdEventConfig.Visible = false;
                allowStaticMethods("applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlGroup.SelectedValue != "0")
                { ddlObjectbind(Convert.ToInt32(ddlGroup.SelectedValue)); }
                gvdEventConfig.Visible = false;
                allowStaticMethods("applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");
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
                    gridBind(Convert.ToInt32(ddlObject.SelectedValue));
                    ddlObjsensorbind(Convert.ToInt32(ddlObject.SelectedValue));
                    gvdEventConfig.Visible = true;
                    allowStaticMethods("applyDatatable('.gvdEventConfigClass');staticMethod('Disable');");
                }
                else
                { BindingClass.ClearDropDown(ddlObjSensor, "Select Location"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Clear Method
        public void clearControls()
        {
            ddlGroupbind(Convert.ToInt32(ddlClient.SelectedValue));
            ddlObjectbind(Convert.ToInt32(ddlGroup.SelectedValue));
            ddlObjsensorbind(Convert.ToInt32(ddlObject.SelectedValue));
            ddlConditionBind();
            txtMin.Text = "";
            txtMax.Text = "";
            txta0.Text = "";
            txta1.Text = "";
            txtUnit.Text = "";
            txtFormat.Text = "";
            cbEnabled.Checked = false;
            btnSave.Text = "Save"; gvdEventConfig.Visible = false;
        }
        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
        #endregion
    }
}