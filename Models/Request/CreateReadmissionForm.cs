using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class CreateReadmissionForm
    {
        public applicationForReadmission _applicationForReadmission { get; set; }

        public student _student { get; set; }

        public IEnumerable<campu> _campus { get; set; }
    }
}