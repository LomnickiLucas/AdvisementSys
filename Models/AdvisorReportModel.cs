using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AdvisementSys.Models
{
    public class AdvisorReportModel
    {
        [Required]
        [Display(Name = "Employee ID")]
        public String EmpID { get; set; }

        public IEnumerable<String> EmployeeID { get; set; }
    }
}
