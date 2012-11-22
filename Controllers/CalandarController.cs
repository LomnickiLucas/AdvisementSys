using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using AdvisementSys.Models.Request;
using System.Data;
using System.Net.Mail;
using System.Text;

namespace AdvisementSys.Controllers
{
    public class CalendarController : Controller
    {
        private Entities db = new Entities();
        //
        // GET: /Calandar/

        public ActionResult Index()
        {
            IEnumerable<appointment> appointments = db.appointments.Where(i => i.employeeid == User.Identity.Name);
            IEnumerable<Attendee> Attendee = db.Attendees.Where(i => i.attendee1 == User.Identity.Name);
            List<Guid> AppointmentIDs = new List<Guid>();
            foreach (Attendee Attend in Attendee)
            {
                AppointmentIDs.Add(Attend.appointmentid);
            }

            foreach (Guid id in AppointmentIDs)
            {
                appointments.Concat(db.appointments.Where(i => i.appointmentid == id));
            }

            List<Events> model = new List<Events>();
            foreach (appointment appointment in appointments)
            {
                Events Events = new Events();
                Events.id = appointment.appointmentid;
                Events.title = appointment.subject;
                Events.allDay = appointment.allday;
                Events.start = ConvertToUnixTimestamp(appointment.starttime).ToString();
                Events.end = ConvertToUnixTimestamp(appointment.endtime).ToString();
                Events.url = Url.Action("Details/" + appointment.appointmentid.ToString());
                switch (appointment.appointmenttype.Trim())
                {
                    case "Personal":
                        Events.color = "#009B00";
                        break;

                    case "Advisement":
                        Events.color = "#36C";
                        break;

                    case "Office":
                        Events.color = "#800080";
                        break;
                }

                model.Add(Events);
            }
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            appointment appointment = db.appointments.Single(i => i.appointmentid == id);
            IEnumerable<Attendee> Attendee = db.Attendees.Where(i => i.appointmentid == id);
            List<CalendarDetailsAttendees> Attendees = new List<CalendarDetailsAttendees>();

            foreach (Attendee attend in Attendee)
            {
                student student = db.students.SingleOrDefault(s => s.studentid == attend.attendee1);
                employee employee = db.employees.SingleOrDefault(e => e.employeeid == attend.attendee1);

                CalendarDetailsAttendees DetailAttendees = new CalendarDetailsAttendees()
                {
                    Attendees = attend,
                    Student = student,
                    Emplployee = employee
                };

                Attendees.Add(DetailAttendees);
            }

            DetailsCalendarModel model = new DetailsCalendarModel()
                {
                    appointment = appointment,
                    Attendees = Attendees
                };

            return View(model);
        }

        public ActionResult Create(String id, String subject)
        {
            appointment appointment = new appointment() { starttime = DateTime.Now, endtime = DateTime.Now.AddHours(.5) };
            employee employee = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
            appointment.employeeid = employee.fname + " " + employee.lname + " (" + employee.employeeid + ")";
            if (subject != null)
                appointment.subject = subject;
            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
            }
            IEnumerable<student> students = db.students;
            List<AutoCompletePOCO> AutoCompleteID = new List<AutoCompletePOCO>();
            foreach (student stud in students)
            {
                AutoCompletePOCO poco = new AutoCompletePOCO()
                {
                    value = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                    Label = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                    Email = stud.email,
                    Role = "Student"
                };
                AutoCompleteID.Add(poco);
            }
            IEnumerable<employee> employees = db.employees;
            List<AutoCompletePOCO> EmployeeID = new List<AutoCompletePOCO>();
            foreach (employee emp in employees)
            {
                AutoCompletePOCO poco = new AutoCompletePOCO()
                {
                    value = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                    Label = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                    Email = emp.email,
                    Role = emp.role
                };
                AutoCompleteID.Add(poco);
                EmployeeID.Add(poco);
            }
            String[] AppointmentType = new String[3] { "Personal", "Advisement", "Office" };

