using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class CategoryDLL
    {
        public List<CategoryModel> getCategory()
        {
            List<CategoryModel> list = new List<CategoryModel>();
            using (DataTable table = DBHelper.ExecuteSelectCommand("uspGET_Category", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        CategoryModel model = new CategoryModel();
                        model.CategoryID = Convert.ToInt32(row["CategoryID"]);
                        model.Name = row["Name"].ToString();
                        model.ImgPath = row["ImgPath"].ToString();
                        model.EnableORDisable = Convert.ToBoolean(row["EnableORDisable"]);
                        model.Min = row["Min"].ToString();
                        model.Max = row["Max"].ToString();
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public CategoryModel getCategoryByCategoryID(int categoryID)
        {

            CategoryModel model = new CategoryModel();
            string query = "select * from  [Category] where CategoryID = @CategoryID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@CategoryID", categoryID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.CategoryID = Convert.ToInt32(row["CategoryID"]);
                    model.Name = row["Name"].ToString();
                    model.ImgPath = row["ImgPath"].ToString();
                    model.EnableORDisable = Convert.ToBoolean(row["EnableORDisable"]);
                    model.Min = row["Min"].ToString();
                    model.Max = row["Max"].ToString();

                }
            }
            return model;
        }

        public bool postCategory(CategoryModel model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {    
                new SqlParameter("@CategoryID", model.CategoryID),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@ImgPath", model.ImgPath),
                new SqlParameter("@EnableOrDisable", model.EnableORDisable),
                new SqlParameter("@Min", model.Min),
                new SqlParameter("@Max", model.Max),
            };

            return DBHelper.ExecuteNonQuery("uspPOST_Category", CommandType.StoredProcedure, parameters);
        }

        public bool disableCategory(int categoryID)
        {
            string query = "update [Category] set  Deleted = 'True' where CategoryID = @CategoryID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@CategoryID", categoryID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        public bool categoryExist(string name)
        {
            string query = "select count(*) as [Status] from [Category]  where Name = @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@Name", name)
            };
            DataTable dt = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters);
            if (dt.Rows.Count == 1)
            {
                return Convert.ToBoolean(dt.Rows[0]["Status"]);
            }
            return true;
        }
    }
}