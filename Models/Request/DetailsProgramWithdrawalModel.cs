using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class DetailsProgramWithdrawalModel
    {
        public applicationForTermOrCompleteProgramWithdrawal _applicationForTermOrCompleteProgramWithdrawal { get; set; }

        public student _student { get; set; }
    }
}