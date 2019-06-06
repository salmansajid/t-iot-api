using TIOT_WEB.Models;
using TIOT_WEB.Service.ServiceCaller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TIOT_WEB.Service
{
    public class CategoryService
    {
        ServiceStatistics SC = new ServiceStatistics();

        public List<CategoryModel> GetCategory()
        {
            var url = "api/Category";
            string result = SC.Getcaller(url);
            if (result.Contains("[]"))
            {
                return null;
            }
            else
            {
                List<CategoryModel> _category = JsonConvert.DeserializeObject<List<CategoryModel>>(result);
                return _category;
            }
            
        }
        public CategoryModel GetCategorytById(int CategoryId)
        {
            var url = "api/Category/" + CategoryId;
            string result = SC.Getcaller(url);
            if (result != null)
            {
                CategoryModel _category = JsonConvert.DeserializeObject<CategoryModel>(result);
                return _category;
            }
            else
            {
                return null;
            }



        }
        public int PostCategory(string Name, bool EnableORDisable, bool Deleted)
        {
            var _object = new
            {
                Name = Name,
                EnableORDisable = EnableORDisable,
                Deleted = Deleted
            };
            var url = "api/Category";
            string result = SC.PostCaller(url, _object);
            int CategoryId = Convert.ToInt32(result);
            return CategoryId;
        }

        public bool PutCategory(int categoryId, string Name, bool EnableORDisable, bool Deleted)
        {
            var _object = new
            {
                Name = Name,
                EnableORDisable = Convert.ToBoolean(EnableORDisable),
                Deleted = Deleted
            };
            var url = "api/Category/" + categoryId;
            string result = SC.PutCaller(url, _object);
            bool Status = Convert.ToBoolean(result);
            return Status;
        }

        //public bool DeleteCategory(int CategoryId)
        //{
        //    var url = "api//" + CategoryId;
        //    string status = SC.DeleteCaller(url);
        //    bool result = Convert.ToBoolean(status);
        //    return result;
        //}
    }
}