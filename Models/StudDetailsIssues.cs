using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models
{
    public class IssuesPOCO
    {
        public Guid IssueID { get; set; }

        public String Name { get; set; }

        public String Date { get; set; }

        public String Status { get; set; }

        public String Urgency { get; set; }

        public String Catagory { get; set; }

        public String Owner { get; set; }
    }
}