using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class DetailsReadmissionForm
    {
        public applicationForReadmission _applicationForReadmission { get; set; }

        public student _student { get; set; }
    }
}