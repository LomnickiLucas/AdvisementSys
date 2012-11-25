using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvisementSys.Models;

namespace AdvisementSys.Models.Request
{
    public class DetailsCalendarModel
    {
        public appointment appointment { get; set; }

        public IEnumerable<CalendarDetailsAttendees> Attendees { get; set; }

        public employee chair { get; set; }
    }
}
