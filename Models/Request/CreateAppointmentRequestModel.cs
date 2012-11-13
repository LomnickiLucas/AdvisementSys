using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class CreateAppointmentRequestModel
    {
        public appointment _appointment { get; set; }

        public IEnumerable<String> _campus { get; set; }

        public IEnumerable<String> StudID { get; set; }

        public IEnumerable<String> EmployeeID { get; set; }

        public String startTime { get; set; }

        public String endTime { get; set; }
    }
}