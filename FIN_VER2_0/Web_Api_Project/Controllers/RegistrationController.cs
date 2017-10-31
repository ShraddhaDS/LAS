using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_Api_Project.Logic;

namespace Web_Api_Project.Controllers
{
    public class RegistrationController : ApiController
    {
        [HttpGet]
        public RegistrationList GetEmployees(int pageIndex, int pageSize, string SearchText)
        {
            EmployeeInfo empInfo = new EmployeeInfo();
            RegistrationList empList = empInfo.GetEmployees(pageIndex, pageSize, SearchText);
            return empList;
        }
        [HttpPost]
        public HttpResponseMessage ActivateDeActivateUser( Registration reg)
        {
            Registration rs = reg;
            EmployeeInfo empInfo = new EmployeeInfo();
            empInfo.ActivateDeactivateUsers(reg.SR_NO);
            return Request.CreateResponse(HttpStatusCode.OK, 1);
        }
    }
}
