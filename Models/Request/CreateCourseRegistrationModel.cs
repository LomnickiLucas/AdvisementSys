using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class CreateCourseRegistrationModel
    {
        public part_timeAnd_orAdditionalCourseRegistrationForm _part_timeAnd_orAdditionalCourseRegistrationForm { get; set; }

        public student _student { get; set; }
    }
}