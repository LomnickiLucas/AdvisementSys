using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class EditUserModel
    {
        public employee _employee { get; set; }

        public IEnumerable<String> faculty { get; set; } 
    }
}