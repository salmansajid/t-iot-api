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
    public partial class ObjectSensor : System.Web.UI.Page
    {
        ObjectSensorService OBJSEN = new ObjectSensorService();
        ObjectService OBJ = new ObjectService();
        ClientService CS = new ClientService();
        SensorService Sensors = new SensorService();
        CategoryService CTS = new CategoryService();
        string alert = "";
        CommonBLL comObj = new CommonBLL();
        ObjectSensorBLL obj = new ObjectSensorBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["admin"] != null)
                {
                    ddlClientBind();
                    ddlCategoryBind();
                }
                else
                {Response.Redirect("Login.aspx");}

            }
        }

        #region GridBind

        public void gridBind()
        {
            try
            {
                List<ObjectSensorModel> list = obj.getObjectSensorList(Convert.ToInt32(ddlObject.SelectedValue));
                BindingClass.GridViewBind(gvdObjectSensor, list);
                gvdObjectSensor.Visible = true;
            }
            catch
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion

        #region  ddlBind


        public void ddlClientBind()
        {
            try
            {
                List<ClientIDName> list = comObj.getClientList();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlClient, list, "Name", "ClientID", "Select Client");
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                    BindingClass.ClearDropDown(ddlSensor, "Select Sensor");
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
                List<ObjectIDName> list = comObj.getObjectListByClient(Convert.ToInt32(ddlClient.SelectedValue));
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
        public void ddlNAsensorBind()
        {
            try
            {
                List<SensorIDSourceID> list = obj.getNASensorListByObject(Convert.ToInt32(ddlObject.SelectedValue));
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlSensor, list, "SourceID", "SensorID", "Select Sensor");
                }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            }

            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlSensorBind()
        {
            try
            {
                List<SensorIDSourceID> list = comObj.getSensorIDSourceID();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlSensor, list, "SourceID", "SensorID", "Select Sensor");
                }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            }

            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlCategoryBind()
        {
            try
            {
                List<CategoryIDName> list = comObj.getCategoryIDName();
                if (list.Count > 0)
                {
                    BindingClass.BindDropDown(ddlcategory, list, "Category", "CategoryID", "Select Category");
                }
                else
                { BindingClass.ClearDropDown(ddlcategory, "Select Category"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion

        #region  ddlSelection change

        protected void ddlClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0")
                {
                    ddlObjectBind();
                    gvdObjectSensor.Visible = false;
                    allowStaticMethods("staticMethod('Disable');");
                }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
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
                    ddlNAsensorBind();
                    gridBind();
                    allowStaticMethods("applyDatatable('.gvdObjectSensorClass');staticMethod('Disable');");
                }
                else
                {
                    gvdObjectSensor.Visible = false;
                    BindingClass.ClearDropDown(ddlSensor, "Select Sensor"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion


        #region  Button clicks

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ddlClientBind();
            clearControls();
            allowStaticMethods("staticMethod('Disable');");
        }
        protected void btnAddObjectSensor_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlClient.SelectedValue != "0" && ddlcategory.SelectedValue != "0" && ddlObject.SelectedValue != "0" && ddlSensor.SelectedValue != "0" && txtName.Text != "" && txtMax.Text != "" && txtMin.Text != "" && txta1.Text != "" && txta0.Text != "")
                {
                    ObjectSensorModelDLL model = new ObjectSensorModelDLL();
                    model.ObjectID = Convert.ToInt32(ddlObject.SelectedValue);
                    model.SensorID = Convert.ToInt32(ddlSensor.SelectedValue);
                    model.Name = txtName.Text;
                    model.SMSAlert = cbSmsAlert.Checked;
                    model.EmailAlert = false;
                    model.Contact = txtContact.Text+ ",";
                    model.Max = Convert.ToInt32(txtMax.Text);
                    model.Min = Convert.ToInt32(txtMin.Text);
                    model.A1 = Convert.ToDouble(txta1.Text);
                    model.A0 = Convert.ToDouble(txta0.Text);
                    model.CategoryID = Convert.ToInt32(ddlcategory.SelectedValue);
                    if (btnAddObjectSensor.Text == "Save")
                    {
                        model.ObjectSensorId = 0;
                        bool exist = obj.objectSensorExist(model.ObjectID, model.SensorID);
                        if (exist == false)
                        {
                            bool status = obj.postObjectSensor(model);
                            if (status == true)
                            { alert = AlertsClass.SuccessAdd; }
                            else
                            { alert = AlertsClass.ErrorWentWrong; }
                        }
                        else
                        { alert = AlertsClass.ErrorExist("Device Sensor "); }
                    }
                    if (btnAddObjectSensor.Text == "Update")
                    {
                        model.ObjectSensorId = Convert.ToInt64(Session["objectsensorId"]);
                        bool status = obj.postObjectSensor(model);
                        if (status == true)
                        { alert = AlertsClass.SuccessAdd; }
                        else
                        { alert = AlertsClass.ErrorWentWrong; }
                    }
                }
                else
                { alert = AlertsClass.ErrorRequired; }
                clearControls();
                ddlNAsensorBind();
                gridBind();
                allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdObjectSensorClass');staticMethod('Disable');");
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
                    btnAddObjectSensor.Text = "Update";
                    allowStaticMethods("applyDatatable('.gvdObjectSensorClass');staticMethod('Enable');");
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    ObjectSensorModelDLL li = obj.getObjectSensorByID(cmdArg);
                    ddlSensorBind();
                    ddlSensor.SelectedValue = li.SensorID.ToString();
                    txtName.Text = li.Name;
                    txtMax.Text = li.Max.ToString();
                    txtMin.Text = li.Min.ToString();
                    txta0.Text = li.A0.ToString();
                    txta1.Text = li.A1.ToString();
                    txtContact.Text = li.Contact;
                    cbSmsAlert.Checked = Convert.ToBoolean(li.SMSAlert);
                    ddlcategory.SelectedValue = li.CategoryID.ToString();
                    Session["objectsensorId"] = cmdArg.ToString();
                  
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnDisable_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Disable")
                {
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    bool status = obj.disableObjectSensor(cmdArg);
                    if (status == true)
                    {
                        alert = AlertsClass.SuccessRemove;
                        gridBind();
                    }
                    else
                    { alert = AlertsClass.ErrorWentWrong; }
                    allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdObjectSensorClass');staticMethod('Disable');");

                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion

        #region ClearControls
        public void clearControls()
        {
            //BindingClass.ClearDropDown(ddlSensor, "Select Sensor");
            //ddlObject.SelectedValue = "0";
            txtName.Text = string.Empty;
            txtMax.Text = string.Empty;
            txtMin.Text = string.Empty;
            txtContact.Text = string.Empty;
            txta0.Text = string.Empty;
            txta1.Text = string.Empty;
            cbSmsAlert.Checked = false;
            btnAddObjectSensor.Text = "Save";
            gvdObjectSensor.Visible = false;

        }
        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
        #endregion

    
    }
}

