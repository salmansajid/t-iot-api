using TIOT_WEB.Models;
using TIOT_WEB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIOT_WEB.Common;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using TIOT_WEB.BAL;

namespace TIOT_WEB
{
    public partial class Category : System.Web.UI.Page
    {
        #region ClassIntances and Varible Declerations
        public string Alert = "";
        CategoryBLL obj = new CategoryBLL();
        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["admin"] != null)
                    { BindgridView(); }
                    else
                    { Response.Redirect("Login.aspx"); }
                }
                catch(Exception)
                { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
            }
        }
        #endregion

        #region  LinkButton Clicks
        protected void linkbtnEdit_Command(object sender, CommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "UpdateID")
                {
                    allowStaticMethods("staticMethod('Enable');applyDatatable('.gvdcategoryclass');");
                    int cmdArg = Convert.ToInt32(e.CommandArgument);
                    CategoryModel li = obj.getCategoryByCategoryID(cmdArg);
                    txtName.Text = li.Name;
                    txtMin.Text = li.Name;
                    txtMax.Text = li.Name;
                    chkEnable.Checked = Convert.ToBoolean(li.EnableORDisable);
                    Session["CategoryId"] = cmdArg.ToString();
                    btnAddCategory.Text = "Update";
                }
            }
            catch(Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void linkbtnRemoveID_Command(object sender, CommandEventArgs e)
        {
            try
            {
            if (e.CommandName == "RemoveID")
            {
                string user = Convert.ToString(e.CommandArgument);
                Session["CategoryId"] = Convert.ToInt32(user);
                allowStaticMethods("openDeleteModal(); staticMethod('Disable');applyDatatable('.gvdcategoryclass');");
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
                    int cmdArg = Convert.ToInt32(Session["CategoryId"]);
                    bool status = obj.disableCategory(cmdArg);
                    if (status == true)
                    { Alert = AlertsClass.SuccessRemove; }
                    else
                    { Alert = AlertsClass.ErrorWentWrong; }
                    Session.Remove("CategoryId");
                    BindgridView();
                    allowStaticMethods("staticMethod('Disable');   applyDatatable('.gvdcategoryclass');ALerts('" + Alert + "');");

                }
            }
            catch (Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        } 
        #endregion

        #region Button Clicks
        protected void btnAddCategory_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtName.Text != "" && txtMin.Text != "" && txtMin.Text != "" && imgCategory.FileName != null)
                {
                    CategoryModel model = new CategoryModel();
                    model.CategoryID = 0;
                    model.Name  = txtName.Text;
                    model.ImgPath = "Images/categoryIcons/" + imgCategory.FileName;
                    model.Min  = txtMin.Text;
                    model.Max  = txtMax.Text;
                    model.EnableORDisable = chkEnable.Checked ? true : false;

                    bool exist = obj.categoryExist(model.Name);
                    if (exist == false)
                    {
                        if (btnAddCategory.Text == "Save")
                        {
                            bool status = obj.postCategory(model);
                            if (status == true)
                            {
                                Alert = AlertsClass.SuccessAdd;
                                string extension = Path.GetExtension(imgCategory.FileName);
                                if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg")
                                {
                                    Stream strm = imgCategory.PostedFile.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 24;
                                        int newHeight = 24;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        string targetPath = Server.MapPath(@"~\Images\categoryIcons\") + imgCategory.FileName;
                                        thumbImg.Save(targetPath, image.RawFormat);
                                    }
                                }
                            }
                            else
                            { Alert = AlertsClass.ErrorWentWrong; }
                        }
                        if (btnAddCategory.Text == "Update")
                        {

                            model.CategoryID = Convert.ToInt32(Session["CategoryId"]);
                            bool status = obj.postCategory(model);
                            if (status == true)
                            {
                                Alert = AlertsClass.SuccessUpdate;
                                string extension = Path.GetExtension(imgCategory.FileName);
                                if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg")
                                {
                                    Stream strm = imgCategory.PostedFile.InputStream;
                                    using (var image = System.Drawing.Image.FromStream(strm))
                                    {
                                        int newWidth = 24;
                                        int newHeight = 24;
                                        var thumbImg = new Bitmap(newWidth, newHeight);
                                        var thumbGraph = Graphics.FromImage(thumbImg);
                                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                                        thumbGraph.DrawImage(image, imgRectangle);
                                        string targetPath = Server.MapPath(@"~\Images\categoryIcons\") + imgCategory.FileName;
                                        thumbImg.Save(targetPath, image.RawFormat);
                                    }
                                }
                            }
                            else
                            { Alert = AlertsClass.ErrorWentWrong; }
                        }
                    }
                    else
                    { Alert = AlertsClass.ErrorExist("Category "); }
                    BindgridView();
                    clearControls();
                }
                else
                { Alert = AlertsClass.ErrorRequired; }
                allowStaticMethods("staticMethod('Disable');   applyDatatable('.gvdcategoryclass');ALerts('" + Alert + "');");
            }
            catch(Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearControls();
            allowStaticMethods("staticMethod('Disable');   applyDatatable('.gvdcategoryclass');");
        }
        #endregion

        #region Binding Controls
        public void BindgridView()
        {
            try
            {
                List<CategoryModel> list = obj.getCategory();
                BindingClass.GridViewBind(GVDCategory, list);
            }
            catch(Exception)
            { BindingClass.ExceptionAlertScriptManager(this.Page, this.GetType()); }

        }
        #endregion

        #region Text and Dropdown Clear Method
        public void clearControls()
        {
            txtName.Text = string.Empty;
            txtMin.Text = string.Empty;
            txtMax.Text = string.Empty;
            Session.Remove("CategoryId");
            chkEnable.Checked = false;
            btnAddCategory.Text = "Save";
        }
        public void allowStaticMethods(string jsfunctions)
        {
            BindingClass.CallScriptManager(this.Page, this.GetType(), jsfunctions);
        }
        #endregion
    }
}