using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class ObjectMaintenanceModel
    {
            public int MainId { get; set; }
            public int ObjectID { get; set; }
            public string IssueComments { get; set; }
            public DateTime IssueDateTime { get; set; }
            public string IssueAuthor { get; set; }
            public string ResolvedComments { get; set; }
            public DateTime ResolvedDateTime { get; set; }
            public string ResolvedPerson { get; set; }
            public bool isActive { get; set; }
            public string cssClass { get; set; }
            public string linkbtnText { get; set; }
    }
}