using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TIOT_WEB.Common
{
    public static class BindingClass
    {
        public static void BindDropDown(DropDownList objdropdown, object li, string Display, string Value, string pleaseSelect)
        {
            try
            {
                objdropdown.Items.Clear();
                objdropdown.DataSource = li;
                objdropdown.DataTextField = Display;
                if (Value != "")
                {
                    objdropdown.DataValueField = Value;
                }
                objdropdown.DataBind();
                objdropdown.Items.Insert(0, new ListItem(pleaseSelect, "0"));
            }
            catch (Exception) { }
        }

        public static void ClearDropDown(DropDownList objdropdown, string pleaseSelect)
        {
            try
            {
                objdropdown.Items.Clear();
                objdropdown.Items.Insert(0, new ListItem(pleaseSelect, "0"));
            }
            catch (Exception) { }
        }

        public static void StaticListBindDropDown(DropDownList objdropdown, int Listlength, List<string> li)
        {
            try
            {
                objdropdown.Items.Clear();
                ListItem[] items = new ListItem[Listlength];
                for (int i = 0; i < Listlength; i++)
                {
                    items[i] = new ListItem(li[i], i.ToString());
                }
                objdropdown.Items.AddRange(items);
                objdropdown.DataBind();
            }
            catch (Exception) { }
        }

        public static void GridViewBind(GridView objgvd, object li)
        {
            try
            {
                objgvd.DataSource = li;
                objgvd.DataBind();
            }
            catch (Exception) { }
        }
        public static void ClearGridView(GridView objgvd)
        {
            try
            {
                DataTable dt = new DataTable();
                objgvd.DataSource = dt;
                objgvd.DataBind();
            }
            catch (Exception) { }
        }

        public static void RepeaterViewBind(Repeater rptview, object li)
        {
            try
            {
                rptview.DataSource = li;
                rptview.DataBind();
            }
            catch (Exception) { }
        }

        public static void ClearRepeaterView(Repeater rptview)
        {
            try
            {
                DataTable dt = new DataTable();
                rptview.DataSource = dt;
                rptview.DataBind();
            }
            catch (Exception) { }
        }


        public static void AlertScriptManager(Page pageName, Type type, string _params)
        {
            ScriptManager.RegisterStartupScript(pageName, type, "script", "ALerts('" + _params + "')", true);
        }

        public static void ExceptionAlertScriptManager(Page pageName, Type type)
        {
            ScriptManager.RegisterStartupScript(pageName, type, "script", "ALerts('" + AlertsClass.ErrorWentWrong + "')", true);
        }

        public static void CallScriptManager(Page pageName, Type type, string JSfunctionName)
        {
            ScriptManager.RegisterStartupScript(pageName, type, "script", JSfunctionName, true);
        }

        

    }
}