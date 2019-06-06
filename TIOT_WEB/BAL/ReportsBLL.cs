using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class ReportsBLL
    {
        ReportsDLL obj = new ReportsDLL();
        #region Consumption Report
        public List<SwitchesReportDayModel> getConsumptionToday(int objectID, string sensor)
        {
            return obj.getConsumptionToday(objectID, sensor);
        }
        public List<SwitchesReportConsumptionModel> getConsumptionByDT(int objectID, DateTime StartDate, DateTime EndDate)
        {
            return obj.getConsumptionByDT(objectID, StartDate, EndDate);
        }
        #endregion
        #region Controlling Report
        public List<SwitchesReportControllingModel> getControllingByDT(int objectID, DateTime StartDate, DateTime EndDate)
        {
            return obj.getControllingByDT(objectID, StartDate, EndDate);
        }
        public List<SwitchesReportControllingModel> getControllingToday(int objectID)
        {
            return obj.getControllingToday(objectID);
        }
        #endregion
        #region Sensor Variation Report
        public List<SensorVariationModel> getSensorVariationReport(int objectSensorID,DateTime StartDate, DateTime EndDate)
        {
            return obj.getSensorVariationReport(objectSensorID, StartDate, EndDate);
        }
        #endregion
         #region DigitalInput Report
        public List<DigitalInputModel> getDigitalInputReport(int objectSensorID, DateTime StartDate, DateTime EndDate)
        {
            return obj.getDigitalInputReport(objectSensorID, StartDate, EndDate);
        }
         #endregion

        #region Individual Sensor Report
        public List<IndividualSensorModel> getIndividualSensorReport(int objectSensorID, DateTime StartDate, DateTime EndDate, double min, double max)
        {
            return obj.getIndividualSensorReport(objectSensorID, StartDate, EndDate, min, max);
        }
         #endregion

        #region DIN & Serial Report
        public List<IndividualSensorModel> getEventLogReport(int objectSensorID, DateTime StartDate, DateTime EndDate)
        {
            return obj.getEventLogReport(objectSensorID, StartDate, EndDate);
        }
        #endregion
    }
}