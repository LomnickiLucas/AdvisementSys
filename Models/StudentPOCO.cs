using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models
{
    public class StudentPOCO
    {
        public String StudentID { get; set; }

        public String FName { get; set; }

        public String LName { get; set; }

        public String Prog { get; set; }

        public String Campus { get; set; }

        public String PhoneNum { get; set; }

        public String Email { get; set; }

        public Boolean Prob { get; set; }

        public Boolean FT { get; set; }

        public Boolean Enr { get; set; }
    }
}