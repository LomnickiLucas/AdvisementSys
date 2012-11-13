using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AdvisementSys.Models
{
    public class Events
    {
        [Display(Name = "id")]
        public Guid id { get; set; }

        [Display(Name = "title")]
        public String title { get; set; }

        [Display(Name = "start")]
        public String start { get; set; }

        [Display(Name = "end")]
        public String end { get; set; }

        [Display(Name = "allDay")]
        public Boolean allDay { get; set; }

        [Display(Name = "url")]
        public String url { get; set; }
    }
}