using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class Client : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string alert = "";
        GroupService GS = new GroupService();
        ClientService CS = new ClientService();
        ClientBLL obj = new ClientBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    
                    if (Session["admin"] != null)
                    { clearControls(); }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region LinkButton Clicks

        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "RemoveID")
            {
                string client = Convert.ToString(e.CommandArgument);
                Session["ClientID"] = Convert.ToInt32(client);
                BindingClass.CallScriptManager(this.Page, this.GetType(), "openDeleteModal();applyDatatable('.gvdclientclass');staticMethod('Disable')");
            }
        }
        protected void linkbtnDel_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int ClientID = Convert.ToInt32(Session["ClientID"]);
                    bool status = obj.disableClient(ClientID);
                    if (status == true){alert = AlertsClass.SuccessRemove;}else{alert = AlertsClass.ErrorWentWrong;}
                    gridBind();
                    Session.Remove("ClientID");
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "ALerts('" + alert + "');CloseDeleteModal();applyDatatable('.gvdclientclass');staticMethod('Disable')");
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
                    BindingClass.CallScriptManager(this.Page, this.GetType(), "applyDatatable('.gvdclientclass');staticMethod('Enable')");
                    int ID = Convert.ToInt32(e.CommandArgument);
                    ClientModel li = obj.getClientByClientID(ID);
                    txtClient.Text = li.Name;
                    txtAddress.Text = li.Address;
                    txtOperatorId.Text = li.OperatorID.ToString();
                    txtContact.Text = li.Contact;
                    txtCode.Text = li.Code;
                    txtEmail.Text = li.Email;
                    txtExpireDate.Text = li.ExpireDate.ToString();
                    btnAddClient.Text = "Update";
                    Session["ClientId"] = ID.ToString();
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region  Button Clicks
        protected void btnAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtClient.Text != "" && txtAddress.Text != "" && txtOperatorId.Text != "" && txtContact.Text != "" && txtCode.Text != "" && txtEmail.Text != "")
                {
                    ClientModel model = new ClientModel();
                    model.Name = txtClient.Text;
                    model.Address = txtAddress.Text;
                    model.OperatorID = Convert.ToInt32(txtOperatorId.Text);
                    model.Contact = txtContact.Text;
                    model.Code = txtCode.Text;
                    model.Email = txtEmail.Text;
                    model.ExpireDate = Convert.ToDateTime(txtExpireDate.Text);

                    if (btnAddClient.Text == "Save")
                    {
                        bool exist = obj.codeExist(model.Code, model.Name);
                        if (exist == false)
                        {
                            model.ClientID = 0;
                            bool status = obj.postClient(model);
                            if (status == true) { alert = AlertsClass.SuccessUpdate; } else { alert = AlertsClass.ErrorWentWrong; }
                        }
                        else
                        { alert = AlertsClass.ErrorExist("Code"); }
                    }
                    if (btnAddClient.Text == "Update")
                    {
                        model.ClientID = Convert.ToInt32(Session["ClientId"]);
                        bool status = obj.postClient(model);
                        if (status == true){alert = AlertsClass.SuccessUpdate;}else{alert = AlertsClass.ErrorWentWrong;}
                    }
                }
                else
                {alert = AlertsClass.ErrorRequired;}
                clearControls();
                BindingClass.CallScriptManager(this.Page, this.GetType(), "ALerts('" + alert + "');applyDatatable('.gvdclientclass'); staticMethod('Disable')");
                
            }
            catch (Exception)
            {BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType());}
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            BindingClass.CallScriptManager(this.Page, this.GetType(), "applyDatatable('.gvdclientclass');staticMethod('Disable')");
        }
        #endregion

        #region Binding Controls
        public void gridBind()
        {
            try
            {
                List<ClientModel> list = obj.getClient();
                if (list.Count > 0)
                {
                    BindingClass.GridViewBind(GvdClient, list);
                }
            }
            catch(Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

           
        }
        #endregion
       
        #region Textboxes clear Method
        public void clearControls()
        {
            gridBind();
            txtClient.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtOperatorId.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtExpireDate.Text = string.Empty;
            btnAddClient.Text = "Save";
            Session.Remove("ClientId");
        }
        #endregion


    }
}