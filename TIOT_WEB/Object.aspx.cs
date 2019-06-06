using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TavlWeb.WebAPI.Authentication;
using Newtonsoft.Json;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class Object : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string alert = "";
        ObjectService OBJS = new ObjectService();
        ClientService CS = new ClientService();
        CommonBLL comObj = new CommonBLL();
        ObjectBLL obj = new ObjectBLL();
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
                    {ddlClientBind();}
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region LinkButton Clicks
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int ID = Convert.ToInt32(Session["ObjectId"]);
                    bool status = obj.disableObject(ID);
                    if (status == true)
                    { alert = AlertsClass.SuccessRemove; }
                    else
                    { alert = AlertsClass.ErrorWentWrong; }
                    GridBind();
                    allowStaticMethods("ALerts('" + alert + "'); closeDeleteModal();applyDatatable('.gvdObjectClass');  staticMethod('Disable');");                
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
                Session["ObjectId"] = Convert.ToInt32(ID);
                allowStaticMethods("openDeleteModal();applyDatatable('.gvdObjectClass'); staticMethod('Disable');");                
            }
        }
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    allowStaticMethods("applyDatatable('.gvdObjectClass'); staticMethod('Enable');");                                    
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    ObjectModelDLL li = obj.getObjectByObjectID(cmdArg);
                    ddlClient.SelectedValue = li.ClientID.ToString();
                    txtObjectName.Text = li.Name;
                    txtAddress.Text = li.Address;
                    txtLat.Text = li.LAT.ToString();
                    txtLong.Text = li.LONG.ToString();
                    txtIMEI.Text = li.IMEI.ToString();
                    txtSimNumber.Text = li.SimNumber.ToString();
                    txtFirmWareVersion.Text = li.FirmWareVersion;
                    txtHardwareVersion.Text = li.HardwareVersion;
                    txtContact.Text = li.Contact+",";
                    ddlDeviceType.SelectedValue = "1";
                    chkRelaySt.Checked = Convert.ToBoolean(li.RelayStatus);
                    chkRelaySt.Checked = Convert.ToBoolean(li.RelayStatus);
                    Session["ObjectId"] = cmdArg.ToString();
                    btnAddObject.Text = "Update";
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Button Clicks
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            ddlClientBind();
            allowStaticMethods("staticMethod('Disable');");
        }
        protected void btnAddObject_Click(object sender, EventArgs e)
        {
            try
            {

                if (ddlClient.SelectedValue != "0" && ddlDeviceType.SelectedValue != "0" && txtObjectName.Text != "" && txtAddress.Text != "" && txtLat.Text != "" && txtLong.Text != "" && txtIMEI.Text != "" && txtSimNumber.Text != "" && txtFirmWareVersion.Text != "" && txtHardwareVersion.Text != "")
                {
                    bool RStatus = chkRelaySt.Checked ? true : false;
                    ObjectModelDLL model = new ObjectModelDLL();
                    model.Name = txtObjectName.Text;
                    model.Address = txtAddress.Text;
                    model.LAT = Convert.ToDouble(txtLat.Text);
                    model.LONG = Convert.ToDouble(txtLong.Text);
                    model.IMEI = Convert.ToInt64(txtIMEI.Text);
                    model.SimNumber = Convert.ToInt64(txtSimNumber.Text);
                    model.ClientID = Convert.ToInt32(ddlClient.SelectedValue);
                    model.HardwareVersion = txtHardwareVersion.Text;
                    model.FirmWareVersion = txtFirmWareVersion.Text;
                    model.ObjectType = ddlDeviceType.SelectedItem.Text;
                    model.Contact = txtContact.Text+",";
                    model.RelayStatus = RStatus;

                    if (btnAddObject.Text == "Save")
                    {
                        model.ObjectID = 0;
                        bool exist = obj.objectExist(model.IMEI.ToString());
                        if (exist == false)
                        {
                            bool status = obj.postObject(model);
                            if (status == true)
                            { alert = AlertsClass.SuccessAdd; }
                            else
                            { alert = AlertsClass.ErrorWentWrong; }
                        }
                        else
                        {
                            { alert = AlertsClass.ErrorExist("IMEI"); }
                        }
                    }
                    if (btnAddObject.Text == "Update")
                    {

                        model.ObjectID = Convert.ToInt32(Session["ObjectId"]);
                        bool status = obj.postObject(model);
                        if (status == true)
                        { alert = AlertsClass.SuccessAdd; }
                        else
                        { alert = AlertsClass.ErrorWentWrong; }
                    }
                }
                else
                { alert = AlertsClass.ErrorRequired; }
                clearControls();
                GridBind();
                allowStaticMethods("ALerts('" + alert + "');applyDatatable('.gvdObjectClass'); staticMethod('Disable'); phonenumber();");
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
                List<ClientIDName> list = comObj.getClientList();
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

        public void GridBind()
        {
            try
            {
                List<ObjectModelDLL> li = obj.getObjectList(Convert.ToInt32(ddlClient.SelectedValue));
                BindingClass.GridViewBind(gvdObject, li);
                gvdObject.Visible = true;
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
                    clearControls();
                    GridBind();
                    allowStaticMethods("applyDatatable('.gvdObjectClass');staticMethod('Disable');");
                }
                else
                {gvdObject.Visible = false;}
                
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Text and Dropdown Clear Method
        public void clearControls()
        {
            txtObjectName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtLat.Text = string.Empty;
            txtLong.Text = string.Empty;
            txtLong.Text = string.Empty;
            txtIMEI.Text = string.Empty;
            txtSimNumber.Text = string.Empty;
            txtFirmWareVersion.Text = string.Empty;
            txtHardwareVersion.Text = string.Empty;
            txtContact.Text = string.Empty;
            chkRelaySt.Checked = false;
            gvdObject.Visible = false;
            ddlDeviceType.SelectedValue = "0";
            btnAddObject.Text = "Save";

        }
        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
        #endregion
    }
}