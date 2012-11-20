using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class IndexStudentRequestModel
    {
        public IEnumerable<StudentPOCO> _student { get; set; }

        public IEnumerable<program> _program { get; set; }

        public IEnumerable<String> _campus { get; set; }

        public String studentID { get; set; }

        public String fName { get; set; }

        public String lName { get; set; }

        public String programCode { get; set; }

        public String campus { get; set; }

        public String email { get; set; }

        public Boolean acadprobation { get; set; }

        public Boolean fulltimestatus { get; set; }

        public Boolean enrolled { get; set; }

        public IEnumerable<String> programs { get; set; }

        public IEnumerable<String> StudID { get; set; }
    }
}