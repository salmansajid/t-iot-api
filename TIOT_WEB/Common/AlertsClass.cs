using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace TIOT_WEB.Common
{
    public static class AlertsClass
    {
        
        //public static string position = "{positionClass:'toast-top-right'})";
        //public static string Success = "toastr.success('";
        //public static string Warning = "toastr.warning('";
        //public static string Error = "toastr.error('";

        //Success Alerts
        public static string SuccessAdd = "Request had been Successfully Added. -success";
        public static string SuccessUpdate = "Request had been Successfully updated. -success ";
        public static string SuccessRemove = "Remove Request had been Approved. -success";


        //Warning Alerts
        //public static string WarningAdd = "(*) Feilds must be Required. -warning";
        


        //Error Alerts
        public static string ErrorWentWrong = "404, Request not found!. -error";
        public static string ErrorPasswordMatch = "Password not matched. -error";
        public static string ErrorRequired = "(*) Feilds must be Required. -error";
        public static string ErrorExist(string Temp)
        {
            string Erroralready = Temp + " already exists, please try with another " + Temp + ". -error";
            return Erroralready;
        }
        //public string ErrorUpdate = Error + "Request had been Successfully updated., ";
        


        public static string paramsError(string Temp)
        {
            string ErrorRemove = "Can't Remove " + Temp + ", it is Configured with management. -error";
            return ErrorRemove;
        }
        

    }
}