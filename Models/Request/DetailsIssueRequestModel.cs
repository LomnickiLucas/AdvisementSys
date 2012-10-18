using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class DetailsIssueRequestModel
    {
        public issue _issue { get; set; }

        public student _student { get; set; }

        public program _program { get; set; }

        public IEnumerable<FormsPOCO> _Forms { get; set; }

        public IEnumerable<note> _note { get; set; }

        public note _newNote { get; set; }

        public String _EmployeeID { get; set; }

        public employee _employee { get; set; }

        public DateTime _date { get; set; }
    }
}