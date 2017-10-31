using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public class Admin_DL
    {
        public int InsertUser_Master(User us)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Add("@FirstName",us.FirstName);
            cmd.Parameters.Add("@MiddleName", us.MiddleName);
            cmd.Parameters.Add("@LastName", us.LastName);
            cmd.Parameters.Add("@Email", us.Email);
            cmd.Parameters.Add("@Contact", us.Contact);
            cmd.Parameters.Add("@Designation", us.Designation);
            cmd.Parameters.Add("@ActionBy", 1);
            //cmd.Parameters.Add("@strActionBy", us.strActionBy);
            cmd.Parameters.Add("@IsActive", us.IsActive);
            //cmd.Parameters.Add("@strIsActive", us.strIsActive);
           
            cmd.Parameters.Add("@Flag", "I");
           

            return DBUtility.ExecuteNonQuery(ref cmd, "USP_UserMaster");
        }

        public DataSet DsUsers(User us)
        {
            SqlCommand cmd = new SqlCommand();
         
            return DBUtility.ExecuteDS(ref cmd, "USP_UserMaster");

        }
       

       
    }
}
