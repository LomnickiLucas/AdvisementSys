using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class DetailsCourseRegistrationModel
    {
        public part_timeAnd_orAdditionalCourseRegistrationForm _part_timeAnd_orAdditionalCourseRegistrationForm { get; set; }

        public IEnumerable<note> _note { get; set; }

        public note _NewNote { get; set; }

        public employee _employee { get; set; }

        public DateTime _date { get; set; }

        public student _student { get; set; }
    }
}