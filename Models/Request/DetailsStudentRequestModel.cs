using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models
{
    public class DetailsStudentRequestModel
    {
        public student _student { get; set; }

        public IEnumerable<IssuesPOCO> _issue { get; set; }
    }
}