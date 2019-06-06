using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string ImgPath { get; set; }
        public bool EnableORDisable { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
    }
}