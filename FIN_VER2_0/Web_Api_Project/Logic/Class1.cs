using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Web_Api_Project.Logic
{

    public class Registration
    {
        public string SR_NO { get; set; }
        public string EMAIL { get; set; }
        public string PASSWORD { get; set; }
        public string NAME { get; set; }
        public string ADDRESS { get; set; }
        public string CITY { get; set; }
        public bool STATUS { get; set; }
        public bool ISACTIVE { get; set; }
        

    }
    public class RegistrationList
    {
        public List<Registration> registrations { get; set; }
        public string totalCount { get; set; }
    }
    public class EmployeeInfo
    {
        public int ActivateDeactivateUsers(string id)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USP_ActivateDeactivateUser", connection);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = id;
             

                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    result  = cmd.ExecuteNonQuery();
                    return result;
                   
                }
                catch (Exception ex)
                {
                    throw;
                   
                }

            }
            return result;
        }
        public RegistrationList GetEmployees(int pageIndex, int pageSize, string SearchText)
        {
            RegistrationList employeeList = new RegistrationList();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["constring"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Get_REGISTRATION", connection);
                cmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                cmd.Parameters.Add("@pageSize", SqlDbType.Int).Value = pageSize;
                if((SearchText=="") || (SearchText==null) || (SearchText == "undefined"))
                    {
                    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = DBNull.Value;
                    }
                else
                {
                    cmd.Parameters.Add("@SearchText", SqlDbType.VarChar).Value = SearchText;
                }
               
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    List<Registration> listEmp = new List<Registration>();
                    while (dr.Read())
                    {
                        Registration emp = new Registration();
                        emp.SR_NO = dr["SR_NO"].ToString();
                        emp.NAME = dr["name"].ToString();
                        emp.EMAIL = dr["email"].ToString();
                        emp.ADDRESS = dr["address"].ToString();
                        emp.STATUS =Convert.ToBoolean( dr["STATUS"].ToString());
                        emp.ISACTIVE = true;
                        listEmp.Add(emp);
                    }

                    dr.NextResult();

                    while (dr.Read())
                    {
                        employeeList.totalCount = dr["totalCount"].ToString();
                    }
                    employeeList.registrations = listEmp;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return employeeList;
        }
    }
}