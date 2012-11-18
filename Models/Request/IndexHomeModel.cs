using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvisementSys.Models;

namespace AdvisementSys.Controllers
{
    public class IndexHomeModel
    {
        public IEnumerable<Events> _Events { get; set; }

        public IEnumerable<IssuesPOCO> _Issues { get; set; }
    }
}
