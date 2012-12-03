using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class EditCalendarModel
    {
        public appointment appointment { get; set; }

        public IEnumerable<CalendarDetailsAttendees> Attendees { get; set; }

        public employee chair { get; set; }

        public IEnumerable<String> _campus { get; set; }

        public IEnumerable<String> appoingmentType { get; set; }

        public String startTime { get; set; }

        public String endTime { get; set; }

        public IEnumerable<AutoCompletePOCO> EmployeeID { get; set; }
    }
}