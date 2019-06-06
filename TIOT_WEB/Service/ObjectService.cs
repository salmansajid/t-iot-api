using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class ObjectService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<ObjectModel> GetObjects()
        {
            var url = "api/Object";
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectModel> _objects = JsonConvert.DeserializeObject<List<ObjectModel>>(result);
                return _objects;
            }
            else
            {
                return null;
            }
            
        }

        public List<Objectdetails> GetObjectdetails(int objectId)
        {
            var url = "api/Object?ObjectId=" + objectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<Objectdetails> _objects = JsonConvert.DeserializeObject<List<Objectdetails>>(result);
                return _objects;
            }
            else
            {
                return null;
            }
        }

        public List<ObjectdetailsNew> GetObjectdetailsNew(int objectId)
        {
            var url = "api/Object?ObjId=" + objectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectdetailsNew> _objects = JsonConvert.DeserializeObject<List<ObjectdetailsNew>>(result);
                return _objects;
            }
            else
            {
                return null;
            }
        }

        public ObjectModel GetObjectById(int ObjectId)
        {
            var url = "api/Object/" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                ObjectModel _objects = JsonConvert.DeserializeObject<ObjectModel>(result);
                return _objects;
            }

            else
            {
                return null;
            }

        }

        public List<ObjectModel> GetObjectsByClientId(int ClientId)
        {
            var url = "api/Object?Clientid=" + ClientId;
            string result = SC.Getcaller(url);

            if (result != null)
            {
                List<ObjectModel> _objects = JsonConvert.DeserializeObject<List<ObjectModel>>(result);
                return _objects;
            }
            else
            {
                return null;
            }

        }

        public List<ObjectModel> GetObjectsByGroupId(int GroupId)
        {
            var url = "api/Object?GroupId=" + GroupId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectModel> _objects = JsonConvert.DeserializeObject<List<ObjectModel>>(result);
                return _objects;
            }

            else
            {
                return null;
            }

        }

        public List<ObjectModel> GetObjectsByClientId(int GroupId, int LoginId)
        {
            var url = "api/Object?GroupId=" + GroupId + " &LoginId=" + LoginId;
            string result = SC.Getcaller(url);
            List<ObjectModel> _objects = JsonConvert.DeserializeObject<List<ObjectModel>>(result);
            return _objects;
        }

        public List<ObjectSensorModel> GetSensorByObjectId(int ObjectId)
        {
            var url = "api/ObjectSensors?ObjtId=" + ObjectId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                List<ObjectSensorModel> _objects = JsonConvert.DeserializeObject<List<ObjectSensorModel>>(result);
                return _objects;
            }

            else
            {
                return null;
            }
        }

        public int PostObject(string name, string address, string lat, string lon, string imei, string simnumber, string firmwareversion, string hardwareversion, bool enabdisab, string clientid, string contact1, bool chkRelaySt)
        {
            var _object = new
            {
                Name = name,
                Address = address,
                LAT = Convert.ToDecimal(lat),
                LONG = Convert.ToDecimal(lon),
                IMEI = Convert.ToInt64(imei),
                SimNumber = Convert.ToInt64(simnumber),
                FirmWareVersion = firmwareversion,
                HardwareVersion = hardwareversion,
                EnableOrDisable = enabdisab,
                ClientID = Convert.ToInt32(clientid),
           
                RelayStatus = chkRelaySt,
                
                
            };
            var url = "api/Object";
            string result = SC.PostCaller(url, _object);
            int objectID = Convert.ToInt32(result);
            return objectID;
        }

        public bool PutObject(int ObjectId, string name, string address, string lat, string lon, string imei, string simnumber, string firmwareversion, string hardwareversion, bool enabdisab, string clientid, string contact1,  bool chkRelaySt)
        {
            var _object = new
            {
                Name = name,
                Address = address,
                LAT = Convert.ToDecimal(lat),
                LONG = Convert.ToDecimal(lon),
                IMEI = Convert.ToInt64(imei),
                SimNumber = Convert.ToInt64(simnumber),
                FirmWareVersion = firmwareversion,
                HardwareVersion = hardwareversion,
                EnableOrDisable = enabdisab,
                ClientID = Convert.ToInt32(clientid),
            
                RelayStatus = chkRelaySt,
                
            };
            var url = "api/Object/" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool DeleteObject(int ObjectId)
        {
            var url = "api/Object/" + ObjectId;
            string status = SC.DeleteCaller(url);
            bool result = Convert.ToBoolean(status);
            return result;
        }


        public List<TavlIntegrationModel> GetTavlStatusByObjectId(int ObjectId)
        {
            var url = "api/Object?TavlId=" + ObjectId;string result = SC.Getcaller(url);
            if (result != null)
            { List<TavlIntegrationModel> _objects = JsonConvert.DeserializeObject<List<TavlIntegrationModel>>(result); return _objects; }
            else
                {return null;}
        }

        public bool PutTavlObject(int ObjectId, int TavlClient, int TavlGroup, string TavlIP, bool TavlStatus)
        {
            var _object = new { TavlClient = TavlClient, TavlGroup = TavlGroup, TavlIP = TavlIP, TavlStatus = TavlStatus };
            var url = "api/Object?Objtavlid="+ ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool PutTavlObjectStatus(int ObjectId)
        {
            var _object = new { };
            var url = "api/Object?ObjectIdTavl=" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public List<AttendanceIntegrationModel> GetAttendanceStatusByObjectId(int ObjectId)
        {
            var url = "api/Object?ObjectIdAttendance=" + ObjectId; string result = SC.Getcaller(url);
            if (result != null)
            { List<AttendanceIntegrationModel> _objects = JsonConvert.DeserializeObject<List<AttendanceIntegrationModel>>(result); return _objects; }
            else
            { return null; }
        }

        public bool PutAttendanceObject(int ObjectId, int AttendanceClientId, string AttendanceIP, bool AttendanceStatus)
        {
            var _object = new { AttendanceClient = AttendanceClientId, AttendanceIP = AttendanceIP, AttendanceStatus = AttendanceStatus };
            var url = "api/Object?Objattendanceid=" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool PutAttendanceObjectStatus(int ObjectId)
        {
            var _object = new { };
            var url = "api/Object?ObjectIdAttendance=" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public List<SurveillanceIntegrationModel> GetSurveillanceByObjectId(int ObjectId)
        {
            var url = "api/Object?SurveillanceId=" + ObjectId; string result = SC.Getcaller(url);
            if (result != null)
            { List<SurveillanceIntegrationModel> _objects = JsonConvert.DeserializeObject<List<SurveillanceIntegrationModel>>(result); return _objects; }
            else
            { return null; }
        }

        public bool PutSurveillanceObject(int ObjectId, string SurveillanceIP, bool SurveillanceStatus)
        {
            var _object = new { SurveillanceIP = SurveillanceIP, SurveillanceStatus = SurveillanceStatus };
            var url = "api/Object?ObjSurid=" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        public bool PutSurveillanceObjectStatus(int ObjectId)
        {
            var _object = new { };
            var url = "api/Object?ObjectIdSurveillance=" + ObjectId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

    }
}
