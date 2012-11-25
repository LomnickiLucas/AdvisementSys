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
using System.Web.Security;

namespace AdvisementSys.Controllers
{
    public class CalendarController : Controller
    {
        private Entities db = new Entities();
        //
        // GET: /Calandar/

        public ActionResult Index()
        {
            IEnumerable<appointment> appoint;
            List<appointment> appointments;
            if (User.IsInRole("Receptionist"))
            {
                appoint = db.appointments;
                appointments = appoint.ToList();
            }
            else
            {
                appoint = db.appointments.Where(i => i.employeeid == User.Identity.Name);
                appointments = appoint.ToList();
                IEnumerable<Attendee> Attendee = db.Attendees.Where(i => i.attendee1 == User.Identity.Name && (i.confirmed == true || i.confirmed == null));
                foreach (Attendee Attend in Attendee)
                {
                    appointments.Add(db.appointments.Single(i => i.appointmentid == Attend.appointmentid));
                }
            }
            List<Events> events = new List<Events>();
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

                events.Add(Events);
            }

            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            list.Add("All");
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
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
                EmployeeID.Add(poco);
            }

            IndexCalendarModel model = new IndexCalendarModel() { Events = events, cNames = list, AutoCom = EmployeeID };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(IndexCalendarModel model)
        {
            IEnumerable<appointment> appoint;
            List<appointment> appointments;
            if (model.cName.Equals("All"))
            {
                if (User.IsInRole("Receptionist"))
                {
                    appoint = db.appointments;
                }
                else
                {
                    appoint = db.appointments.Where(i => i.employeeid == User.Identity.Name);
                }
            }
            else
            {
                if (User.IsInRole("Receptionist"))
                {
                    appoint = db.appointments.Where(i => i.cname == model.cName);
                }
                else
                {
                    appoint = db.appointments.Where(i => i.employeeid == User.Identity.Name && i.cname == model.cName);
                }
            }

            if (model.advisorID != null)
            {
                model.advisorID = model.advisorID.Remove(0, model.advisorID.Length - 10);
                model.advisorID = model.advisorID.Remove(model.advisorID.Length - 1);

                appoint = appoint.Where(a => a.employeeid == model.advisorID);
                appointments = appoint.ToList();
                IEnumerable<Attendee> Attendee = db.Attendees.Where(i => i.attendee1 == model.advisorID && (i.confirmed == true || i.confirmed == null));
                foreach (Attendee Attend in Attendee)
                {
                    if (model.cName.Equals("All"))
                    {
                        appointments.Add(db.appointments.Single(i => i.appointmentid == Attend.appointmentid));
                    }
                    else
                    {
                        appointment app = db.appointments.Single(i => i.appointmentid == Attend.appointmentid);
                        if (app.cname == model.cName)
                            appointments.Add(app);
                    }
                }
            }
            else
            {
                appointments = appoint.ToList();
            }
            if (!User.IsInRole("Receptionist"))
            {
                IEnumerable<Attendee> Attendee = db.Attendees.Where(i => i.attendee1 == User.Identity.Name && (i.confirmed == true || i.confirmed == null));
                foreach (Attendee Attend in Attendee)
                {
                    if (model.cName.Equals("All"))
                    {
                        appointments.Add(db.appointments.Single(i => i.appointmentid == Attend.appointmentid));
                    }
                    else
                    {
                        appointment app = db.appointments.Single(i => i.appointmentid == Attend.appointmentid);
                        if (app.cname == model.cName)
                            appointments.Add(app);
                    }
                }
            }

            List<Events> events = new List<Events>();
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

                events.Add(Events);
            }

            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            list.Add("All");
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
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
                EmployeeID.Add(poco);
            }

            model.cNames = list;
            model.Events = events;
            model.AutoCom = EmployeeID;
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

                if(student != null)
                {
                    program prog = db.programs.Single(p => p.programcode == student.programcode);

                    DetailAttendees.StudFaculty = prog.faculty;
                }

                Attendees.Add(DetailAttendees);
            }

            DetailsCalendarModel model = new DetailsCalendarModel() { appointment = appointment, Attendees = Attendees, chair = db.employees.Single(emp => emp.employeeid == appointment.employeeid) };

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

                                student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                if (stud != null)
                                {
                                    attendee.confirmed = true;
                                }

                                if (!attendee.attendee1.Equals(model._appointment.employeeid))
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

                                        student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                        if (stud != null)
                                        {
                                            attendee.confirmed = true;
                                        }

                                        if (!attendee.attendee1.Equals(model._appointment.employeeid))
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

                                        student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                        if (stud != null)
                                        {
                                            attendee.confirmed = true;
                                        }

                                        if (!attendee.attendee1.Equals(model._appointment.employeeid))
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

                                    student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                    if (stud != null)
                                    {
                                        attendee.confirmed = true;
                                    }

                                    if (!attendee.attendee1.Equals(model._appointment.employeeid))
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

                                    student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                    if (stud != null)
                                    {
                                        attendee.confirmed = true;
                                    }

                                    if (!attendee.attendee1.Equals(model._appointment.employeeid))
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

                                    student stud = db.students.SingleOrDefault(s => s.studentid == id);

                                    if (stud != null)
                                    {
                                        attendee.confirmed = true;
                                    }

                                    if (!attendee.attendee1.Equals(model._appointment.employeeid))
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
                    if (model.repeating.Trim().Equals("Not Repeating"))
                    {
                        ConfirmationEmail(creator, model._appointment);
                    }
                    else
                    {
                        ConfirmationEmailSeries(creator, model._appointment);
                    }

                    if (model.emailAll)
                    {
                        if (model.repeating.Trim().Equals("Not Repeating"))
                        {
                            EmailAttendees(creator, model._appointment);
                        }
                        else
                        {
                            EmailAttendeesSeries(creator, model._appointment);
                        }
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

                DeleteEmail(appointment);

                db.appointments.DeleteObject(appointment);

                return Json("200");
            }
            catch (Exception ex)
            {
                return View();
            }
            finally 
            {
                db.SaveChanges();
            }
        }

        [HttpPost, ActionName("DeleteSeries")]
        public ActionResult DeleteSeriesConfirmed(Guid id)
        {
            try
            {
                IEnumerable<appointment> appointments = db.appointments.Where(i => i.repeating == id);

                foreach (appointment appointment in appointments)
                {
                    IEnumerable<Attendee> Attendees = db.Attendees.Where(a => a.appointmentid == appointment.appointmentid);

                    foreach (Attendee attend in Attendees)
                    {
                        db.Attendees.DeleteObject(attend);
                    }

                    db.appointments.DeleteObject(appointment);
                }

                DeleteSeriesEmail(appointments);

                return Json("200");
            }
            catch (Exception ex)
            {
                return View();
            }
            finally
            {
                db.SaveChanges();
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

        private void DeleteSeriesEmail(IEnumerable<appointment> appointments)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            IEnumerable<appointment> appoint = db.appointments.Where(a => a.repeating == appointments.First().repeating).OrderBy(a => a.starttime);

            DateTime start = appoint.First().starttime;
            DateTime end = appoint.OrderByDescending(a => a.starttime).First().starttime;

            employee chair = db.employees.Single(e => e.employeeid == appointments.FirstOrDefault().employeeid);
            String EmailTo = chair.email;

            foreach (appointment appointment in appointments)
            {
                IEnumerable<Attendee> Attendees = db.Attendees.Where(attend => attend.appointmentid == appointment.appointmentid);
                
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
            }

            mail.From = new MailAddress("SheridanAdvisementSys@gmail.com");
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Series Deleted By " + chair.fname + " " + chair.lname;
            mail.Body = "Your " + appointments.First().appointmenttype.Trim() + " appointment series regarding " + appointments.First().subject + " at " + start + " to " + end
                + " that was to take place at " + appointments.First().cname + " has been deleted. If you would like to inquire further please contact " + chair.fname + " " + chair.lname + " regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SheridanAdvisementSys@gmail.com", "Sheridan123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        private void DeleteEmail(appointment appointment)
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
            mail.Subject = "Appointment Deleted By " + chair.fname + " " + chair.lname;
            mail.Body = "Your " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " that was to take place at " + appointment.cname + " has been deleted. If you would like to inquire further please contact " + chair.fname + " " + chair.lname + " regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SheridanAdvisementSys@gmail.com", "Sheridan123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        private void ConfirmationEmail(employee employee, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress("SheridanAdvisementSys@gmail.com");
            mail.To.Add(employee.email);
            mail.Subject = "Appointment Confirmation for " + employee.fname + " " + employee.lname;
            mail.Body = "You have successfully created an " + appointment.appointmenttype.Trim() + " appointment at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ".";
            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("SheridanAdvisementSys@gmail.com", "Sheridan123");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        private void ConfirmationEmailSeries(employee employee, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            IEnumerable<appointment> appointments = db.appointments.Where(a => a.repeating == appointment.repeating).OrderBy(a => a.starttime);

            DateTime start = appointments.First().starttime;
            DateTime end = appointments.OrderByDescending(a => a.starttime).First().starttime;

            mail.From = new MailAddress("SheridanAdvisementSys@gmail.com");
            mail.To.Add(employee.email);
            mail.Subject = "Appointment Confirmation for " + employee.fname + " " + employee.lname;
            mail.Body = "You have successfully created an series of " + appointment.appointmenttype.Trim() + " appointments starting on " + start + " to " + end
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
            mail.Body = "You have been requested to attend an " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
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

        private void EmailAttendeesSeries(employee emp, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            IEnumerable<appointment> appointments = db.appointments.Where(a => a.repeating == appointment.repeating).OrderBy(a => a.starttime);

            DateTime start = appointments.First().starttime;
            DateTime end = appointments.OrderByDescending(a => a.starttime).First().starttime;

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
            mail.Body = "You have been requested to attend an " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " starting on " + start + " to " + end
                + " and the appointments will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
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
