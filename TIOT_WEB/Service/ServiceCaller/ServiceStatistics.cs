using TIOT_WEB.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TIOT_WEB.Service.ServiceCaller
{
    public class ServiceStatistics
    {
        public Uri BaseAddress = new System.Uri(System.Configuration.ConfigurationManager.AppSettings["BaseUrl"]);
        //Get Request Caller method
        public string Getcaller(string url)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = BaseAddress;
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/JSON"));
                if (httpclient.GetAsync(url).Result != null)
                {
                    using (HttpResponseMessage response = httpclient.GetAsync(url).Result)
                    {

                        if (Convert.ToInt32(response.StatusCode) == 500)
                        {
                            return null;
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                            var result = response.Content.ReadAsStringAsync().Result;
                            if (result.Contains("[]"))
                            {
                                return null;
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        //Post Request Caller method
        public string PostCaller(string url, object _params)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = BaseAddress;
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/JSON"));
                var content = new StringContent(JsonConvert.SerializeObject(_params).ToString(), Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = httpclient.PostAsync(url, content).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    if (result != "0")
                    {
                        return result;
                    }
                    else
                    {
                        result = "0";
                        return result;
                    }

                }
            }
        }

        //Put Request Caller method
        public string PutCaller(string url, object _object)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = BaseAddress;
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/JSON"));
                var content = new StringContent(JsonConvert.SerializeObject(_object).ToString(), Encoding.UTF8, "application/json");
                using (HttpResponseMessage response = httpclient.PutAsync(url, content).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }

        //Delete Request Caller method
        public string DeleteCaller(string url)
        {
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = BaseAddress;
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/JSON"));
                using (HttpResponseMessage response = httpclient.DeleteAsync(url).Result)
                {
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }


        public string GetcallerIPChange(string url, string IP)
        {
            Uri _BaseAddress = new System.Uri("http://" + IP + "/SmartSchoolAPI/");
            //  Uri _BaseAddress = new System.Uri("http://124.29.205.150/SmartSchoolAPI/api/DailyAttendance/2/");
            using (HttpClient httpclient = new HttpClient())
            {
                httpclient.BaseAddress = _BaseAddress;
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/JSON"));
                if (httpclient.GetAsync(url).Result != null)
                {
                    using (HttpResponseMessage response = httpclient.GetAsync(url).Result)
                    {

                        if (Convert.ToInt32(response.StatusCode) == 500)
                        {
                            return null;
                        }
                        else
                        {
                            response.EnsureSuccessStatusCode();
                            var result = response.Content.ReadAsStringAsync().Result;
                            if (result.Contains("[]"))
                            {
                                return null;
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
                else
                {
                    return null;
                }
            }
        }


        
    }
}