            CreateAppointmentRequestModel model = new CreateAppointmentRequestModel() { _appointment = appointment, _campus = list, AttendeesAutoComplete = AutoCompleteID, EmployeeID = EmployeeID, startTime = DateTime.Now.ToShortTimeString(), endTime = DateTime.Now.AddHours(.5).ToShortTimeString(), appoingmentType = AppointmentType, emailAll = true, repeatingType = new String[] { "Not Repeating", "Daily (Business Days)", "Weekly", "Bi-Weekly", "Monthly", "Yearly" } };
            if (id != null)
            {
                student student = db.students.Single(s => s.studentid == id);
                model.Attendees = student.fname + " " + student.lname + " (" + student.studentid + "), ";
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAppointmentRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<String> attendeesID = new List<string>();
                    if (model.Attendees != null)
                    {
                        String EmployeeID = model.Attendees;
                        String[] attendees = EmployeeID.Split(',');

                        for (int i = 0; i < attendees.Count() - 1; i++)
                        {
                            if (i != 0)
                            {
                                String id = attendees[i].Remove(0, attendees[i].Length - 10);
                                attendeesID.Add(id.Remove(id.Length - 1));
                            }
                            else
                            {
                                String id = attendees[i].Remove(0, attendees[i].Length - 10);
                                attendeesID.Add(id.Remove(id.Length - 1));
                            }
                        }
                    }
                    model._appointment.employeeid = model._appointment.employeeid.Remove(0, model._appointment.employeeid.Length - 10);
                    model._appointment.employeeid = model._appointment.employeeid.Remove(model._appointment.employeeid.Length - 1);

                    model.repeatingType = new String[] { "Not Repeating", "Daily (Business Days)", "Weekly", "Bi-Weekly", "Monthly", "Yearly" };
                    IEnumerable<campu> campus = db.campus;
                    List<String> list = new List<String>();
                    foreach (campu camp in campus)
                    {
                        list.Add(camp.cname);
                    }
                    IEnumerable<student> students = db.students;
                    List<AutoCompletePOCO> AutoCompleteID = new List<AutoCompletePOCO>();
                    foreach (student stud in students)
                    {
                        AutoCompletePOCO poco = new AutoCompletePOCO()
                        {
                            value = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                            Label = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                            Email = stud.email,
                            Role = "Student"
                        };
                        AutoCompleteID.Add(poco);
                    }
                    IEnumerable<employee> employees = db.employees;
                    List<AutoCompletePOCO> EmpAutoCom = new List<AutoCompletePOCO>();
                    foreach (employee emp in employees)
                    {
                        AutoCompletePOCO poco = new AutoCompletePOCO()
                        {
                            value = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                            Label = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                            Email = emp.email,
                            Role = emp.role
                        };
                        AutoCompleteID.Add(poco);
                        EmpAutoCom.Add(poco);
                    }
                    model.AttendeesAutoComplete = AutoCompleteID;
                    model.EmployeeID = EmpAutoCom;
                    String[] AppointmentType = new String[3] { "Personal", "Advisement", "Office" };
                    model._campus = list;
                    model.appoingmentType = AppointmentType;

                    DateTime date1 = DateTime.Parse(model.startTime);
                    TimeSpan time1 = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                    DateTime date2 = DateTime.Parse(model.endTime);
                    TimeSpan time2 = new TimeSpan(date2.Hour, date2.Minute, date2.Second);
                    model._appointment.starttime = model._appointment.starttime.Add(time1);
                    model._appointment.endtime = model._appointment.endtime.Add(time2);

                    if (model.repeating.Trim().Equals("Not Repeating"))
                    {
                        model._appointment.appointmentid = Guid.NewGuid();
                        db.appointments.AddObject(model._appointment);
                        db.SaveChanges();

                        if (attendeesID.Count > 0)
                        {
                            List<Attendee> Attendee = new List<Models.Attendee>();
                            foreach (String id in attendeesID)
                            {
                                Attendee attendee = new Attendee()
                                {
                                    id = Guid.NewGuid(),
                                    appointmentid = model._appointment.appointmentid,
                                    attendee1 = id
                                };

                                if (!attendee.attendee1.Equals(model.EmployeeID))
                                    Attendee.Add(attendee);
                            }
                            foreach (Attendee attend in Attendee)
                            {
                                db.Attendees.AddObject(attend);
                                db.SaveChanges();
                            }
                        }
                    }
                    else if (model.repeating.Trim().Equals("Daily (Business Days)"))
                    {
                        DateTime endRepeatingDate = model._appointment.endtime;
                        model._appointment.endtime = model._appointment.starttime;
                        model._appointment.endtime = model._appointment.endtime.Add(-time1);
                        model._appointment.endtime = model._appointment.endtime.Add(time2);
                        model._appointment.repeating = Guid.NewGuid();

                        while (model._appointment.starttime.Date <= endRepeatingDate.Date)
                        {
                            if ((model._appointment.starttime.DayOfWeek != DayOfWeek.Saturday) && (model._appointment.starttime.DayOfWeek != DayOfWeek.Sunday))
                            {
                                appointment appointment = new appointment()
                                    {
                                        allday = model._appointment.allday,
                                        appointmenttype = model._appointment.appointmenttype,
                                        cname = model._appointment.cname,
                                        description = model._appointment.description,
                                        employeeid = model._appointment.employeeid,
                                        endtime = model._appointment.endtime,
                                        repeating = model._appointment.repeating,
                                        starttime = model._appointment.starttime,
                                        subject = model._appointment.subject
                                    };
                                appointment.appointmentid = Guid.NewGuid();
                                model._appointment.appointmentid = appointment.appointmentid;
                                db.appointments.AddObject(appointment);
                                db.SaveChanges();

                                if (attendeesID.Count > 0)
                                {
                                    List<Attendee> Attendee = new List<Models.Attendee>();
                                    foreach (String id in attendeesID)
                                    {
                                        Attendee attendee = new Attendee()
                                        {
                                            id = Guid.NewGuid(),
                                            appointmentid = appointment.appointmentid,
                                            attendee1 = id
                                        };

                                        if (!attendee.attendee1.Equals(model.EmployeeID))
                                            Attendee.Add(attendee);
                                    }
                                    foreach (Attendee attend in Attendee)
                                    {
                                        db.Attendees.AddObject(attend);
                                        db.SaveChanges();
                                    }
                                }
                            }
                            model._appointment.starttime = model._appointment.starttime.AddDays(1);
                            model._appointment.endtime = model._appointment.endtime.AddDays(1);
                        }

                    }
                    else if (model.repeating.Trim().Equals("Weekly"))
                    {
                        DateTime endRepeatingDate = model._appointment.endtime;
                        model._appointment.endtime = model._appointment.starttime;
                        model._appointment.endtime = model._appointment.endtime.Add(-time1);
                        model._appointment.endtime = model._appointment.endtime.Add(time2);
                        model._appointment.repeating = Guid.NewGuid();

                        while (model._appointment.starttime.Date <= endRepeatingDate.Date)
                        {
                                appointment appointment = new appointment()
                                {
                                    allday = model._appointment.allday,
                                    appointmenttype = model._appointment.appointmenttype,
                                    cname = model._appointment.cname,
                                    description = model._appointment.description,
                                    employeeid = model._appointment.employeeid,
                                    endtime = model._appointment.endtime,
                                    repeating = model._appointment.repeating,
                                    starttime = model._appointment.starttime,
                                    subject = model._appointment.subject
                                };
                                appointment.appointmentid = Guid.NewGuid();
                                model._appointment.appointmentid = appointment.appointmentid;
                                db.appointments.AddObject(appointment);
                                db.SaveChanges();

                                if (attendeesID.Count > 0)
                                {
                                    List<Attendee> Attendee = new List<Models.Attendee>();
                                    foreach (String id in attendeesID)
                                    {
                                        Attendee attendee = new Attendee()
                                        {
                                            id = Guid.NewGuid(),
                                            appointmentid = appointment.appointmentid,
                                            attendee1 = id
                                        };

                                        if (!attendee.attendee1.Equals(model.EmployeeID))
                                            Attendee.Add(attendee);
                                    }
                                    foreach (Attendee attend in Attendee)
                                    {
                                        db.Attendees.AddObject(attend);
                                        db.SaveChanges();
                                    }
                            }
                            model._appointment.starttime = model._appointment.starttime.AddDays(7);
                            model._appointment.endtime = model._appointment.endtime.AddDays(7);
                        }

                    }
                    else if (model.repeating.Trim().Equals("Bi-Weekly"))
                    {
                        DateTime endRepeatingDate = model._appointment.endtime;
                        model._appointment.endtime = model._appointment.starttime;
                        model._appointment.endtime = model._appointment.endtime.Add(-time1);
                        model._appointment.endtime = model._appointment.endtime.Add(time2);
                        model._appointment.repeating = Guid.NewGuid();

                        while (model._appointment.starttime.Date <= endRepeatingDate.Date)
                        {
                            appointment appointment = new appointment()
                            {
                                allday = model._appointment.allday,
                                appointmenttype = model._appointment.appointmenttype,
                                cname = model._appointment.cname,
                                description = model._appointment.description,
                                employeeid = model._appointment.employeeid,
                                endtime = model._appointment.endtime,
                                repeating = model._appointment.repeating,
                                starttime = model._appointment.starttime,
                                subject = model._appointment.subject
                            };
                            appointment.appointmentid = Guid.NewGuid();
                            model._appointment.appointmentid = appointment.appointmentid;
                            db.appointments.AddObject(appointment);
                            db.SaveChanges();

                            if (attendeesID.Count > 0)
                            {
                                List<Attendee> Attendee = new List<Models.Attendee>();
                                foreach (String id in attendeesID)
                                {
                                    Attendee attendee = new Attendee()
                                    {
                                        id = Guid.NewGuid(),
                                        appointmentid = appointment.appointmentid,
                                        attendee1 = id
                                    };

                                    if (!attendee.attendee1.Equals(model.EmployeeID))
                                        Attendee.Add(attendee);
                                }
                                foreach (Attendee attend in Attendee)
                                {
                                    db.Attendees.AddObject(attend);
                                    db.SaveChanges();
                                }
                            }
                            model._appointment.starttime = model._appointment.starttime.AddDays(14);
                            model._appointment.endtime = model._appointment.endtime.AddDays(14);
                        }

                    }
                    else if (model.repeating.Trim().Equals("Monthly"))
                    {
                        DateTime endRepeatingDate = model._appointment.endtime;
                        model._appointment.endtime = model._appointment.starttime;
                        model._appointment.endtime = model._appointment.endtime.Add(-time1);
                        model._appointment.endtime = model._appointment.endtime.Add(time2);
                        model._appointment.repeating = Guid.NewGuid();

                        while (model._appointment.starttime.Date <= endRepeatingDate.Date)
                        {
                            appointment appointment = new appointment()
                            {
                                allday = model._appointment.allday,
                                appointmenttype = model._appointment.appointmenttype,
                                cname = model._appointment.cname,
                                description = model._appointment.description,
                                employeeid = model._appointment.employeeid,
                                endtime = model._appointment.endtime,
                                repeating = model._appointment.repeating,
                                starttime = model._appointment.starttime,
                                subject = model._appointment.subject
                            };
                            appointment.appointmentid = Guid.NewGuid();
                            model._appointment.appointmentid = appointment.appointmentid;
                            db.appointments.AddObject(appointment);
                            db.SaveChanges();

                            if (attendeesID.Count > 0)
                            {
                                List<Attendee> Attendee = new List<Models.Attendee>();
                                foreach (String id in attendeesID)
                                {
                                    Attendee attendee = new Attendee()
                                    {
                                        id = Guid.NewGuid(),
                                        appointmentid = appointment.appointmentid,
                                        attendee1 = id
                                    };

                                    if (!attendee.attendee1.Equals(model.EmployeeID))
                                        Attendee.Add(attendee);
                                }
                                foreach (Attendee attend in Attendee)
                                {
                                    db.Attendees.AddObject(attend);
                                    db.SaveChanges();
                                }
                            }
                            model._appointment.starttime = model._appointment.starttime.AddMonths(1);
                            model._appointment.endtime = model._appointment.endtime.AddMonths(1);
                        }

                    }
                    else if (model.repeating.Trim().Equals("Yearly"))
                    {
                        DateTime endRepeatingDate = model._appointment.endtime;
                        model._appointment.endtime = model._appointment.starttime;
                        model._appointment.endtime = model._appointment.endtime.Add(-time1);
                        model._appointment.endtime = model._appointment.endtime.Add(time2);
                        model._appointment.repeating = Guid.NewGuid();

                        while (model._appointment.starttime.Date <= endRepeatingDate.Date)
                        {
                            appointment appointment = new appointment()
                            {
                                allday = model._appointment.allday,
                                appointmenttype = model._appointment.appointmenttype,
                                cname = model._appointment.cname,
                                description = model._appointment.description,
                                employeeid = model._appointment.employeeid,
                                endtime = model._appointment.endtime,
                                repeating = model._appointment.repeating,
                                starttime = model._appointment.starttime,
                                subject = model._appointment.subject
                            };
                            appointment.appointmentid = Guid.NewGuid();
                            model._appointment.appointmentid = appointment.appointmentid;
                            db.appointments.AddObject(appointment);
                            db.SaveChanges();

                            if (attendeesID.Count > 0)
                            {
                                List<Attendee> Attendee = new List<Models.Attendee>();
                                foreach (String id in attendeesID)
                                {
                                    Attendee attendee = new Attendee()
                                    {
                                        id = Guid.NewGuid(),
                                        appointmentid = appointment.appointmentid,
                                        attendee1 = id
                                    };

                                    if (!attendee.attendee1.Equals(model.EmployeeID))
                                        Attendee.Add(attendee);
                                }
                                foreach (Attendee attend in Attendee)
                                {
                                    db.Attendees.AddObject(attend);
                                    db.SaveChanges();
                                }
                            }
                            model._appointment.starttime = model._appointment.starttime.AddYears(1);
                            model._appointment.endtime = model._appointment.endtime.AddYears(1);
                        }

                    }

                    employee creator = db.employees.Single(emp => emp.employeeid == User.Identity.Name);
                    ConfirmationEmail(creator, model._appointment);

                    if (model.emailAll)
                    {
                        EmailAttendees(creator, model._appointment);
                    }
                    return RedirectToAction("Details/" + model._appointment.appointmentid, "Calendar");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        public ActionResult Edit(Guid id)
        {
            appointment appointment = db.appointments.Single(i => i.appointmentid == id);
            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
            }
            IEnumerable<student> students = db.students;
            List<AutoCompletePOCO> AutoCompleteID = new List<AutoCompletePOCO>();
            foreach (student stud in students)
            {
                AutoCompletePOCO poco = new AutoCompletePOCO()
                {
                    value = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                    Label = stud.fname + " " + stud.lname + " (" + stud.studentid + ")",
                    Email = stud.email,
                    Role = "Student"
                };
                AutoCompleteID.Add(poco);
            }
            IEnumerable<employee> employees = db.employees;
            List<AutoCompletePOCO> EmployeeID = new List<AutoCompletePOCO>();
            foreach (employee emp in employees)
            {
                AutoCompletePOCO poco = new AutoCompletePOCO()
                {
                    value = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                    Label = emp.fname + " " + emp.lname + " (" + emp.employeeid + ")",
                    Email = emp.email,
                    Role = emp.role
                };
                AutoCompleteID.Add(poco);
                EmployeeID.Add(poco);
            }
            CreateAppointmentRequestModel model = new CreateAppointmentRequestModel() { _appointment = appointment, _campus = list, AttendeesAutoComplete = AutoCompleteID, EmployeeID = EmployeeID, startTime = appointment.starttime.ToShortTimeString(), endTime = appointment.endtime.ToShortTimeString() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CreateAppointmentRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<campu> campus = db.campus;
                    List<String> list = new List<String>();
                    foreach (campu camp in campus)
                    {
                        list.Add(camp.cname);
                    }
                    model._campus = list;
                    DateTime date1 = DateTime.Parse(model.startTime);
                    TimeSpan time1 = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                    DateTime date2 = DateTime.Parse(model.endTime);
                    TimeSpan time2 = new TimeSpan(date2.Hour, date2.Minute, date2.Second);
                    model._appointment.starttime = model._appointment.starttime.Add(time1);
                    model._appointment.endtime = model._appointment.endtime.Add(time2);
                    db.appointments.Attach(model._appointment);
                    db.ObjectStateManager.ChangeObjectState(model._appointment, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + model._appointment.appointmentid, "Calendar");
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                appointment appointment = db.appointments.Single(i => i.appointmentid == id);
                IEnumerable<Attendee> Attendees = db.Attendees.Where(a => a.appointmentid == id);

                foreach (Attendee attend in Attendees)
                {
                    db.Attendees.DeleteObject(attend);
                }

                db.appointments.DeleteObject(appointment);
                db.SaveChanges();

                return Json("200");
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

        private void ConfirmationEmail(employee employee, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("SheridanAdvisementSys@gmail.com");
            mail.To.Add(employee.email);
            mail.Subject = "Appointment Confirmation for " + employee.fname + " " + employee.lname;
            mail.Body = "You have successfully created an " + appointment.appointmenttype + " appointment at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ".";
            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SheridanAdvisementSys@gmail.com", "Sheridan123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        private void EmailAttendees(employee emp, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            employee chair = db.employees.Single(e => e.employeeid == appointment.employeeid);
            IEnumerable<Attendee> Attendees = db.Attendees.Where(attend => attend.appointmentid == appointment.appointmentid);
            String EmailTo = chair.email;

            foreach (Attendee Attendee in Attendees)
            {
                student student = db.students.SingleOrDefault(stud => stud.studentid == Attendee.attendee1);

                if (student != null)
                {
                    EmailTo += ", " + student.email;
                }
                else
                {
                    employee employee = db.employees.Single(e => e.employeeid == Attendee.attendee1);

                    EmailTo += ", " + employee.email;
                }
            }

            mail.From = new MailAddress("SheridanAdvisementSys@gmail.com");
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Invitation From " + emp.fname + " " + emp.lname;
            mail.Body = "You have been requested to attend an " + appointment.appointmenttype + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";
            if (appointment.description != null)
            {
                mail.Body += "\n\nDescription Included: \n" + appointment.description;
            }

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SheridanAdvisementSys@gmail.com", "Sheridan123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

    }
}
