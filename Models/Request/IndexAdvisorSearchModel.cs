using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvisementSys.Models;
using System.ComponentModel.DataAnnotations;

namespace AdvisementSys.Controllers
{
    public class IndexAdvisorSearchModel
    {
        
        public IEnumerable<EmployeePOCO> _employee { get; set; }

        public IEnumerable<String> _faculty { get; set; }

        public IEnumerable<String> _role { get; set; }

        public IEnumerable<String> _EmployeeID { get; set; }

        [Display(Name = "Faculty")]
        public String faculty { get; set; }

        [Display(Name = "Role")]
        public String role { get; set; }

        [Display(Name = "Email")]
        public String email { get; set; }

        [Display(Name = "Last Name")]
        public String lname { get; set; }

        [Display(Name = "First Name")]
        public String fname { get; set; }

        [Display(Name = "Employee ID")]
        public String EmployeeID { get; set; }
    }
}
