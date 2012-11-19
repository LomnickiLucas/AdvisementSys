using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AdvisementSys.Models
{
    public class CampusReportModel
    {
        [Required]
        [Display(Name = "Program")]
        public String Program { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Program List")]
        public IEnumerable<String> ProgList { get; set; }

        [Required]
        [Display(Name = "User")]
        public String User { get; set; }
    }
}