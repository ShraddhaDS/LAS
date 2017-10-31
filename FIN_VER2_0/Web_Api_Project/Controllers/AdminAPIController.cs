using BLL;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Web_Api_Project.Controllers
{
    [RoutePrefix("/api/AdminAPI")]
    public class AdminAPIController : ApiController
    {
        
        [HttpPost]
        public HttpResponseMessage InsertUser_Master(User us)
        {
          
            Admin_BL db = new Admin_BL();
            //us = new Model.User();
            int result = 0;
            try
            {              
                db.InsertUser_Master(us);   
                

                result = 1;
            }
            catch (Exception e)
            {

                result = 0;
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    

        [HttpGet]
        public List<User> GetUsers(User us)
        {
            Admin_DL dll = new Admin_DL();

          
            Admin_BL db = new Admin_BL();
            List<User> Users = new List<User>();
            try
            {


                // Users = db.InsertUser_Master(us);
                //Users = dbContext.tblStudents.ToList();

            }
            catch (Exception e)
            {
                Users = null;
            }

            return Users;
        }

       
       
        
}

    
}
