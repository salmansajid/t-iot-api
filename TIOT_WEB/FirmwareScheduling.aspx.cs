using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.Models;
using TIOT_WEB.Service;

namespace TIOT_WEB
{
    public partial class HardwareBasedScheduling : System.Web.UI.Page
    {
        #region ClassInstances

        public string Alert = "";        
        ObjectService OBJ = new ObjectService();
        ReportService RS = new ReportService();
        ClientService CL = new ClientService();
        GroupService GS = new GroupService();
        LoginGroupService LGS = new LoginGroupService();
        EventConfigurationService ECS = new EventConfigurationService();

        #endregion

        #region PageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    {
                        ddlClientbind();
                        BindingClass.ClearDropDown(ddlgroup, "Select Branch");
                        BindingClass.ClearDropDown(ddlObject, "Select Device");
                        ddlScheduleBind();
                        ddlHoursBind();
                        ddlMinsBind();
                        ddlSecsBind();
                    }
                    //else if (Session["poweruser"] != null)
                    //{
                    //    var list = (LoginModelForUser)Session["poweruser"];
                    //    int _clId = list.ClientID;
                    //    ddlGroupbind(_clId);
                    //    ddlgroup.Visible = true;
                    //}
                    //else if (Session["user"] != null)
                    //{
                    //    var list = (LoginModelForUser)Session["user"];
                    //    int sessionLoginID = list.LoginID;
                    //    LoginGroupModel li = LGS.GetLoginGroupByLogin(sessionLoginID);
                    //    if (li != null)
                    //    {
                    //        ddlObjectbind(li.GroupID);
                    //        ddlObject.Visible = true;
                    //    }
                    //}
                }
                catch (Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType());}
            }
        }

        #endregion

        #region ddlBind

        protected void ddlClientbind()
        {
            try
            {
                List<ClientModel> li = CL.GetClient();
                if (li != null)
                {
                    var list = li.Select(o => new { o.Name, o.ClientID });
                    BindingClass.BindDropDown(ddlclient, list, "Name", "ClientID", "Select Client");
                }
                else
                { BindingClass.ClearDropDown(ddlclient, "Select Client"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlGroupbind(int ClientId)
        {
            try
            {
                List<GroupModel> li = GS.GetGroupByClientId(ClientId);
                if (li != null)
                {
                    var list = li.Select(o => new { o.Name, o.GroupID });
                    BindingClass.BindDropDown(ddlgroup, list, "Name", "GroupID", "Select Branch");
                }
                else
                { BindingClass.ClearDropDown(ddlgroup, "Select Branch"); }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlObjectbind(int GroupId)
        {
            try
            {
                List<ObjectModel> li = OBJ.GetObjectsByGroupId(GroupId);
                if (li != null)
                {
                    var list = li.Select(o => new { o.Name, o.ObjectID });
                    BindingClass.BindDropDown(ddlObject, list, "Name", "ObjectID", "Select Device");
                }
                else
                {BindingClass.ClearDropDown(ddlObject, "Select Device");}
            }
            catch(Exception)
            {BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType());}
        }

        protected void ddlScheduleBind()
        {
            try
            {
                ListItem[] items = new ListItem[17];
                items[0] = new ListItem("Select Schedule", "0");
                items[1] = new ListItem("1","92");
                items[2] = new ListItem("2", "93");
                items[3] = new ListItem("3", "94");
                items[4] = new ListItem("4", "95");
                items[5] = new ListItem("5", "96");
                items[6] = new ListItem("6", "97");
                items[7] = new ListItem("7", "98");
                items[8] = new ListItem("8", "99");
                items[9] = new ListItem("9", "100");
                items[10] = new ListItem("10", "101");
                items[11] = new ListItem("11", "102");
                items[12] = new ListItem("12", "103");
                items[13] = new ListItem("13", "104");
                items[14] = new ListItem("14", "105");
                items[15] = new ListItem("15", "106");
                items[16] = new ListItem("16", "107");
                ddlSchedule.Items.AddRange(items);
                ddlSchedule.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void ddlHoursBind()
        {
            try
            {
                ListItem[] items = new ListItem[25];
                items[0] = new ListItem("Hours", "0");
                items[1] = new ListItem("12am", "00");
                items[2] = new ListItem("01am", "01");
                items[3] = new ListItem("02am", "02");
                items[4] = new ListItem("03am", "03");
                items[5] = new ListItem("04am", "04");
                items[6] = new ListItem("05am", "05");
                items[7] = new ListItem("06am", "06");
                items[8] = new ListItem("07am", "07");
                items[9] = new ListItem("08am", "08");
                items[10] = new ListItem("09am", "09");
                items[11] = new ListItem("10am", "0A");
                items[12] = new ListItem("11am", "0B");
                items[13] = new ListItem("12pm", "0C");
                items[14] = new ListItem("01pm", "0D");
                items[15] = new ListItem("02pm", "0E");
                items[16] = new ListItem("03pm", "0F");
                items[17] = new ListItem("04pm", "10");
                items[18] = new ListItem("05pm", "11");
                items[19] = new ListItem("06pm", "12");
                items[20] = new ListItem("07pm", "13");
                items[21] = new ListItem("08pm", "14");
                items[22] = new ListItem("09pm", "15");
                items[23] = new ListItem("10pm", "16");
                items[24] = new ListItem("11pm", "17");
                //items[1] = new ListItem("00am", "00");
                //items[2] = new ListItem("01am", "01");
                //items[3] = new ListItem("02am", "02");
                //items[4] = new ListItem("03am", "03");
                //items[5] = new ListItem("04am", "04");
                //items[6] = new ListItem("05am", "05");
                //items[7] = new ListItem("06am", "06");
                //items[8] = new ListItem("07am", "07");
                //items[9] = new ListItem("08am", "08");
                //items[10] = new ListItem("09am", "09");
                //items[11] = new ListItem("10am", "0A");
                //items[12] = new ListItem("11am", "0B");
                //items[13] = new ListItem("12pm", "0C");
                //items[14] = new ListItem("13pm", "0D");
                //items[15] = new ListItem("14pm", "0E");
                //items[16] = new ListItem("15pm", "0F");
                //items[17] = new ListItem("16pm", "10");
                //items[18] = new ListItem("17pm", "11");
                //items[19] = new ListItem("18pm", "12");
                //items[20] = new ListItem("19pm", "13");
                //items[21] = new ListItem("20pm", "14");
                //items[22] = new ListItem("21pm", "15");
                //items[23] = new ListItem("22pm", "16");
                //items[24] = new ListItem("23pm", "17");
                //items[25] = new ListItem("24", "18");
                ddlhours.Items.AddRange(items);
                ddlhours.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlMinsBind()
        {
            try
            {
                ListItem[] items = new ListItem[61];
                items[0] = new ListItem("Minutes", "0");
                items[1] = new ListItem("00", "00");
                items[2] = new ListItem("01", "01");
                items[3] = new ListItem("02", "02");
                items[4] = new ListItem("03", "03");
                items[5] = new ListItem("04", "04");
                items[6] = new ListItem("05", "05");
                items[7] = new ListItem("06", "06");
                items[8] = new ListItem("07", "07");
                items[9] = new ListItem("08", "08");
                items[10] = new ListItem("09", "09");
                items[11] = new ListItem("10", "0A");
                items[12] = new ListItem("11", "0B");
                items[13] = new ListItem("12", "0C");
                items[14] = new ListItem("13", "0D");
                items[15] = new ListItem("14", "0E");
                items[16] = new ListItem("15", "0F");
                items[17] = new ListItem("16", "10");
                items[18] = new ListItem("17", "11");
                items[19] = new ListItem("18", "12");
                items[20] = new ListItem("19", "13");
                items[21] = new ListItem("20", "14");
                items[22] = new ListItem("21", "15");
                items[23] = new ListItem("22", "16");
                items[24] = new ListItem("23", "17");
                items[25] = new ListItem("24", "18");
                items[26] = new ListItem("25", "19");
                items[27] = new ListItem("26", "1A");
                items[28] = new ListItem("27", "1B");
                items[29] = new ListItem("28", "1C");
                items[30] = new ListItem("29", "1D");
                items[31] = new ListItem("30", "1E");
                items[32] = new ListItem("31", "1F");
                items[33] = new ListItem("32", "20");
                items[34] = new ListItem("33", "21");
                items[35] = new ListItem("34", "22");
                items[36] = new ListItem("35", "23");
                items[37] = new ListItem("36", "24");
                items[38] = new ListItem("37", "25");
                items[39] = new ListItem("38", "26");
                items[40] = new ListItem("39", "27");
                items[41] = new ListItem("40", "28");
                items[42] = new ListItem("41", "29");
                items[43] = new ListItem("42", "2A");
                items[44] = new ListItem("43", "2B");
                items[45] = new ListItem("44", "2C");
                items[46] = new ListItem("45", "2D");
                items[47] = new ListItem("46", "2E");
                items[48] = new ListItem("47", "2F");
                items[49] = new ListItem("48", "30");
                items[50] = new ListItem("49", "31");
                items[51] = new ListItem("50", "32");
                items[52] = new ListItem("51", "33");
                items[53] = new ListItem("52", "34");
                items[54] = new ListItem("53", "35");
                items[55] = new ListItem("54", "36");
                items[56] = new ListItem("55", "37");
                items[57] = new ListItem("56", "38");
                items[58] = new ListItem("57", "39");
                items[59] = new ListItem("58", "3A");
                items[60] = new ListItem("59", "3B");


                ddlmins.Items.AddRange(items);
                ddlmins.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        protected void ddlSecsBind()
        {
            try
            {
                ListItem[] items = new ListItem[61];
                items[0] = new ListItem("Seconds", "0");
                items[1] = new ListItem("00", "00");
                items[2] = new ListItem("01", "01");
                items[3] = new ListItem("02", "02");
                items[4] = new ListItem("03", "03");
                items[5] = new ListItem("04", "04");
                items[6] = new ListItem("05", "05");
                items[7] = new ListItem("06", "06");
                items[8] = new ListItem("07", "07");
                items[9] = new ListItem("08", "08");
                items[10] = new ListItem("09", "09");
                items[11] = new ListItem("10", "0A");
                items[12] = new ListItem("11", "0B");
                items[13] = new ListItem("12", "0C");
                items[14] = new ListItem("13", "0D");
                items[15] = new ListItem("14", "0E");
                items[16] = new ListItem("15", "0F");
                items[17] = new ListItem("16", "10");
                items[18] = new ListItem("17", "11");
                items[19] = new ListItem("18", "12");
                items[20] = new ListItem("19", "13");
                items[21] = new ListItem("20", "14");
                items[22] = new ListItem("21", "15");
                items[23] = new ListItem("22", "16");
                items[24] = new ListItem("23", "17");
                items[25] = new ListItem("24", "18");
                items[26] = new ListItem("25", "19");
                items[27] = new ListItem("26", "1A");
                items[28] = new ListItem("27", "1B");
                items[29] = new ListItem("28", "1C");
                items[30] = new ListItem("29", "1D");
                items[31] = new ListItem("30", "1E");
                items[32] = new ListItem("31", "1F");
                items[33] = new ListItem("32", "20");
                items[34] = new ListItem("33", "21");
                items[35] = new ListItem("34", "22");
                items[36] = new ListItem("35", "23");
                items[37] = new ListItem("36", "24");
                items[38] = new ListItem("37", "25");
                items[39] = new ListItem("38", "26");
                items[40] = new ListItem("39", "27");
                items[41] = new ListItem("40", "28");
                items[42] = new ListItem("41", "29");
                items[43] = new ListItem("42", "2A");
                items[44] = new ListItem("43", "2B");
                items[45] = new ListItem("44", "2C");
                items[46] = new ListItem("45", "2D");
                items[47] = new ListItem("46", "2E");
                items[48] = new ListItem("47", "2F");
                items[49] = new ListItem("48", "30");
                items[50] = new ListItem("49", "31");
                items[51] = new ListItem("50", "32");
                items[52] = new ListItem("51", "33");
                items[53] = new ListItem("52", "34");
                items[54] = new ListItem("53", "35");
                items[55] = new ListItem("54", "36");
                items[56] = new ListItem("55", "37");
                items[57] = new ListItem("56", "38");
                items[58] = new ListItem("57", "39");
                items[59] = new ListItem("58", "3A");
                items[60] = new ListItem("59", "3B");
                ddlsecs.Items.AddRange(items);
                ddlsecs.DataBind();
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }

        #endregion

        #region ddlSelection

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlclient.SelectedValue != "0")
                {
                    ddlGroupbind(Convert.ToInt32(ddlclient.SelectedValue));
                }
                else
                {
                    BindingClass.ClearDropDown(ddlgroup, "Select Branch");
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                }
                allowStaticMethods("staticMethod();");
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
                    ddlObjectbind(Convert.ToInt32(ddlgroup.SelectedValue));
                }
                else
                {
                    BindingClass.ClearDropDown(ddlObject, "Select Device");
                }
                allowStaticMethods("staticMethod();");
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }




        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            string scheduleID = ddlSchedule.SelectedValue;
            string hours = ddlhours.SelectedValue;
            string mins = ddlmins.SelectedValue;
            string secs = ddlsecs.SelectedValue;


            string monday = ChkMonday.Checked ? "1" : "0";
            string tuesday = ChkTuesday.Checked ? "1" : "0";
            string wednesday = ChkWednesday.Checked ? "1" : "0";
            string thursday = ChkThursday.Checked ? "1" : "0";
            string friday = ChkFriday.Checked ? "1" : "0";
            string saturday = ChkSaturday.Checked ? "1" : "0";
            string sunday = ChkSunday.Checked ? "1" : "0";

            string weekdays = monday + tuesday + wednesday + thursday + friday + saturday + sunday + "0";
            string weekdayshex = Convert.ToInt32(weekdays, 2).ToString("X");


            string switchoneON = onS1.Checked ? "1" : "0";
            string switchtwoON = onS2.Checked ? "1" : "0";
            string switchthreeON = onS3.Checked ? "1" : "0";
            string switchfourON = onS4.Checked ? "1" : "0";
            string switchfiveON = onS5.Checked ? "1" : "0";
            string switchsixON = onS6.Checked ? "1" : "0";
            string switchsevenON = onS7.Checked ? "1" : "0";
            string switcheightON = onS8.Checked ? "1" : "0";

            string switchesON = switcheightON + switchsevenON + switchsixON + switchfiveON + switchfourON + switchthreeON + switchtwoON + switchoneON;
            string switchesONALL = Convert.ToInt32(switchesON, 2).ToString("X2");

            string switchoneOFF = offS1.Checked ? "1" : "0";
            string switchtwoOFF = offS2.Checked ? "1" : "0";
            string switchthreeOFF = offS3.Checked ? "1" : "0";
            string switchfourOFF = offS4.Checked ? "1" : "0";
            string switchfiveOFF = offS5.Checked ? "1" : "0";
            string switchsixOFF = offS6.Checked ? "1" : "0";
            string switchsevenOFF = offS7.Checked ? "1" : "0";
            string switcheightOFF = offS8.Checked ? "1" : "0";

            string switchesOFF = switcheightOFF + switchsevenOFF + switchsixOFF + switchfiveOFF + switchfourOFF + switchthreeOFF + switchtwoOFF + switchoneOFF;
            string switchesOFFALL = Convert.ToInt32(switchesOFF, 2).ToString("X2");

            string Schedulecommand = "";
            if(ddlObject.SelectedValue == "3")
            {
                Schedulecommand = "@" + scheduleID + ":" + weekdayshex + hours + mins + secs + switchesOFFALL + "00" + switchesONALL + "00;@";
            }
            else
            {
                Schedulecommand = "@" + scheduleID + ":" + weekdayshex + hours + mins + secs + switchesONALL + "00" + switchesOFFALL + "00;@";
            }


            txtcommd.Text = Schedulecommand;

            allowStaticMethods("staticMethod();");
        }

        public void allowStaticMethods(string jsfunctions)
        { BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions); }
    }
}