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
    }
}