using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models
{
    public class StudentResponseModel
    {
        public IEnumerable<student> _student { get; set; }

        public IEnumerable<issue> _issue { get; set; }

        public IEnumerable<program> _program { get; set; }

        public IEnumerable<note> _programm { get; set; }
    }
}