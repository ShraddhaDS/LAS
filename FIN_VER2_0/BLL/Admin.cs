using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class Admin_BL
    {

     
       public int InsertUser_Master(User us)
        {

            Admin_DL admin = new Admin_DL();
            return admin.InsertUser_Master(us);
        }

        public List<User> getList()
        {
            List<User> liuser = new List<User>();

         
          return  liuser;
           
        }

      
      
    }
}
