﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvisementSys.Models.Request
{
    public class CreateProbationaryContractModel
    {
        public probationaryContractPlan _probationaryContractPlan { get; set; }

        public student _student { get; set; }
    }
}