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
    public partial class Scheduling : System.Web.UI.Page
    {
        #region Class Instances & public Varibles
        public string _alert = "";
        public int _cmdArg;
        public bool _status;
        ObjectService OBJ = new ObjectService();
        ReportService RS = new ReportService();
        ClientService CL = new ClientService();
        GroupService GS = new GroupService();
        LoginGroupService LGS = new LoginGroupService();
        SchedulingService SS = new SchedulingService();
        SchedulingModel SM = new SchedulingModel();
        HolidaySchedulingService HSS = new HolidaySchedulingService();
        CommonBLL globalobj = new CommonBLL();
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
                        ddlClientbind();
                        ddlDays.Items.Clear();
                        ddlgroup.Visible = false;
                        ddlObject.Visible = false;
                    }
                    else if (Session["poweruser"] != null)
                    {

                        divClient.Visible = false;
                        ddlObject.Visible = false;
                        var list = (LoginModelForUser)Session["poweruser"];
                        int _clId = list.ClientID;
                        ddlGroupbind(_clId);
                        ddlgroup.Visible = true;
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
                        }
                    }
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

            }
        }
        #endregion

        #region Binding Controls
        public void ddlClientbind()
        {
            try
            {
                List<ClientModel> li = CL.GetClient();
                if (li != null)
                {
                    var list = li.Select(o => new { o.Name, o.ClientID }).Distinct();
                    BindingClass.BindDropDown(ddlclient, list, "Name", "ClientID", "Select Client");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlGroupbind(int ClientId)
        {
            try
            {
                List<GroupModel> li = GS.GetGroupByClientId(ClientId);
                if (li != null)
                {
                    var list2 = li.Select(o => new { o.Name, o.GroupID }).Distinct();
                    BindingClass.BindDropDown(ddlgroup, list2, "Name", "GroupID", "Select Group");

                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlObjectbind(int GroupId)
        {
            try{
            List<ObjectModel> li = OBJ.GetObjectsByGroupId(GroupId);
            if (li != null)
            {
                var list2 = li.Select(o => new { o.Name, o.ObjectID }).Distinct();
                BindingClass.BindDropDown(ddlObject, list2, "Name", "ObjectID", "Select Object");
            }
 
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void BindddlSensor()
        {
            try{
            List<ObjectSensorModel> li = OBJ.GetSensorByObjectId(Convert.ToInt32(ddlObject.SelectedValue));
            if (li != null)
            {
                var list2 = li.Select(o => new { o.Name, o.ObjectSensorId }).Distinct();
                BindingClass.BindDropDown(ddlSensor, list2, "Name", "ObjectSensorId", "ALL Routes");
            }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        public void ddlListItemDays()
        {
            try
            {
                ListItem[] items = new ListItem[10];
                items[0] = new ListItem("Select Days", "0");
                items[1] = new ListItem("Monday", "1");
                items[2] = new ListItem("Tuesday", "2");
                items[3] = new ListItem("Wednesday", "3");
                items[4] = new ListItem("Thursday", "4");
                items[5] = new ListItem("Friday", "5");
                items[6] = new ListItem("Saturday", "6");
                items[7] = new ListItem("Sunday", "7");
                items[8] = new ListItem("Weekly", "8");
                items[9] = new ListItem("Holidays", "9");
                ddlDays.Items.AddRange(items);
                ddlDays.DataBind();
            }   
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ddlListItemDaysForUser()
        {
            try{
            ListItem[] items = new ListItem[9];
            items[0] = new ListItem("Select Days", "0");
            items[1] = new ListItem("Monday", "1");
            items[2] = new ListItem("Tuesday", "2");
            items[3] = new ListItem("Wednesday", "3");
            items[4] = new ListItem("Thursday", "4");
            items[5] = new ListItem("Friday", "5");
            items[6] = new ListItem("Saturday", "6");
            items[7] = new ListItem("Sunday", "7");
            items[8] = new ListItem("Weekly", "8");
            //items[9] = new ListItem("Holidays", "9");
            ddlDays.Items.AddRange(items);
            ddlDays.DataBind();
                  }   
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }



        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclient.SelectedItem.Text != "Select Client")
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;
                GvdAllScheduling.Visible = false;
                ddlObject.Items.Clear();
                ddlObject.Items.Insert(0, new ListItem("Select Object", "0"));
                ddlgroup.Visible = true;
                ddlgroup.Items.Clear();
                var _sltdval = ddlclient.SelectedValue;
                int val = Convert.ToInt32(_sltdval);
                ddlGroupbind(val);
            }
            else
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;
                GvdAllScheduling.Visible = false;
                ddlObject.Items.Clear();
                ddlgroup.Items.Clear();
                ddlgroup.Items.Insert(0, new ListItem("Select Group", "0"));
                ddlObject.Items.Insert(0, new ListItem("Select Object", "0"));
                ddlDays.Items.Clear();
                ddlDays.Items.Add("Select Days");
            }
            allowStaticMethods("staticMethod();");
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlgroup.SelectedItem.Text != "Select Group")
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;

                GvdAllScheduling.Visible = false;
                ddlObject.Visible = true;
                List<ObjectModel> li = OBJ.GetObjectsByGroupId(Convert.ToInt32(ddlgroup.SelectedValue));
                var list2 = li.Select(o => new { o.Name, o.ObjectID }).Distinct();
                BindingClass.BindDropDown(ddlObject, list2, "Name", "ObjectID", "Select Object");

            }
            else
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;
                GvdAllScheduling.Visible = false;
                ddlObject.Items.Clear();
                ddlDays.Items.Add("Select Days");
                ddlDays.Items.Clear();
                ddlListItemDays();
                ddlObject.Items.Insert(0, new ListItem("Select Object", "0"));
            }
            allowStaticMethods("staticMethod();");
        }
        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObject.SelectedItem.Text != "Select Object")
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;
                ddlDays.Items.Clear();
                GvdAllScheduling.Visible = false;
                ddlDays.Visible = true;
                //if (Session["user"] != null)
                //{
                    ddlListItemDaysForUser();
                //}
                //else
                //{
                    //ddlListItemDays();
                //}

            }
            else
            {
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
                ddlSensor.Visible = false;
                ddlDays.Items.Clear();
                ddlDays.Items.Add("Select Days");
                GvdAllScheduling.Visible = false;
            }
            allowStaticMethods("staticMethod();");
        }
        protected void ddlDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDays.SelectedItem.Text != "Select Days")
            {
                if (ddlDays.SelectedItem.Text == "Holidays")
                {
                    GvdAllScheduling.Visible = false;

                    holidaysPanel.Visible = true;
                    GvdBindHolidayscheduling(Convert.ToInt32(ddlgroup.SelectedValue));
                }
                else
                {
                    btnSave.Visible = true;
                    ddlSensor.Visible = true;
                    BindddlSensor();
                    holidaysPanel.Visible = false;
                    GvdAllScheduling.Visible = true;
                    GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                }
            }
            else
            {
                ddlSensor.Visible = false;
                GvdAllScheduling.Visible = false;
                holidaysPanel.Visible = false;
                btnSave.Visible = false;
            }
            allowStaticMethods("staticMethod();");

        }
        protected void ddlSensor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int DayId = Convert.ToInt32(ddlDays.SelectedValue);
            int ObjectId = Convert.ToInt32(ddlObject.SelectedValue);
            int objSenID = Convert.ToInt32(ddlSensor.SelectedValue);
            if (DayId == 8)
            {
                List<SchedulingModel> li = SS.GetSchedulingByObjectAndOBSIDweekly(ObjectId, objSenID);
                if (li != null)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        if (li[i].Name == null)
                        {
                            li[i].Name = "ALL";
                        }
                    }
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
                else
                {
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
            }
            else
            {
                List<SchedulingModel> li = SS.GetSchedulingByObject_Day_OBSID(ObjectId, DayId, objSenID);
                if (li != null)
                {
                    if (li[0].Name == null)
                    {
                        li[0].Name = "ALL";
                    }
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
                else
                {
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
            }
            allowStaticMethods("staticMethod();");
        }


        public void GvdBindHolidayscheduling(int GroupID)
        {
            List<HolidaySchedulingModel> li = HSS.GetHolidaySchedulingByGroupId(GroupID);
            BindingClass.GridViewBind(GvdSHolidayscheduling, li);
            allowStaticMethods("staticMethod();");
        }
        public void GvdBindObjdetail(int objectId, int DayID)
        {
            if (DayID == 8)
            {
                List<SchedulingModel> li = SS.GetSchedulingByObjectAndOBSIDweekly(objectId, Convert.ToInt32(ddlSensor.SelectedValue));
                if (li != null)
                {
                    for (int i = 0; i < li.Count; i++)
                    {
                        if (li[i].Name == null)
                        {
                            li[i].Name = "ALL";
                        }
                    }
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
                else
                {
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
            }
            else
            {
                List<SchedulingModel> li = SS.GetSchedulingByObjectId(objectId, DayID);
                if (li != null)
                {
                    if (li[0].Name == null)
                    {
                        li[0].Name = "ALL";
                    }
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
                else
                {
                    GvdAllScheduling.DataSource = li;
                    GvdAllScheduling.DataBind();
                }
            }
            allowStaticMethods("staticMethod();");
        }
        #endregion

        #region Button Clicks
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool checkBox = true;
                int responce = 0;
                string starttime = "09:00AM";
                string endtime = "6:00PM";

                if (ddlDays.SelectedItem.Text != "Weekly")
                {
                    responce = SS.PostEquipmentScheduling(Convert.ToInt32(ddlObject.SelectedValue), starttime, endtime, Convert.ToInt32(ddlDays.SelectedValue), Convert.ToInt32(ddlSensor.SelectedValue), checkBox);
                    if (responce > 0)
                    {
                        GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                        _alert = AlertsClass.SuccessAdd;
                    }
                }
                if (ddlDays.SelectedItem.Text == "Weekly")
                {
                    for (int i = 1; i < 8; i++)
                    {
                        int days = i;
                        responce = SS.PostEquipmentScheduling(Convert.ToInt32(ddlObject.SelectedValue), starttime, endtime, days, Convert.ToInt32(ddlSensor.SelectedValue), checkBox);
                        _alert = AlertsClass.SuccessAdd;
                    }
                    GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                }
                BindddlSensor();
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnAddHoliday_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlDays.SelectedItem.Text == "Holidays")
                {
                    bool enable = false;
                    if (chkEnabledHoliday.Checked)
                    {enable = true;}
                    int responce = HSS.PostHolidayScheduling(txtHolidaydesc.Text, txtHolidaydt.Text, enable, Convert.ToInt32(ddlgroup.SelectedValue));
                    if (responce > 0)
                    {_alert = AlertsClass.SuccessAdd;}
                    else
                    {_alert = AlertsClass.ErrorWentWrong;}
                }
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region  LinkButton Clicks
        protected void linkUpdateHoliday_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    bool cbstatus = false;
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        CheckBox status = row.FindControl("chkstatus") as CheckBox;
                        if (status.Checked == true)
                        {cbstatus = true;}
                        _cmdArg = Convert.ToInt32(e.CommandArgument);
                         _status = HSS.PutHolidayScheduling(_cmdArg, cbstatus);
                         if (_status == true)
                        {_alert = AlertsClass.SuccessUpdate;}
                        else
                        {_alert = AlertsClass.ErrorWentWrong;}
                    }
                    GvdBindHolidayscheduling(Convert.ToInt32(ddlgroup.SelectedValue));
                }
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
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
                    _cmdArg = Convert.ToInt32(e.CommandArgument);
                    _status = HSS.DeleteHolidayScheduling(_cmdArg);
                    if (_status == true)
                    { _alert = AlertsClass.SuccessRemove; }
                    else
                    { _alert = AlertsClass.ErrorWentWrong; }
                }
                GvdBindHolidayscheduling(Convert.ToInt32(ddlgroup.SelectedValue));
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");

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
                    _cmdArg = Convert.ToInt32(e.CommandArgument);
                     _status = SS.DeleteScheduling(_cmdArg);
                    if (_status == true)
                    { _alert = AlertsClass.SuccessRemove; }
                    else
                    { _alert = AlertsClass.ErrorWentWrong; }
                }

                GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

            //if (e.CommandName == "RemoveID")
            //{
            //    string scheduleId = Convert.ToString(e.CommandArgument);
            //    Session["scheduleId"] = Convert.ToInt32(scheduleId);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openDeleteModal();", true);
            //}
        }
        protected void linkUpdateAll_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Update")
                {
                    int _cmdArg = Convert.ToInt32(e.CommandArgument);
                    SchedulingModel li = SS.GetSchedulingById(_cmdArg);
                    Session["scheduleId"] = _cmdArg.ToString();
                    int obs = li.ObjectSensorId;
                    int _days = li.Days;

                    bool cbstatus = false;
                    var lb = (LinkButton)sender;
                    var row = (GridViewRow)lb.NamingContainer;
                    if (row != null)
                    {
                        TextBox S_Time = row.FindControl("txtstarttime") as TextBox;
                        string StartTime = S_Time.Text;
                        TextBox E_Time = row.FindControl("txtendtime") as TextBox;
                        string EndTime = E_Time.Text;
                        CheckBox chk = (CheckBox)row.FindControl("chkstatus");
                        if (chk.Checked == true)
                        {
                            cbstatus = true;
                        }

                        bool responce = SS.PutEquipmentScheduling(_cmdArg, Convert.ToInt32(ddlObject.SelectedValue), StartTime, EndTime, _days, obs, cbstatus);
                        if (responce == true)
                        {
                            GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                            _alert = AlertsClass.SuccessUpdate;
                        }
                    }
                    allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
                }
               
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnDelAll_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int _cmdArg = Convert.ToInt32(e.CommandArgument);
                    bool delstatus = SS.DeleteScheduling(_cmdArg);
                    if (delstatus == true)
                    {
                        _alert = AlertsClass.SuccessRemove;
                    }
                    else
                    {
                        _alert = AlertsClass.ErrorWentWrong;
                    }
                    GvdBindObjdetail(Convert.ToInt32(ddlObject.SelectedValue), Convert.ToInt32(ddlDays.SelectedValue));
                    Session.Remove("scheduleId");
                }
                allowStaticMethods("ALerts('" + _alert + "');staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        #endregion

        #region  Clear Methods
        public void clearControls()
        {
            //BindingClass.ClearDropDown(ddlClient, "Select Client");
            //BindingClass.ClearDropDown(ddlGroup, "Select Branch");
            //BindingClass.ClearDropDown(ddlObject, "Select Device");
            //txtTavlClientID.Text = string.Empty;
            //txtTavlGroupID.Text = string.Empty;
            //txtTavlIP.Text = string.Empty;
            //gvdTavlIntegration.Visible = false;
        }
        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
        #endregion

    }
     

}