using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TIOT_WEB.Models;

namespace TIOT_WEB.DAL
{
    public class ClientDLL
    {
        public List<ClientModel> getClient()
        {
            List<ClientModel> list = new List<ClientModel>();
            using (DataTable table = DBHelper.ExecuteSelectCommand("uspGET_Clients", CommandType.StoredProcedure))
            {
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        ClientModel model = new ClientModel();
                        model.ClientID = Convert.ToInt32(row["ClientID"]);
                        model.Name = row["Name"].ToString();
                        model.Address = row["Address"].ToString();
                        model.OperatorID = Convert.ToInt32(row["OperatorID"]);
                        model.Contact = row["Contact"].ToString();
                        model.Code = row["Code"].ToString();
                        model.Email = row["Email"].ToString();
                        model.Enabled = Convert.ToBoolean(row["Enabled"]);
                        model.ExpireDate = Convert.ToDateTime(row["ExpireDate"]);
                        model.Deleted = Convert.ToBoolean(row["Deleted"]);
                        list.Add(model);
                    }
                }
            }
            return list;
        }

        public ClientModel getClientByClientID(int clientID)
        {

            ClientModel model = new ClientModel();
            string query = "select * from  [Client] where ClientID = @ClientID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ClientID", clientID),
            };

            using (DataTable table = DBHelper.ExecuteParamerizedSelectCommand(query, CommandType.Text, parameters))
            {
                if (table.Rows.Count == 1)
                {
                    DataRow row = table.Rows[0];
                    model.ClientID = Convert.ToInt32(row["ClientID"]);
                    model.OperatorID = Convert.ToInt32(row["OperatorID"]);
                    model.Address = row["Address"].ToString();
                    model.Name = row["Name"].ToString();
                    model.Contact = row["Contact"].ToString();
                    model.Code = row["Code"].ToString();
                    model.Email = row["Email"].ToString();
                    model.ExpireDate = Convert.ToDateTime(row["ExpireDate"]);
                }
            }
            return model;
        }

        public bool postClient(ClientModel model)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {    
                new SqlParameter("@ClientID", model.ClientID),
                new SqlParameter("@Name", model.Name),
                new SqlParameter("@Address", model.Address),
                new SqlParameter("@OperatorID", model.OperatorID),
                new SqlParameter("@Contact", model.Contact),
                new SqlParameter("@Code", model.Code),
                new SqlParameter("@Email", model.Email),
                new SqlParameter("@ExpireDate", model.ExpireDate),
            };

            return DBHelper.ExecuteNonQuery("uspPOST_Client", CommandType.StoredProcedure, parameters);
        }

        public bool disableClient(int clientID)
        {
            string query = "update [Client] set Enabled = 'False' , Deleted = 'True' where ClientID = @ClientID";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@ClientID", clientID)
            };
            return DBHelper.ExecuteNonQuery(query, CommandType.Text, parameters);
        }
        
        public bool codeExist(string code,string name)
        {
            string query = "Select count(*) as [Status] from [Client]  where Code = @Code or Name = @Name";
            SqlParameter[] parameters = new SqlParameter[]
            {   
                new SqlParameter("@Code", code),
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