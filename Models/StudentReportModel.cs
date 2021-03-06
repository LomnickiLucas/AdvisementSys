﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace AdvisementSys.Models
{
    public class StudentReportModel
    {
        [Required]
        [Display(Name = "User")]
        public String User { get; set; }

        public String Student { get; set; }

        [Required]
        [Display(Name = "StudentID")]
        public String StudentID { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public IEnumerable<AutoCompletePOCO> StudID { get; set; }
    }
}
