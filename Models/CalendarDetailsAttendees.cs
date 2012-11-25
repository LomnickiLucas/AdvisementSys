using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models
{
    public class CalendarDetailsAttendees
    {
        public Attendee Attendees { get; set; }

        public employee Emplployee { get; set; }

        public student Student { get; set; }

        public String StudFaculty { get; set; }
    }
}