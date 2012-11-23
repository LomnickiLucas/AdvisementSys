using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class IndexCalendarModel
    {
        public IEnumerable<Events> Events { get; set; }

        public IEnumerable<String> cNames { get; set; }

        public IEnumerable<AutoCompletePOCO> AutoCom { get; set; }

        public String advisorID { get; set; }

        public String cName { get; set; }
    }
}