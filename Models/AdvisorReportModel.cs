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

        [Required]
        [Display(Name = "User")]
        public String User { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime startDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime endDate { get; set; }

        public String Employee { get; set; }

        public IEnumerable<AutoCompletePOCO> EmployeeID { get; set; }
    }
}
