using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class IndexIssueRequestModel
    {
        public IEnumerable<issue> _issue { get; set; }

        public IEnumerable<String> _employee { get; set; }

        public IEnumerable<catagory> _catagories { get; set; }

        public List<String> _programcode { get; set; }

        public String _selectedpcode { get; set;}

        public String _employeeid { get; set; }

        public String _name { get; set; }

        public DateTime _date1 { get; set; }

        public DateTime _date2 { get; set; }

        public String _status { get; set; }

        public String _urgency { get; set; }

        public String _category { get; set; }
    }
}