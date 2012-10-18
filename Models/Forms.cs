using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AdvisementSys.Models
{
    public class FormsPOCO
    {
        [DataMember]
        public String Date { get; set; }

        [DataMember]
        public String FormType { get; set; }

        [DataMember]
        public String Status { get; set; }

        [DataMember]
        public String Controller { get; set; }

        [DataMember]
        public Guid FormID { get; set; }
    }
}