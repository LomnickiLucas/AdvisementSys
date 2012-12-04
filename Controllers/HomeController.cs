using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;

namespace AdvisementSys.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            try
            {
                DateTime date = DateTime.Today.AddDays(7);
                DateTime date2 = DateTime.Now.AddMonths(-1);
                IEnumerable<appointment> appointments = db.appointments.Where(i => i.employeeid == User.Identity.Name && i.starttime >= DateTime.Today && i.starttime <= date);
                appointments = appointments.OrderBy(a => a.starttime);
                IEnumerable<issue> issues = db.issues.Where(i => i.date <= date2 && i.status != "Complete");
                List<Events> Events = new List<Events>();
                foreach (appointment appointment in appointments)
                {
                    Events Event = new Events();
                    Event.id = appointment.appointmentid;
                    Event.title = appointment.subject;
                    Event.allDay = appointment.allday;
                    Event.start = ConvertToUnixTimestamp(appointment.starttime).ToString();
                    Event.end = ConvertToUnixTimestamp(appointment.endtime).ToString();
                    Event.url = Url.Action("Details/" + appointment.appointmentid.ToString());
                    switch (appointment.appointmenttype.Trim())
                    {
                        case "Personal":
                            Event.color = "#009B00";
                            break;

                        case "Advisement":
                            Event.color = "#36C";
                            break;

                        case "Office":
                            Event.color = "#800080";
                            break;
                    }
                    Events.Add(Event);
                }

                List<IssuesPOCO> _issues = new List<IssuesPOCO>();
                foreach (issue i in issues.OrderByDescending(e => e.date))
                {
                    IssuesPOCO temp = new IssuesPOCO();
                    temp.IssueID = i.issueid;
                    temp.Name = i.issuename;
                    temp.Date = ConvertToUnixTimestamp(i.date).ToString();
                    temp.Status = i.status;
                    temp.Urgency = i.urgency;

                    _issues.Add(temp);
                }

                IndexHomeModel model = new IndexHomeModel() { _Events = Events, _Issues = _issues };

                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        private double ConvertToUnixTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }

    }
}
