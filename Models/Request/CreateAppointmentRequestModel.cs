using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using AdvisementSys.Controllers;

namespace AdvisementSys.Models.Request
{
    public class CreateAppointmentRequestModel
    {
        public appointment _appointment { get; set; }

        public IEnumerable<String> _campus { get; set; }

        public IEnumerable<AutoCompletePOCO> AttendeesAutoComplete { get; set; }

        public IEnumerable<AutoCompletePOCO> EmployeeID { get; set; }

        public IEnumerable<String> appoingmentType { get; set; }

        public String startTime { get; set; }

        public String endTime { get; set; }

        public String Attendees { get; set; }

        public Boolean emailAll { get; set; }
    }
}