using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;

namespace TIOT_WEB.Service
{
    public class TAVLService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<TAVLModel> GetTAVLObjectList(int ClientId, int GroupId, string IP)
        {
            var url = "api/TavlService?ClientId=" + ClientId + "&GroupId=" + GroupId + "&IPAddress=" + IP + "&isReverseGeocoding=DISABLE";
            string result = SC.Getcaller(url);
            if (result.Contains("[]"))
            {
                return null;
            }
            else
            {

                List<TAVLModel> Response = JsonConvert.DeserializeObject<List<TAVLModel>>(result);
                for (int i = 0; i < Response.Count; i++)
                {
                    DateTime strTime = Convert.ToDateTime(Response[i].gpsTime);
                    string st = strTime.ToString();
                    string[] arr = st.Split(' ');
                    Response[i].date = arr[0];
                    Response[i].time = arr[1] + arr[2];
                    if (Convert.ToInt32(Response[i].speed) > 3)
                    {

                        Response[i]._speed = "4";
                    }
                    else if (Convert.ToInt32(Response[i].speed) > 256)
                    {
                        Response[i]._speed = "0";
                    }
                    else
                    {
                        Response[i]._speed = "0";
                    }
                    if (strTime > DateTime.Now.AddHours(-24))
                    {
                        Response[i]._speed = "5";
                    }
                }
                return Response;
            }

        }
    }
}