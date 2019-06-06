using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TIOT_WEB.DAL;
using TIOT_WEB.Models;

namespace TIOT_WEB.BAL
{
    public class CategoryBLL
    {
        CategoryDLL obj = new CategoryDLL();

        public List<CategoryModel> getCategory()
        { return obj.getCategory(); }

        public CategoryModel getCategoryByCategoryID(int categoryID)
        { return obj.getCategoryByCategoryID(categoryID); }

        public bool postCategory(CategoryModel model)
        { return obj.postCategory(model); }

        public bool disableCategory(int categoryID)
        { return obj.disableCategory(categoryID); }

        public bool categoryExist(string name)
        { return obj.categoryExist(name); }
    }
}