using TIOT_WEB.Models;
using TIOT_WEB.Service;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using TIOT_WEB.Common;
using System.Globalization;
using TIOT_WEB.BAL;


namespace TIOT_WEB
{
    public partial class Dashboard : System.Web.UI.Page
    {
        #region Class Instance
        public string Alert = "";
        GroupService GS = new GroupService();
        ClientService CL = new ClientService();
        ObjectService OBJ = new ObjectService();
        LoginGroupService LGS = new LoginGroupService();
        ObjectSensorService OBJ_SEN = new ObjectSensorService();
        EventLogService ELS = new EventLogService();
        CommandQueueService CQS = new CommandQueueService();
        StoreProcedureService SP = new StoreProcedureService();
        CommandHistoryService CHS = new CommandHistoryService();
        SensorCommandService SCS = new SensorCommandService();
        RelayNotificationService RNS = new RelayNotificationService();
        TAVLService TS = new TAVLService();
        AttendanceService AS = new AttendanceService();
        CommonBLL globalobj = new CommonBLL();
        DashboardBLL obj = new DashboardBLL();
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                try
                {
                    if (Session["admin"] != null)
                    {
                        ddlClientbind();
                        ddlgroup.Visible = false;

                    }
                    else if (Session["poweruser"] != null)
                    {
                        string ID = Session["poweruser"].ToString();
                        string[] powerSession = ID.Split(',');
                        int clientID = Convert.ToInt32(powerSession[1]);
                        ddlGroupbind(clientID);
                        sessionId.Text = clientID.ToString() + "ClientID";
                        ddlclientdiv.Visible = false;
                        allowStaticMethods("setInterval(function () {$(GetAlertsByClient('" + clientID + "'));}, 10000); $('#imgnotify').show();");
                    }
                    else if (Session["user"] != null)
                    {
                        ddlclientdiv.Visible = false;
                        ddlgroupdiv.Visible = false;
                        int loginID = Convert.ToInt32(Session["user"]);
                        int groupID = globalobj.getGroupIDForUser(loginID);
                        if (groupID != 0)
                        {
                            BindObjectRepeater(groupID);
                            sessionId.Text = groupID.ToString() + "GroupID";
                            allowStaticMethods("setInterval(function () {$(GetAlertsByGroup('" + groupID + "'))}, 10000); $('#imgnotify').show();");
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

        #region ddlSelection Methods
        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclient.SelectedValue != "0")
                {
                    ddlgroup.Visible = true;
                    int _sltdval = Convert.ToInt32(ddlclient.SelectedValue);
                    sessionId.Text = _sltdval.ToString();
                    int val = Convert.ToInt32(_sltdval);
                    ddlGroupbind(val);
                    if (Session["admin"] != null)
                    { allowStaticMethods("GetAlertsByClient('" + val + "'); setInterval(function () {$(GetAlertsByClient('" + val + "'))}, 60000); $('#tblnotify').empty(); $('#imgnotify').show();ddlselect2('.ddlSelect');"); }
                }
                else
                {
                    BindingClass.ClearDropDown(ddlgroup, "Select Branch");
                    allowStaticMethods("ddlselect2('.ddlSelect')");
                }
                BindingClass.ClearRepeaterView(rptObject);
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlgroup.SelectedValue != "0")
                {
                    Timer1.Enabled = true;
                    rptObject.Visible = true;
                    var _sltdval = ddlgroup.SelectedValue;
                    int val = Convert.ToInt32(_sltdval);
                    BindObjectRepeater(val);
             
                }
                else
                { BindingClass.ClearRepeaterView(rptObject); }
                allowStaticMethods("ddlselect2('.ddlSelect');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region ddlBind
        public void ddlGroupbind(int ClientId)
        {
            try
            {
                List<GroupIDName> li = globalobj.getGroupList(ClientId);
                if (li.Count > 0)
                { BindingClass.BindDropDown(ddlgroup, li, "Name", "GroupID", "Select Branch"); }
                else
                { BindingClass.ClearDropDown(ddlgroup, "Select Branch"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void ddlClientbind()
        {
            try
            {
                List<ClientIDName> li = globalobj.getClientList();
                if (li.Count > 0)
                { BindingClass.BindDropDown(ddlclient, li, "Name", "ClientID", "Select Client"); }
                else
                { BindingClass.ClearDropDown(ddlclient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Bindings Methods
        public void BindObjectRepeater(int groupId)
        {
            try
            {
                divIOT.Visible = false; divAttendance.Visible = false; divtracking.Visible = false;
                List<ObjectListDashboard> li = globalobj.getObjectList(groupId);
                if (li.Count > 0)
                {
                    rptObject.DataSource = li;
                    rptObject.DataBind();
                    List<ObjectListDashboard> tracker = new List<ObjectListDashboard>(); tracker = li.Where(x => x.TavlStatus == true).ToList();
                    if (tracker.Count > 0)
                    { rptTracker.DataSource = tracker; rptTracker.DataBind(); }
                    else
                    { rptTracker.DataSource = tracker; rptTracker.DataBind(); divtracking.Visible = true; }
                    List<ObjectListDashboard> attendence = new List<ObjectListDashboard>(); attendence = li.Where(x => x.AttendanceStatus == true).ToList();
                    if (tracker.Count > 0)
                    { rptAttendance.DataSource = attendence; rptAttendance.DataBind(); }
                    else
                    { rptAttendance.DataSource = attendence; rptAttendance.DataBind(); divAttendance.Visible = true; }
                    List<ObjectRelayStatus> LStatus = new List<ObjectRelayStatus>();
                    List<ObjectRelaysByGroup> LRstatus = new List<ObjectRelaysByGroup>();
                    for (int i = 0; i < li.Count; i++)
                    {
                        LStatus = obj.getObjectRelays(li[i].ObjectID, li[i].RelayStatus);
                        if (LStatus != null)
                        {

                            ObjectRelaysByGroup lsi = new ObjectRelaysByGroup();
                            lsi.Device = LStatus[i].Device;
                            lsi.ObjectID = LStatus[i].ObjectID;
                            lsi.DateTimeStamp = LStatus[i].DateTimeStamp;
                            if (li[i].RelayStatus == true)
                            {
                                lsi.RelayStatus = 1;
                            }
                            if (li[i].RelayStatus == false)
                            {
                                lsi.RelayStatus = 0;
                            }
                            if (lsi.DateTimeStamp  < DateTime.Now.AddMinutes(-10))
                            {
                                lsi.StatusClass = "fa fa-plug fa-fw faTopDate-red";
                            }
                            else
                            {
                                lsi.StatusClass = "fa fa-plug fa-fw faTopDate-primary";
                            }

                            LRstatus.Add(lsi);
                        }
                    }
                    RepeatergroupDetail.DataSource = LRstatus;
                    RepeatergroupDetail.DataBind();
                }
                else
                { divIOT.Visible = true; divAttendance.Visible = true; divtracking.Visible = true; }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void ObjectGridBind(int objectId)
        {
            try
            {
                ObjectStatus li = globalobj.getObjectStatus(objectId);
                if (li != null)
                {
                    lblobj.Text = li.Name;
                    lblsim.Text = li.SimNumber;
                    lblObjId.Text = objectId.ToString();
                    lblTime.Text = li.LastRecordReceived.ToString();
                    GvdBindObjdetail(objectId, li.RelayStatus);
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void GvdBindObjdetail(int objectId, bool relayStatus)
        {
            try
            {
                List<ObjectRelayStatus> lirelays = obj.getObjectRelays(objectId, relayStatus);
                List<ObjectAnalog> liAin = obj.getObjectAin(objectId);
                List<ObjectDigital> liDin = obj.getObjectDin(objectId);
                List<ObjectTemperature> liTemp = obj.getObjectTemp(objectId);
                Gvdobjsensor.DataSource = lirelays; Gvdobjsensor.DataBind();
                rptappliances.DataSource = lirelays; rptappliances.DataBind();
                rptDIN.DataSource = liDin; rptDIN.DataBind();
                rptdigitalsensor.DataSource = liDin; rptdigitalsensor.DataBind();
                rptAnalog.DataSource = liAin; rptAnalog.DataBind();
                rptAnalogsensor.DataSource = liAin; rptAnalogsensor.DataBind();
                rptTemp.DataSource = liTemp; rptTemp.DataBind();
                rpttempsensor.DataSource = liTemp; rpttempsensor.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        public void BindAttendanceList(int ClientId, string IP)
        {
            try
            {
                List<AttendanceModelDashboard> list = obj.getAttendance(ClientId);
                list = list.OrderBy(x => x.ClockTime).ToList();
                rptAttendancesuccess.DataSource = list;
                rptAttendancesuccess.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Link buttton and Item command
        protected void rptObject_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("lnkbtnviewObjdt"))
                {
                    //Session["ObjIDTimer"] = null;
                    string val = Convert.ToString(e.CommandArgument);
                    int id = Convert.ToInt32(val);
                    //Session["ObjIDTimer"] = id;
                    if (val != null)
                    {
                        ObjectGridBind(id);
                        allowStaticMethods("$('#myModal').modal();togl();chngeDin();ddlselect2('.ddlSelect');");
                    }
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void rptAttendance_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("lnkbtnviewATDN"))
                {

                    string val = Convert.ToString(e.CommandArgument);
                    if (val != null)
                    {
                        int id = Convert.ToInt32(val);
                        BindAttendanceList(id, "s");
                        allowStaticMethods("$('#myAttendanceModal').modal();ddlselect2('.ddlSelect');");
                    }
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void Gvdobjsensor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Control ctrl = e.CommandSource as Control;
                GridViewRow row = ctrl.Parent.NamingContainer as GridViewRow;
                string command = Convert.ToString(e.CommandArgument);
                string[] splitCommand = command.Split(',');
                int SensorID = Convert.ToInt32(splitCommand[0]);
                int ObjectID = Convert.ToInt32(splitCommand[1]);

                if (e.CommandName.Equals("chkstatus"))
                {

                    LinkButton btn = (LinkButton)row.FindControl("lnkbtnStatus");
                    string btntext = btn.Text;
                    int res = obj.getRelayCommand(ObjectID, SensorID, btntext);
                    if (res != 0)
                    {
                        int returnValue = CQS.PostCommand(ObjectID, res);
                    }
                    commandUser(ObjectID, SensorID, res);
                    allowStaticMethods("toastr.warning('Your request has been successfully submitted, It will be executed in few moments', 'Request',{positionClass:'toast-bottom-right'}); chngeDin();ddlselect2('.ddlSelect');");
                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void lnkbtnOFF_Click(object sender, EventArgs e)
        {
            try
            {
                int ObjectId = Convert.ToInt32(lblObjId.Text);
                int res = obj.getRelayCommand(ObjectId, 0, "OFF");
                if (res != 0)
                { int returnValue = CQS.PostCommand(ObjectId, res); }
                commandUser(ObjectId, 0, res);
                allowStaticMethods("toastr.warning('Your request has been successfully submitted, It will be executed in few moments', 'OFF',{positionClass:'toast-bottom-right'}); chngeDin();ddlselect2('.ddlSelect');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void lnkBtnAllON_Click(object sender, EventArgs e)
        {
            try
            {
                int ObjectId = Convert.ToInt32(lblObjId.Text);
                int res = obj.getRelayCommand(ObjectId, 0, "ON");
                if (res != 0)
                { int returnValue = CQS.PostCommand(ObjectId, res); }
                commandUser(ObjectId, 0, res);
                allowStaticMethods("toastr.warning('Your request has been successfully submitted, It will be executed in few moments', 'ON',{positionClass:'toast-bottom-right'}); chngeDin();ddlselect2('.ddlSelect');");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (ddlgroup.SelectedValue != "0")
            { BindObjectRepeater(Convert.ToInt32(ddlgroup.SelectedValue)); }
            allowStaticMethods("ddlselect2('.ddlSelect');");

        }


        public void commandUser(int objectId, int sensorId, int commandId)
        {
            try
            {
                CommandLogUserModel _obj = new CommandLogUserModel();
                _obj.CommandID = commandId;
                _obj.ObjectID = objectId;
                _obj.SensorID = sensorId;
                if (Session["admin"] != null)
                {
                    _obj.UserID = 2;
                }
                if (Session["poweruser"] != null)
                {
                    string temp = Session["poweruser"].ToString();
                    string[] IdSplit = temp.Split(',');
                    _obj.UserID = Convert.ToInt32(IdSplit[0]);
                }
                if (Session["user"] != null)
                {
                    string temp = Session["user"].ToString();
                    string[] IdSplit = temp.Split(',');
                    _obj.UserID = Convert.ToInt32(IdSplit[0]);
                }
                obj.postCommandLogUser(_obj);
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        #endregion

        #region Clear Methods & Alerts
        public void ClearControls()
        {
            try
            {
                List<object> li = new List<object>();
                BindingClass.ClearDropDown(ddlclient, "Select Client");
                BindingClass.ClearDropDown(ddlgroup, "Select Group");
                BindingClass.RepeaterViewBind(rptObject, li);
                BindingClass.RepeaterViewBind(rptTracker, li);
                BindingClass.RepeaterViewBind(rptAttendance, li);
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
        #endregion

        #region static Web Methods

        [WebMethod]
        public static string GetNONALerts()
        {
            ObjectSensorService OBJ_SEN = new ObjectSensorService();
            CommandHistoryService CHS = new CommandHistoryService();
            SensorCommandService SCS = new SensorCommandService();
            List<CommandHistoryModel> li = CHS.GetNonAlerts();
            if (li != null)
            {
                DataTable dt = ToDataTable(li);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int cmdId = Convert.ToInt32(dt.Rows[i]["CommandId"]);
                    int CmdHistoryId = Convert.ToInt32(dt.Rows[i]["CommandHistoryId"]);
                    List<SensorCommandModel> lii = SCS.GetSensorCommandbyCmdId(cmdId);
                    int objId = Convert.ToInt32(dt.Rows[i]["ObjectId"]);
                    int sensorId = Convert.ToInt32(lii[0].SensorID);
                    string text = "";
                    if (sensorId != 0)
                    {
                        ObjNameObjSNameCateNameModel _li = OBJ_SEN.GetObjectSensorsByObjIdForNotify(objId, sensorId);
                        if (_li != null)
                        {
                            text = "Your " + _li.ObjectName + " " + _li.CategoryName + " at " + _li.ObjectSensorName + " is " + lii[0].Description + "break Alert " + dt.Rows[i]["DateTimeStamp"] + " ";
                            bool status = CHS.PutAlertState(CmdHistoryId);
                            return text;
                        }
                    }
                    else
                    {
                        if (cmdId == 3)
                        {
                            text = "Your " + dt.Rows[i]["DeviceName"].ToString() + " Sensors are " + lii[0].Description + "break Alert " + dt.Rows[i]["DateTimeStamp"] + " ";
                            bool status = CHS.PutAlertState(CmdHistoryId);
                        }
                        if (cmdId == 4)
                        {
                            text = "Your " + dt.Rows[i]["DeviceName"].ToString() + " Sensors are " + lii[0].Description + "break Alert " + dt.Rows[i]["DateTimeStamp"] + " ";
                            bool status = CHS.PutAlertState(CmdHistoryId);
                        }
                        return text;
                    }
                }
            }
            return "";
        }
        [WebMethod]
        public static string NotificationsByClient(int clientId, string starttime, string endtime)
        {
            DashboardBLL obj = new DashboardBLL();
            starttime = starttime.Replace(',', ' '); DateTime sttime = DateTime.Parse(starttime);
            endtime = endtime.Replace(',', ' '); DateTime edtime = DateTime.Parse(endtime);
            var objects = obj.getCustomNotificationbyClient(clientId, sttime, edtime);
            if (objects.Count > 0)
            {
                string result = JsonConvert.SerializeObject(objects);
                return result;
            }
            return "";
        }
        [WebMethod]
        public static string NotificationsByGroup(int groupId, string starttime, string endtime)
        {
            DashboardBLL obj = new DashboardBLL();
            starttime = starttime.Replace(',', ' '); DateTime sttime = DateTime.Parse(starttime);
            endtime = endtime.Replace(',', ' '); DateTime edtime = DateTime.Parse(endtime);
            var objects = obj.getCustomNotificationbyGroup(groupId, sttime, edtime);
            if (objects.Count > 0)
            {
                string result = JsonConvert.SerializeObject(objects);
                return result;
            }
            return "";
        }
        [WebMethod]
        public static string GetObjectbyId(int ObjectId)
        {
            ObjectService OBS = new ObjectService();
            var objects = OBS.GetObjectById(ObjectId);
            if (objects != null)
            {
                string result = JsonConvert.SerializeObject(objects);
                return result;
            }
            return "";
        }

        //Tavl WebMethod
        [WebMethod]
        public static string TavlObject(int ClientId, int GroupId, string IP)
        {
            TAVLService TS = new TAVLService();
            var _object = TS.GetTAVLObjectList(ClientId, GroupId, IP);
            if (_object != null)
            {
                string result = JsonConvert.SerializeObject(_object);
                return result;
            }
            return "";
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
        #endregion

        protected void RepeatergroupDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string objectID = (e.Item.FindControl("hfObjectId") as HiddenField).Value;
                string[] arr = objectID.Split(',');
                bool RS = false;
                if (arr[1] == "0") { RS = false; } if (arr[1] == "1") { RS = true; }
                Repeater rptGroups = e.Item.FindControl("rptgroup") as Repeater;

                List<ObjectRelayStatus> LRstatus = new List<ObjectRelayStatus>();
                List<ObjectRelayStatus> LRstatus2 = new List<ObjectRelayStatus>();

                LRstatus = obj.getObjectRelays(Convert.ToInt32(arr[0]), RS);
                if (LRstatus != null)
                {
                    for (int i = 0; i < LRstatus.Count; i++)
                    {
                        ObjectRelayStatus lsi = new ObjectRelayStatus();
                        lsi.Name = LRstatus[i].Name;
                        lsi.Category = LRstatus[i].Category;
                        lsi.Status = LRstatus[i].Status;
                        lsi.StatusIOTClass = LRstatus[i].StatusIOTClass;
                        LRstatus2.Add(lsi);
                    }
                }
                rptGroups.DataSource = LRstatus2;
                rptGroups.DataBind();
            }
        }
    }


}

