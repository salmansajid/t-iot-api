using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;

namespace TIOT_WEB
{
    public partial class Sensor : System.Web.UI.Page
    {
        #region PageLoad & varaibles
        public string Alert = "";
        SensorService SNS = new SensorService();
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
                        BindgridView();
                        clearControls();
                    }
                    else
                    {Response.Redirect("404.aspx");}
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region Button Clicks
        protected void btnAddSensor_Click(object sender, EventArgs e)
        {
            try
            {
                bool enable = cbEnabled.Checked ? true : false;
                if (txtSourceId.Text != "" && txtSourceName.Text != "" && txtUnit.Text != "")
                {
                    if (btnAddSensor.Text == "Save")
                    {
                        if (cbEnabled.Checked)
                        {
                            int responce = SNS.PostSensor(txtSourceId.Text, txtSourceName.Text, txtUnit.Text, enable);
                            if (responce != 0)
                            { Alert = AlertsClass.SuccessAdd; }
                            else
                            { Alert = AlertsClass.ErrorWentWrong; }
                        }
                    }
                    if (btnAddSensor.Text == "Update")
                    {
                        int sensorId = Convert.ToInt32(Session["sensorId"]);
                        bool status = SNS.PutSensor(sensorId, txtSourceId.Text, txtSourceName.Text, txtUnit.Text, enable);
                        if (status == true)
                        { Alert = AlertsClass.SuccessUpdate; }
                        else
                        { Alert = AlertsClass.ErrorWentWrong; }
                    }
                }
                else
                { Alert = AlertsClass.ErrorRequired; }
                BindgridView();
                clearControls();
                allowStaticMethods("ALerts('" + Alert + "');staticMethod('Disable');  applyDatatable('.gvdSensorclass');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        protected void btnClear_Click(object sender, EventArgs e)
        { 
            clearControls();
            allowStaticMethods("staticMethod('Disable');  applyDatatable('.gvdSensorclass');");
        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    allowStaticMethods("staticMethod('Enable');  applyDatatable('.gvdSensorclass');");
                    clearControls();
                    int _cmdArg = Convert.ToInt32(e.CommandArgument);
                    SensorModel li = SNS.GetSensor(_cmdArg);
                    Session["sensorId"] = _cmdArg.ToString();
                    txtSourceName.Text = li.SourceName;
                    txtSourceId.Text = li.SourceID;
                    txtUnit.Text = li.Unit;
                    cbEnabled.Checked = li.EnableOrDisable;
                    txtSourceName.Text = li.SourceName;
                    btnAddSensor.Text = "Update";
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveID")
                {
                    string user = Convert.ToString(e.CommandArgument);
                    Session["sensorId"] = Convert.ToInt32(user);
                    allowStaticMethods("openDeleteModal();staticMethod('Disable');");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int _cmdArg = Convert.ToInt32(e.CommandArgument);
                    bool status = SNS.DeleteSensor(_cmdArg);
                    if (status == true)
                    { Alert = AlertsClass.SuccessRemove; }
                    else
                    { Alert = AlertsClass.ErrorWentWrong; }
                    BindgridView();
                    allowStaticMethods("ALerts('" + Alert + "'); staticMethod('Disable');  applyDatatable('.gvdSensorclass');");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region  Binding Controls
        public void BindgridView()
        {
            try
            {
                List<SensorModel> li = SNS.GetSensors();
                BindingClass.GridViewBind(sensorGrid, li);
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Text and Dropdown Clear Method
        public void clearControls()
        {
            txtSourceId.Text = string.Empty;
            txtSourceName.Text = string.Empty;
            txtUnit.Text = string.Empty;
            btnAddSensor.Text = "Save";
            Session.Remove("sensorId");
        }
        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
        #endregion

    }
}