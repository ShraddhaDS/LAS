using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
         public int? Userid     { get; set; }
        [Required(ErrorMessage = "First Name is required.", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Last Name is required.", AllowEmptyStrings = false)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }

        public string Designation { get; set; }
        public int ActionBy { get; set; }
        public string strActionBy { get; set; }
        public bool IsActive { get; set; }
        public string strIsActive { get; set; }
    }
    public class Role
    {
        public int? Roleid { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string strStatus { get; set; }
    }
}
