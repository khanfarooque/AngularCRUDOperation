using AngularCRUDOperation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AngularCRUDOperation.Repository
{
    public class DB
    {
        string conStr = ConfigurationManager.ConnectionStrings["cs"].ConnectionString.ToString();
        
        // Add Data
        public void InsertEmployee(EmployeeModel employeeModel)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = new SqlCommand();
            com.CommandText = "InsertEmployee";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@empname", employeeModel.empname);
            com.Parameters.AddWithValue("@empaddress", employeeModel.empaddress);
            com.Parameters.AddWithValue("@empcity", employeeModel.empcity);
            com.Connection = con;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }
        //Display all record
        public DataSet SelectAllEmployee()
        {
            SqlConnection con = new SqlConnection(conStr);
            
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee",con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        // Update all record
        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = new SqlCommand();
            com.CommandText = "UpdateEmployee";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@empid", employeeModel.empid);
            com.Parameters.AddWithValue("@empname", employeeModel.empname);
            com.Parameters.AddWithValue("@empaddress", employeeModel.empaddress);
            com.Parameters.AddWithValue("@empcity", employeeModel.empcity);
            com.Connection = con;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            
        }
        // Get Record by id
        public DataSet SelectEmployeeById(int id)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Employee WHERE empid=" + Convert.ToInt32(id) + "", con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        // Delete record
        public void DeleteEmployee(int empid)
        {
            SqlConnection con = new SqlConnection(conStr);
            SqlCommand com = new SqlCommand();
            com.CommandText = "DeleteEmployee";
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@empid", empid);
            com.Connection = con;
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }


        public string InsertEmployee(List<EmployeeModel> employeeModels)
        {
            string res = string.Empty;
            try
            {
                if (employeeModels != null)
                {
                    for (int i = 0; i < employeeModels.Count - 1; i++)
                    {
                        SqlConnection con = new SqlConnection(conStr);
                        SqlCommand com = new SqlCommand();
                        com.CommandText = "INSERT INTO Employee (empname, empaddress, empcity) VALUES ('" + employeeModels[i].empname + "','" + employeeModels[i].empaddress + "','" + employeeModels[i].empcity + "')";
                        com.CommandType = CommandType.Text;
                        com.Connection = con;
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();
                        res = "success";
                    }
                }
                return res;
            }
            catch {
               return res = "failed";
            }
        }

        public int userlogin(UserModel userModel, out UserModel modelss)
        {
            int result = 0;
            modelss = new UserModel();
            SqlConnection con = new SqlConnection(conStr);
            string query = "SELECT * FROM UserDetail WHERE user_id='" + userModel.user_id + "' AND user_pass='" + userModel.user_pass + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserDetail");
            if (ds.Tables["UserDetail"].Rows.Count > 0)
            {
                modelss.user_id = ds.Tables["UserDetail"].Rows[0]["user_id"].ToString();
                modelss.user_pass = ds.Tables["UserDetail"].Rows[0]["user_pass"].ToString();
                modelss.user_name = ds.Tables["UserDetail"].Rows[0]["user_name"].ToString();
                modelss.user_role = ds.Tables["UserDetail"].Rows[0]["user_role"].ToString();
               
                result = 1;   
            }
            else
            {
                result = 0;
            }

            return result;
        }
    }
}