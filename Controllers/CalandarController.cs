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
        private readonly String MAIL_ADDRESS = "SheridanAdvisementSys@gmail.com";
        private readonly String MAIL_SMTP = "smtp.gmail.com";
        private readonly String MAIL_PASS = "Sheridan123";

        private Entities db = new Entities();
        //
        // GET: /Calandar/

        /// <summary>
        /// populates Index view and includes all appointments for the receptionist.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// peforms postback for Index filters in order to create a less chaotic look.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// populates the details page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// populates the reate page
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
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
            String[] AppointmentType = new String[3] { "Advisement", "Personal", "Office" };

            CreateAppointmentRequestModel model = new CreateAppointmentRequestModel() { _appointment = appointment, _campus = list, AttendeesAutoComplete = AutoCompleteID, EmployeeID = EmployeeID, startTime = DateTime.Now.ToShortTimeString(), endTime = DateTime.Now.AddHours(.5).ToShortTimeString(), appoingmentType = AppointmentType, emailAll = true, repeatingType = new String[] { "Not Repeating", "Daily (Business Days)", "Weekly", "Bi-Weekly", "Monthly", "Yearly" } };
            if (id != null)
            {
                student student = db.students.Single(s => s.studentid == id);
                model.Attendees = student.fname + " " + student.lname + " (" + student.studentid + "), ";
            }
            return View(model);
        }

        /// <summary>
        /// creates appointments
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateAppointmentRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //deconstructs the string down to a collection of student IDs
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
                    String[] AppointmentType = new String[3] { "Advisement", "Personal", "Office" };
                    model._campus = list;
                    model.appoingmentType = AppointmentType;

                    DateTime date1 = DateTime.Parse(model.startTime);
                    TimeSpan time1 = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                    DateTime date2 = DateTime.Parse(model.endTime);
                    TimeSpan time2 = new TimeSpan(date2.Hour, date2.Minute, date2.Second);
                    model._appointment.starttime = model._appointment.starttime.Add(time1);
                    model._appointment.endtime = model._appointment.endtime.Add(time2);
                    //based on whether the appointment is repeating will use one fo the following sets of code to create the appoinment/series of appointments
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

        /// <summary>
        /// not complete still needs to be re-written
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
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

                if (student != null)
                {
                    program prog = db.programs.Single(p => p.programcode == student.programcode);

                    DetailAttendees.StudFaculty = prog.faculty;
                }

                Attendees.Add(DetailAttendees);
            }

            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
            }

            String[] AppointmentType = new String[3] { "Advisement", "Personal", "Office" };

            EditCalendarModel model = new EditCalendarModel() { appointment = appointment, Attendees = Attendees, chair = db.employees.Single(emp => emp.employeeid == appointment.employeeid), appoingmentType = AppointmentType, _campus = list, startTime = appointment.starttime.ToShortTimeString(), endTime = appointment.endtime.ToShortTimeString() };

            return View(model);
        }

        /// <summary>
        /// Still needs to be re written
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditCalendarModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime date1 = DateTime.Parse(model.startTime);
                    TimeSpan time1 = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                    DateTime date2 = DateTime.Parse(model.endTime);
                    TimeSpan time2 = new TimeSpan(date2.Hour, date2.Minute, date2.Second);
                    DateTime today1 = model.appointment.starttime;
                    today1 = today1.Add(time1);
                    DateTime today2 = model.appointment.endtime.Date;
                    today2 = today2.Add(time2);
                    model.appointment.starttime = today1;
                    model.appointment.endtime = today2;
                    db.appointments.Attach(model.appointment);
                    db.ObjectStateManager.ChangeObjectState(model.appointment, EntityState.Modified);
                    db.SaveChanges();

                    EditEmail(model.appointment);

                    return RedirectToAction("Details", "Calendar", new { id = model.appointment.appointmentid });
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        private void EditEmail(appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = "Your Appointment Has Been Edited";
            mail.Body = "Your " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " that is to take place at " + appointment.cname + " has been edited. If you would like to inquire further please view the changes within the system or contact " + chair.fname + " " + chair.lname + " regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// gets invoked via AJAX for deletion of appointments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// AJAX invokes this and it deletes appointment series
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// convert to a unix time stammp
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double ConvertToUnixTimestamp(DateTime value)
        {
            //create Timespan by subtracting the value provided from
            //the Unix Epoch
            TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

            //return the total seconds (which is a UNIX timestamp)
            return (double)span.TotalSeconds;
        }


        /// <summary>
        /// sends an email to all attendees to a series of appointments informing them that the appointments have been deleted
        /// </summary>
        /// <param name="appointments"></param>
        private void DeleteSeriesEmail(IEnumerable<appointment> appointments)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            IEnumerable<appointment> appoint = db.appointments.Where(a => a.repeating == appointments.FirstOrDefault().repeating).OrderBy(a => a.starttime);

            DateTime start = appoint.FirstOrDefault().starttime;
            DateTime end = appoint.OrderByDescending(a => a.starttime).FirstOrDefault().starttime;

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Series Deleted By " + chair.fname + " " + chair.lname;
            mail.Body = "Your " + appointments.First().appointmenttype.Trim() + " appointment series regarding " + appointments.First().subject + " at " + start + " to " + end
                + " that was to take place at " + appointments.First().cname + " has been deleted. If you would like to inquire further please contact " + chair.fname + " " + chair.lname + " regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// Sends an Email notification informing that an appointmnet has been deleted to all attendees of the appointment
        /// </summary>
        /// <param name="appointment"></param>
        private void DeleteEmail(appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Deleted By " + chair.fname + " " + chair.lname;
            mail.Body = "Your " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " that was to take place at " + appointment.cname + " has been deleted. If you would like to inquire further please contact " + chair.fname + " " + chair.lname + " regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// Sends an email to the creator of the appointment confirming that the appointment has been created
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="appointment"></param>
        private void ConfirmationEmail(employee employee, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(employee.email);
            mail.Subject = "Appointment Confirmation for " + employee.fname + " " + employee.lname;
            mail.Body = "You have successfully created an " + appointment.appointmenttype.Trim() + " appointment at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ".";
            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// sends an email to the creater of the appointment confirming that the the series appointments have been created. 
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="appointment"></param>
        private void ConfirmationEmailSeries(employee employee, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            IEnumerable<appointment> appointments = db.appointments.Where(a => a.repeating == appointment.repeating).OrderBy(a => a.starttime);

            DateTime start = appointments.First().starttime;
            DateTime end = appointments.OrderByDescending(a => a.starttime).First().starttime;

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(employee.email);
            mail.Subject = "Appointment Confirmation for " + employee.fname + " " + employee.lname;
            mail.Body = "You have successfully created an series of " + appointment.appointmenttype.Trim() + " appointments starting on " + start + " to " + end
                + " and the appointment will take place at " + appointment.cname + ".";
            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// sends an email to the attendees informing that they have been invited to an appointment
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="appointment"></param>
        private void EmailAttendees(employee emp, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

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

            mail.From = new MailAddress(MAIL_ADDRESS);
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
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// sends an email to the attendees informing that they have been invited to an series of appointments 
        /// </summary>
        /// <param name="emp"></param>
        /// <param name="appointment"></param>
        private void EmailAttendeesSeries(employee emp, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

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

            mail.From = new MailAddress(MAIL_ADDRESS);
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
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// Is called via AJAX an confirms an attendee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentID"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Confirm")]
        public ActionResult Confirm(String id, Guid appointmentID)
        {
            Attendee model = db.Attendees.SingleOrDefault(a => a.attendee1 == id && a.appointmentid.Equals(appointmentID));

            model.confirmed = true;

            db.Attendees.Attach(model);
            db.ObjectStateManager.ChangeObjectState(model, EntityState.Modified);
            db.SaveChanges();

            appointment appointment = db.appointments.SingleOrDefault(a => a.appointmentid.Equals(appointmentID));

            EmailAttendeesConfirmation(id, appointment);

            return Json("200");
        }

        /// <summary>
        /// emails all attendees of an appointment informing them that an attendee has confirmed the appointment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointment"></param>
        private void EmailAttendeesConfirmation(String id, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            String fname = "";
            String lname = "";

            employee i = db.employees.SingleOrDefault(e => e.employeeid == id);

            if (i != null)
            {
                fname = i.fname;
                lname = i.lname;
            }
            else
            {
                student s = db.students.SingleOrDefault(c => c.studentid == id);

                fname = s.fname;
                lname = s.lname;
            }

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Confirmation Notice From " + fname + " " + lname;
            mail.Body = fname + " " + lname +" has confirmed that he/she will be attending the " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// Is called via AJAX to confirm an attendee of an appointment for a series of appoinhtments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentID"></param>
        /// <param name="repeatingID"></param>
        /// <returns></returns>
        [HttpPost, ActionName("ConfirmSeries")]
        public ActionResult ConfirmSeries(String id, Guid appointmentID, Guid repeatingID)
        {
            Attendee model;
            IEnumerable<appointment> appointments = db.appointments.Where(i => i.repeating == repeatingID);

            foreach (appointment appointment in appointments)
            {
                IEnumerable<Attendee> Attendees = db.Attendees.Where(a => a.appointmentid == appointment.appointmentid);

                foreach (Attendee attend in Attendees)
                {
                    if (attend.attendee1 == id)
                    {
                        model = attend;

                        model.confirmed = true;

                        db.Attendees.Attach(model);
                        db.ObjectStateManager.ChangeObjectState(model, EntityState.Modified);
                    }
                }       
            }

            db.SaveChanges();

            appointment appoint = db.appointments.SingleOrDefault(a => a.appointmentid.Equals(appointmentID));

            EmailAttendeesConfirmationSeries(id, appoint);

            return Json("200");
        }

        /// <summary>
        /// emails all attendees informing them that an attendee has confirmed a series of appointments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointment"></param>
        private void EmailAttendeesConfirmationSeries(String id, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            String fname = "";
            String lname = "";

            employee i = db.employees.SingleOrDefault(e => e.employeeid == id);

            if (i != null)
            {
                fname = i.fname;
                lname = i.lname;
            }
            else
            {
                student s = db.students.SingleOrDefault(c => c.studentid == id);

                fname = s.fname;
                lname = s.lname;
            }

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = "Appointment Series Confirmation Notice From " + fname + " " + lname;
            mail.Body = fname + " " + lname + " has confirmed that he/she will be attending the series of " + appointment.appointmenttype.Trim() + " appointments regarding " + appointment.subject + " at " + start + " to " + end
                + " and the appointments will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// Is called via AJAX to set an attendee as declinging an appointment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentID"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Decline")]
        public ActionResult Decline(String id, Guid appointmentID, string reason)
        {
            Attendee model = db.Attendees.SingleOrDefault(a => a.attendee1 == id && a.appointmentid.Equals(appointmentID));

            model.confirmed = false;
            model.reason = reason;

            db.Attendees.Attach(model);
            db.ObjectStateManager.ChangeObjectState(model, EntityState.Modified);
            db.SaveChanges();

            appointment appointment = db.appointments.SingleOrDefault(a => a.appointmentid.Equals(appointmentID));

            EmailAttendeesDecline(id, appointment);

            return Json("200");
        }

        /// <summary>
        /// emails all attendees of an appointment infoirming them that an attendee has decling an appointment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointment"></param>
        private void EmailAttendeesDecline(String id, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            String fname = "";
            String lname = "";

            employee i = db.employees.SingleOrDefault(e => e.employeeid == id);

            if (i != null)
            {
                fname = i.fname;
                lname = i.lname;
            }
            else
            {
                student s = db.students.SingleOrDefault(c => c.studentid == id);

                fname = s.fname;
                lname = s.lname;
            }

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = fname + " " + lname + " has Declined an Appointment";
            mail.Body = fname + " " + lname + " has declined the " + appointment.appointmenttype.Trim() + " appointment regarding " + appointment.subject + " at " + appointment.starttime.ToString() + " to " + appointment.endtime.ToString()
                + " and the appointment will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        /// <summary>
        /// is called VIA Ajax and declines an attendee from an appointment series
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointmentID"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost, ActionName("DeclineSeries")]
        public ActionResult DeclineSeries(String id, Guid appointmentID, string reason)
        {
            Attendee model;
            appointment appointID = db.appointments.SingleOrDefault(a => a.appointmentid == appointmentID);
            IEnumerable<appointment> appointments = db.appointments.Where(i => i.repeating == appointID.repeating);

            foreach (appointment appointment in appointments)
            {
                IEnumerable<Attendee> Attendees = db.Attendees.Where(a => a.appointmentid == appointment.appointmentid);

                foreach (Attendee attend in Attendees)
                {
                    if (attend.attendee1 == id)
                    {
                        model = attend;

                        model.confirmed = false;
                        model.reason = reason;

                        db.Attendees.Attach(model);
                        db.ObjectStateManager.ChangeObjectState(model, EntityState.Modified);
                    }
                }
            }

            db.SaveChanges();

            appointment appoint = db.appointments.SingleOrDefault(a => a.appointmentid.Equals(appointmentID));

            EmailAttendeesDeclineSeries(id, appoint);

            return Json("200");
        }

        /// <summary>
        /// Emails all attenddees that an attendee has declined an a series of appointments
        /// </summary>
        /// <param name="id"></param>
        /// <param name="appointment"></param>
        private void EmailAttendeesDeclineSeries(String id, appointment appointment)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient(MAIL_SMTP);

            String fname = "";
            String lname = "";

            employee i = db.employees.SingleOrDefault(e => e.employeeid == id);

            if (i != null)
            {
                fname = i.fname;
                lname = i.lname;
            }
            else
            {
                student s = db.students.SingleOrDefault(c => c.studentid == id);

                fname = s.fname;
                lname = s.lname;
            }

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

            mail.From = new MailAddress(MAIL_ADDRESS);
            mail.To.Add(EmailTo);
            mail.Subject = fname + " " + lname + " has Declined the Appointment Series";
            mail.Body = fname + " " + lname + " has declined the " + appointment.appointmenttype.Trim() + " appointment series regarding " + appointment.subject + " at " + start + " to " + end
                + " and the appointment will take place at " + appointment.cname + ". The chair for the appointment will be " + chair.fname + " " + chair.lname + ", please contact him/her regarding any further details at "
                + chair.email + " or " + chair.phonenum + ".";

            mail.Body += "\n\nThis is an automated message please to do not respond to this email.";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(MAIL_ADDRESS, MAIL_PASS);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
