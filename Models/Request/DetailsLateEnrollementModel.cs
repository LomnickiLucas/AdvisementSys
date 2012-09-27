using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class DetailsLateEnrollementModel
    {
        public requestForLateEnrolment _requestForLateEnrolment { get; set; }

        public student _student { get; set; }
    }
}