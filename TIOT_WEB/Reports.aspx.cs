using TIOT_WEB.Models;
using TIOT_WEB.Service;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class Reports : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string Alert = "";
        ObjectService OBJ = new ObjectService();
        ReportService RS = new ReportService();
        ClientService CL = new ClientService();
        GroupService GS = new GroupService();
        LoginGroupService LGS = new LoginGroupService();
        StoreProcedureService SPS = new StoreProcedureService();
        SwitchesReportServices SRS = new SwitchesReportServices();
        ClientModel client = new ClientModel();
        CommonBLL objC = new CommonBLL();
        ReportsBLL obj = new ReportsBLL();

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
                        ddlclientbind();
                        ddlgroup.Visible = false;
                        ddlObject.Visible = false;
                    }
                    else if (Session["poweruser"] != null)
                    {
                        ddlObject.Visible = false;
                        var list = (LoginModelForUser)Session["poweruser"];
                        int _clId = list.ClientID;
                        ddlGroupbind(_clId);
                        dvcl.Visible = false;
                        ddlgroup.Visible = true;

                    }
                    else if (Session["user"] != null)
                    {

                        dvcl.Visible = false;
                        dvgp.Visible = false;
                        var list = (LoginModelForUser)Session["user"];
                        int sessionLoginID = list.LoginID;
                        LoginGroupModel li = LGS.GetLoginGroupByLogin(sessionLoginID);
                        if (li != null)
                        {
                            ddlObjectbind(li.GroupID);
                            ddlObject.Visible = true;
                        }


                    }
                }
                catch (Exception)
                {
                    Alert = AlertsClass.ErrorWentWrong;
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "ALerts('" + Alert + "')", true);
            }

        } 
        #endregion

        #region LinkButton Clicks
        protected void lnkbtnEQConsumpRpt_Click(object sender, EventArgs e)
        {
            string calender = txtdtrange.Text;
            string[] cal = calender.Split('-');
            string StrStartdate = cal[0]; string StrEnddate = cal[1];
            DateTime Startdate = Convert.ToDateTime(StrStartdate);
            DateTime Enddate = Convert.ToDateTime(StrEnddate);

            GvdBind_Consumption(Convert.ToInt32(ddlObject.SelectedValue), Startdate, Enddate);
        }
        protected void lnkbtnEQControlingRpt_Click(object sender, EventArgs e)
        {
            string calender = txtdtrange.Text;
            string[] cal = calender.Split('-');
            string StrStartdate = cal[0]; string StrEnddate = cal[1];
            DateTime Startdate = Convert.ToDateTime(StrStartdate);
            DateTime Enddate = Convert.ToDateTime(StrEnddate);
            GvdBind_Controlling(Convert.ToInt32(ddlObject.SelectedValue), Startdate, Enddate);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal2", "$('#myModal2').modal(); loadGraph(); gvdtempete('#gv_ControllingReport');", true);
        }
        protected void lnkbtnTemperature_Click(object sender, EventArgs e)
        {
            GvdBind_Temperature();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal3", "$('#myModal3').modal(); gvdtempete('#gvdTemperature');", true);
        }
        #endregion
        #region Binding Controls
        public void ddlclientbind()
        {
            try
            {
                List<ClientIDName> list = objC.getClientList();
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlclient, list, "Name", "ClientID", "Select Client"); }
                else
                { BindingClass.ClearDropDown(ddlclient, "Select Client"); }
            }
            catch (Exception) { }
            //{ Alert = AlertsClass.ErrorWentWrong; AlertScriptManager(Alert); }
        }
        public void ddlGroupbind(int ClientId)
        {
            try
            {
                List<GroupIDName> li = objC.getGroupList(ClientId);
                if (li.Count > 0)
                { BindingClass.BindDropDown(ddlgroup, li, "Name", "GroupID", "Select Branch"); }
                else
                { BindingClass.ClearDropDown(ddlgroup, "Select Branch"); }
            }
            catch (Exception) { }
            //{ Alert = AlertsClass.ErrorWentWrong; AlertScriptManager(Alert); }
        }
        protected void ddlObjectbind(int GroupId)
        {
            try
            {
                List<ObjectListDashboard> list = objC.getObjectList(GroupId);
                if (list.Count > 0)
                { BindingClass.BindDropDown(ddlObject, list, "Name", "ObjectID", "Select Device"); }
                else
                { BindingClass.ClearDropDown(ddlObject, "Select Device"); }
            }
            catch (Exception) { }
            //{ Alert = AlertsClass.ErrorWentWrong; AlertScriptManager(Alert); }
        }


        public void GvdBind_Controlling(int ObjectId, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate.Date == DateTime.Now.Date)
            {
                List<SwitchesReportControllingModel> Li = SRS.GetCurrentdateSwitchesReportControling(ObjectId);
                lblname2.Text = "";
                lblname2.Text = ddlObject.SelectedItem.Text;
                BindingClass.GridViewBind(gv_ControllingReport, Li);
            }
            else
            {
                List<SwitchesReportControllingModel> Li = obj.getControllingByDT(ObjectId, StartDate, EndDate);
                lblname2.Text = "";
                lblname2.Text = ddlObject.SelectedItem.Text;
                BindingClass.GridViewBind(gv_ControllingReport, Li);
            }


        }
        public void GvdBind_Consumption(int ObjectId, DateTime StartDate, DateTime EndDate)
        {
            if (StartDate.Date == DateTime.Now.Date)
            {
                List<CurrentDTReportModel> li = new List<CurrentDTReportModel>();

                lblCConsmName.Text = "";
                lblCConsmName.Text = ddlObject.SelectedItem.Text;
                List<ReportModel> li_current = RS.GetConsumptionReport(ObjectId, "Current");
                if (li_current != null)
                {
                    List<ReportModel> li_voltage = RS.GetConsumptionReport(ObjectId, "Voltage");
                    for (int i = 0; i < li_current.Count; i++)
                    {
                        CurrentDTReportModel obj = new CurrentDTReportModel();
                        obj.Name = li_current[i].Name;
                        obj.Current = li_current[i].Value;
                        obj.Voltage = li_voltage[i].Value;
                        obj.Power = (li_current[i].Value * li_voltage[i].Value) / 1000;
                        li.Add(obj);
                    }
                }
                BindingClass.GridViewBind(gvdCurrentConsump, li);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myCurrentConsumptionModal", "$('#myCurrentConsumptionModal').modal(); gvdtempete('#gvdCurrentConsump');loadGraphConsumption();", true);
            }
            else
            {
                lblname.Text = "";
                lblname.Text = ddlObject.SelectedItem.Text;
                List<SwitchesReportConsumptionModel> li = obj.getConsumptionByDT(ObjectId, StartDate, EndDate);
                BindingClass.GridViewBind(Gvdconsumptionreport, li);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal(); gvdtempete('#Gvdconsumptionreport');", true);
            }
        }
        public void GvdBind_Temperature()
        {
            string calender = txtdtrange.Text;
            string[] cal = calender.Split('-');
            string StrStartdate = cal[0]; string StrEnddate = cal[1];
            DateTime Startdate = Convert.ToDateTime(StrStartdate);
            DateTime Enddate = Convert.ToDateTime(StrEnddate);
            lblname3.Text = "";
            lblname3.Text = ddlObject.SelectedItem.Text;
            List<sp_SMSLOGTEMPModel> li = SPS.GetSMSLOGBy(Convert.ToInt32(ddlObject.SelectedValue), 1109, Startdate, Enddate);
            BindingClass.GridViewBind(gvdTemperature, li);
        }

       
        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclient.SelectedItem.Text != "Select Client")
            {
                div2.Visible = false;
                ddlgroup.Visible = true;
                ddlgroup.Items.Clear();
                var _sltdval = ddlclient.SelectedValue;
                int val = Convert.ToInt32(_sltdval);
                ddlGroupbind(val);
                ddlObject.Items.Clear();
                ddlObject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Device", "0"));
            }
            else
            {
                ddlgroup.Items.Clear();
                ddlObject.Items.Clear();
                ddlgroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Branch", "0"));
                ddlObject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Device", "0"));
                div2.Visible = false;
                divDate.Visible = false;
            }
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlgroup.SelectedItem.Text != "Select Branch")
            {
                div2.Visible = false;
                ddlObject.Visible = true;
                ddlObjectbind(Convert.ToInt32(ddlgroup.SelectedValue));
            }
            else
            {
                ddlObject.Items.Clear();
                ddlObject.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Device", "0"));
                div2.Visible = false;
                ddlObject.Visible = false;
                divDate.Visible = false;
            }
        }
        protected void ddlObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlObject.SelectedItem.Text != "Select Device")
            {
                divDate.Visible = true;
                div2.Visible = true;
                if (ddlObject.SelectedValue == "33")
                {
                    euipment.Visible = false;
                    controling.Visible = false;
                }
                else
                {
                    euipment.Visible = true;
                    controling.Visible = true;
                }

            }
            else
            {
                div2.Visible = false;
                divDate.Visible = false;
            }

        } 
        #endregion

        #region Generate PDF
        protected void ExportToPDF()
        {
            try
            {
                if (Gvdconsumptionreport.Rows.Count > 0)
                {

                    //Create the PDF Document

                    Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 0f);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();

                    string imagepath = Server.MapPath("~/Images/logo2.jpg");
                    //String strFullImagePath = Path.Combine(imagepath, "Images", "BankAlfalah.png");

                    iTextSharp.text.Image LogoPng = iTextSharp.text.Image.GetInstance(imagepath);
                    LogoPng.ScaleToFit(150f, 100f);
                    pdfDoc.AddAuthor("H.M.Kamran");
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font times2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font fontHeaderxss = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);
                    iTextSharp.text.Font fontNames = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

                    Paragraph paragraph3 = new Paragraph();
                    paragraph3.Add(new Chunk(" Date: " + DateTime.Now.ToShortDateString() + "                                                                      ", times2));
                    paragraph3.Add(new Chunk("Equipment Consumption Report", times));
                    paragraph3.Add(new Chunk(LogoPng, 50, -10, true));
                    pdfDoc.Add(paragraph3);

                    //Paragraph paragraph4 = new Paragraph("\n\nClient Name: " + ddlclient.SelectedItem.Text, fontNames);
                    //paragraph4.Alignment = Element.ALIGN_LEFT;
                    //paragraph4.IndentationLeft = 55;
                    //pdfDoc.Add(paragraph4);

                    Paragraph paragraph1 = new Paragraph("Object Name: " + ddlObject.SelectedItem.Text + "\n\n", fontNames);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    paragraph1.IndentationLeft = 55;
                    pdfDoc.Add(paragraph1);

                    PdfPTable table = new PdfPTable(Gvdconsumptionreport.Columns.Count);

                    //dynamic headercolumns
                    for (int x = 0; x < Gvdconsumptionreport.Columns.Count; x++)
                    {
                        //widths[x] = (int)Gvdconsumptionreport.Columns[x].ItemStyle.Width.Value;
                        string cellText = Server.HtmlDecode(Gvdconsumptionreport.HeaderRow.Cells[x].Text);
                        PdfPCell cell = new PdfPCell(new Phrase(cellText, fontHeaderxss));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#70a1cc"));

                        table.AddCell(cell);
                    }

                    //dynamic rows
                    for (int i = 0; i < Gvdconsumptionreport.Rows.Count; i++)
                    {
                        if (Gvdconsumptionreport.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 0; j < Gvdconsumptionreport.Columns.Count; j++)
                            {
                                string cellText = Server.HtmlDecode(Gvdconsumptionreport.Rows[i].Cells[j].Text);
                                PdfPCell cell = new PdfPCell(new Phrase(cellText, fontNames));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;" + "filename=Equipment_Consumption_Report.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong!')", true);
            }
            //try
            //{
            //    if (Gvdconsumptionreport.Rows.Count > 0)
            //    {
            //        PdfPTable table = new PdfPTable(Gvdconsumptionreport.Columns.Count);
            //        //iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(Gvdconsumptionreport.Columns.Count);
            //        //int padding = 5;

            //        //Set the column widths
            //        //float[] widths = new float[Gvdconsumptionreport.Columns.Count];
            //        for (int x = 0; x < Gvdconsumptionreport.Columns.Count; x++)
            //        {
            //            //widths[x] = (int)Gvdconsumptionreport.Columns[x].ItemStyle.Width.Value;
            //            string cellText = Server.HtmlDecode(Gvdconsumptionreport.HeaderRow.Cells[x].Text);
            //            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(cellText + ""))
            //            {
            //                BackgroundColor = new iTextSharp.text.BaseColor(
            //               System.Drawing.ColorTranslator.FromHtml("#b3daff")
            //             ),
            //                //Padding = padding
            //            };
            //            table.AddCell(cell);
            //        }
            //        //table.SetTotalWidth(widths);
            //        //table.LockedWidth = true;
            //        //Transfer rows from GridView to table
            //        for (int i = 0; i < Gvdconsumptionreport.Rows.Count; i++)
            //        {
            //            if (Gvdconsumptionreport.Rows[i].RowType == DataControlRowType.DataRow)
            //            {
            //                for (int j = 0; j < Gvdconsumptionreport.Columns.Count; j++)
            //                {
            //                    string cellText = Server.HtmlDecode
            //                                      (Gvdconsumptionreport.Rows[i].Cells[j].Text);
            //                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText))
            //                    {
            //                        //Padding = padding
            //                    };

            //                    //Set Color of Alternating row
            //                    if (i % 2 != 0)
            //                    {
            //                        cell.BackgroundColor = new iTextSharp.text.BaseColor(
            //                       System.Drawing.ColorTranslator.FromHtml("#b3daff"));
            //                    }
            //                    table.AddCell(cell);
            //                }
            //            }
            //        }

            //        //Create the PDF Document
            //        string imagepath = Server.MapPath("~/Images/logo2.jpg");
            //        //String strFullImagePath = Path.Combine(imagepath, "Images", "BankAlfalah.png");
            //        Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 0f);
            //        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            //        iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            //        Paragraph paragraph = new Paragraph("\n\n\n Equipment Consumption Report \n\n\n", times);
            //        Paragraph para = new Paragraph("\n\n\n Date: " + DateTime.Now.ToShortDateString() + "\n\n\n");
            //        para.Alignment = Element.ALIGN_CENTER;
            //        paragraph.Alignment = Element.ALIGN_CENTER;
            //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        //Chunk c1 = new Chunk("Equipment ON / OFF Report \n");

            //        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
            //        gif.ScaleToFit(200f, 150f);
            //        gif.SpacingBefore = 1f;
            //        gif.SpacingAfter = 5f;
            //        gif.Alignment = Element.ALIGN_CENTER;
            //        pdfDoc.Add(gif);
            //        pdfDoc.Add(paragraph);
            //        pdfDoc.Add(para);
            //        pdfDoc.Add(table);
            //        pdfDoc.Close();
            //        Response.ContentType = "application/pdf";
            //        Response.AddHeader("content-disposition", "attachment;" +
            //                                       "filename=Equipment Consumption Report.pdf");
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.Write(pdfDoc);
            //        Response.End();
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }
        protected void ExportToPDF2()
        {

            try
            {
                if (gv_ControllingReport.Rows.Count > 0)
                {

                    //Create the PDF Document
                    string imagepath = Server.MapPath("~/Images/logo2.jpg");
                    //String strFullImagePath = Path.Combine(imagepath, "Images", "BankAlfalah.png");

                    Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 0f);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();

                    iTextSharp.text.Image LogoPng = iTextSharp.text.Image.GetInstance(imagepath);
                    LogoPng.ScaleToFit(150f, 100f);
                    pdfDoc.AddAuthor("H.M.Kamran");
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font times2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font fontHeaderxss = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);
                    iTextSharp.text.Font fontNames = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);


                    Paragraph paragraph3 = new Paragraph();
                    paragraph3.Add(new Chunk(" Date: " + DateTime.Now.ToShortDateString() + "                                                                         ", times2));
                    paragraph3.Add(new Chunk("Equipment Controlling Report", times));
                    paragraph3.Add(new Chunk(LogoPng, 50, -10, true));
                    pdfDoc.Add(paragraph3);

                    //Paragraph paragraph4 = new Paragraph("\n\nClient Name: " + ddlclient.SelectedItem.Text, fontNames);
                    //paragraph4.Alignment = Element.ALIGN_LEFT;
                    //paragraph4.IndentationLeft = 55;
                    //pdfDoc.Add(paragraph4);

                    Paragraph paragraph1 = new Paragraph("Object Name: " + ddlObject.SelectedItem.Text + "\n\n", fontNames);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    paragraph1.IndentationLeft = 55;
                    pdfDoc.Add(paragraph1);


                    PdfPTable table = new PdfPTable(gv_ControllingReport.Columns.Count);

                    for (int x = 0; x < gv_ControllingReport.Columns.Count; x++)
                    {
                        //widths[x] = (int)Gvdconsumptionreport.Columns[x].ItemStyle.Width.Value;
                        string cellText = Server.HtmlDecode(gv_ControllingReport.HeaderRow.Cells[x].Text);
                        PdfPCell cell = new PdfPCell(new Phrase(cellText, fontHeaderxss));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#70a1cc"));
                        table.AddCell(cell);
                    }

                    for (int i = 0; i < gv_ControllingReport.Rows.Count; i++)
                    {
                        if (gv_ControllingReport.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 0; j < gv_ControllingReport.Columns.Count; j++)
                            {
                                string cellText = Server.HtmlDecode(gv_ControllingReport.Rows[i].Cells[j].Text);
                                PdfPCell cell = new PdfPCell(new Phrase(cellText, fontNames));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;" + "filename=Equipment_Controlling_Report.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong!')", true);
            }


        }

        protected void ExportToPDF3()
        {

            try
            {
                if (gvdTemperature.Rows.Count > 0)
                {

                    //Create the PDF Document
                    string imagepath = Server.MapPath("~/Images/logo2.jpg");
                    //String strFullImagePath = Path.Combine(imagepath, "Images", "BankAlfalah.png");

                    Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 0f);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();

                    iTextSharp.text.Image LogoPng = iTextSharp.text.Image.GetInstance(imagepath);
                    LogoPng.ScaleToFit(150f, 100f);
                    pdfDoc.AddAuthor("H.M.Kamran");
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 12, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font times2 = new iTextSharp.text.Font(bfTimes, 8, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
                    iTextSharp.text.Font fontHeaderxss = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.WHITE);
                    iTextSharp.text.Font fontNames = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);


                    Paragraph paragraph3 = new Paragraph();
                    paragraph3.Add(new Chunk(" Date: " + DateTime.Now.ToShortDateString() + "                                                                         ", times2));
                    paragraph3.Add(new Chunk("Temperature Sensor Report", times));
                    paragraph3.Add(new Chunk(LogoPng, 50, -10, true));
                    pdfDoc.Add(paragraph3);

                    //Paragraph paragraph4 = new Paragraph("\n\nClient Name: " + ddlclient.SelectedItem.Text, fontNames);
                    //paragraph4.Alignment = Element.ALIGN_LEFT;
                    //paragraph4.IndentationLeft = 55;
                    //pdfDoc.Add(paragraph4);

                    Paragraph paragraph1 = new Paragraph("Device Name: " + ddlObject.SelectedItem.Text + "\n\n", fontNames);
                    paragraph1.Alignment = Element.ALIGN_LEFT;
                    paragraph1.IndentationLeft = 55;
                    pdfDoc.Add(paragraph1);


                    PdfPTable table = new PdfPTable(gvdTemperature.Columns.Count);

                    for (int x = 0; x < gvdTemperature.Columns.Count; x++)
                    {
                        //widths[x] = (int)Gvdconsumptionreport.Columns[x].ItemStyle.Width.Value;
                        string cellText = Server.HtmlDecode(gvdTemperature.HeaderRow.Cells[x].Text);
                        PdfPCell cell = new PdfPCell(new Phrase(cellText, fontHeaderxss));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.BackgroundColor = new BaseColor(ColorTranslator.FromHtml("#70a1cc"));
                        table.AddCell(cell);
                    }

                    for (int i = 0; i < gvdTemperature.Rows.Count; i++)
                    {
                        if (gvdTemperature.Rows[i].RowType == DataControlRowType.DataRow)
                        {
                            for (int j = 0; j < gvdTemperature.Columns.Count; j++)
                            {
                                string cellText = Server.HtmlDecode(gvdTemperature.Rows[i].Cells[j].Text);
                                PdfPCell cell = new PdfPCell(new Phrase(cellText, fontNames));
                                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                                table.AddCell(cell);
                            }
                        }
                    }

                    pdfDoc.Add(table);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;" + "filename=TemperatureSensorReport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('something went wrong!')", true);
            }

            //try
            //{
            //    if (gv_ControllingReport.Rows.Count > 0)
            //    {
            //        PdfPTable table = new PdfPTable(gv_ControllingReport.Columns.Count);
            //        //iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(Gvdconsumptionreport.Columns.Count);
            //        //int padding = 5;

            //        //Set the column widths
            //        //float[] widths = new float[Gvdconsumptionreport.Columns.Count];
            //        for (int x = 0; x < gv_ControllingReport.Columns.Count; x++)
            //        {
            //            //widths[x] = (int)Gvdconsumptionreport.Columns[x].ItemStyle.Width.Value;
            //            string cellText = Server.HtmlDecode(gv_ControllingReport.HeaderRow.Cells[x].Text);
            //            iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(cellText + ""))
            //            {
            //                BackgroundColor = new iTextSharp.text.BaseColor(
            //               System.Drawing.ColorTranslator.FromHtml("#b3daff")
            //             ),
            //                //Padding = padding
            //            };
            //            table.AddCell(cell);
            //        }
            //        //table.SetTotalWidth(widths);
            //        //table.LockedWidth = true;
            //        //Transfer rows from GridView to table
            //        for (int i = 0; i < gv_ControllingReport.Rows.Count; i++)
            //        {
            //            if (gv_ControllingReport.Rows[i].RowType == DataControlRowType.DataRow)
            //            {
            //                for (int j = 0; j < gv_ControllingReport.Columns.Count; j++)
            //                {
            //                    string cellText = Server.HtmlDecode
            //                                      (gv_ControllingReport.Rows[i].Cells[j].Text);
            //                    iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new iTextSharp.text.Phrase(cellText))
            //                    {
            //                        //Padding = padding
            //                    };

            //                    //Set Color of Alternating row
            //                    if (i % 2 != 0)
            //                    {
            //                        cell.BackgroundColor = new iTextSharp.text.BaseColor(
            //                       System.Drawing.ColorTranslator.FromHtml("#b3daff"));
            //                    }
            //                    table.AddCell(cell);
            //                }
            //            }
            //        }

            //        //Create the PDF Document
            //        string imagepath = Server.MapPath("~/Images/logo2.jpg");
            //        //String strFullImagePath = Path.Combine(imagepath, "Images", "BankAlfalah.png");
            //        Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 0f);
            //        BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            //        iTextSharp.text.Font times = new iTextSharp.text.Font(bfTimes, 16, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            //        Paragraph paragraph = new Paragraph("\n\n\n Equipment Controlling Report \n\n\n", times);
            //        Paragraph para = new Paragraph("\n\n\n Date: " + DateTime.Now.ToShortDateString() + "\n\n\n");
            //        para.Alignment = Element.ALIGN_CENTER;
            //        paragraph.Alignment = Element.ALIGN_CENTER;
            //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            //        pdfDoc.Open();
            //        //Chunk c1 = new Chunk("Equipment ON / OFF Report \n");

            //        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
            //        gif.ScaleToFit(200f, 150f);
            //        gif.SpacingBefore = 1f;
            //        gif.SpacingAfter = 5f;
            //        gif.Alignment = Element.ALIGN_CENTER;
            //        pdfDoc.Add(gif);
            //        pdfDoc.Add(paragraph);
            //        pdfDoc.Add(para);
            //        pdfDoc.Add(table);
            //        pdfDoc.Close();
            //        Response.ContentType = "application/pdf";
            //        Response.AddHeader("content-disposition", "attachment;" +
            //                                       "filename=Equipment Controlling Report.pdf");
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        Response.Write(pdfDoc);
            //        Response.End();
            //    }
            //}
            //catch (Exception)
            //{
            //}
        } 
        
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
            iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
            image.ScalePercent(scale);
            PdfPCell cell = new PdfPCell(image);
            cell.BorderColor = new iTextSharp.text.BaseColor(
                           System.Drawing.ColorTranslator.FromHtml("#3c8dbc"));
            cell.VerticalAlignment = cell.VerticalAlignment;
            cell.HorizontalAlignment = align;
            cell.PaddingBottom = 0f;
            cell.PaddingTop = 0f;
            return cell;
        }
        #endregion
        //public override void VerifyRenderingInServerForm(Control control)
        //{
        //}
        #region Button Clicks
        protected void btnRptEqpConsp_Click(object sender, EventArgs e)
        {
            ExportToPDF();
        }
        protected void btnControlingRpt_Click(object sender, EventArgs e)
        {
            ExportToPDF2();
        }

        protected void btnTemprature_Click(object sender, EventArgs e)
        {
            ExportToPDF3();
        } 
        #endregion


    }

}

























            