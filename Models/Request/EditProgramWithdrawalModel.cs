﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class EditProgramWithdrawalModel
    {
        public applicationForTermOrCompleteProgramWithdrawal _applicationForTermOrCompleteProgramWithdrawal { get; set; }

        public student _student { get; set; }
    }
}