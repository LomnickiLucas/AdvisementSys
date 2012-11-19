using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using AdvisementSys.Models.Request;
using System.Data;

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
            List<Events> model = new List<Events>();
            foreach (appointment appointment in appointments)
            {
                Events Events = new Events();
                Events.id = appointment.appointmentid;
                Events.title = appointment.subject;
                Events.allDay = false;
                Events.start = ConvertToUnixTimestamp(appointment.starttime).ToString();
                Events.end = ConvertToUnixTimestamp(appointment.endtime).ToString();
                Events.url = Url.Action("Details/" + appointment.appointmentid.ToString());

                model.Add(Events);
            }
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            appointment model = db.appointments.Single(i => i.appointmentid == id);
            return View(model);
        }

        public ActionResult Create()
        {
            appointment appointment = new appointment() { employeeid = User.Identity.Name, starttime = DateTime.Now, endtime = DateTime.Now.AddHours(.5) };
            IEnumerable<campu> campus = db.campus;
            List<String> list = new List<String>();
            foreach (campu camp in campus)
            {
                list.Add(camp.cname);
            }
            IEnumerable<student> students = db.students;
            List<String> StudentID = new List<string>();
            foreach (student stud in students)
            {
                StudentID.Add(stud.studentid);
            }
            IEnumerable<employee> employees = db.employees;
            List<String> EmployeeID = new List<string>();
            foreach (employee emp in employees)
            {
                EmployeeID.Add(emp.employeeid);
            }
            CreateAppointmentRequestModel model = new CreateAppointmentRequestModel() { _appointment = appointment, _campus = list, StudID = StudentID, EmployeeID = EmployeeID, startTime = DateTime.Now.ToShortTimeString(), endTime = DateTime.Now.AddHours(.5).ToShortTimeString() };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateAppointmentRequestModel model)
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
                    model._appointment.appointmentid = Guid.NewGuid();
                    DateTime date1 = DateTime.Parse(model.startTime);
                    TimeSpan time1 = new TimeSpan(date1.Hour, date1.Minute, date1.Second);
                    DateTime date2 = DateTime.Parse(model.endTime);
                    TimeSpan time2 = new TimeSpan(date2.Hour, date2.Minute, date2.Second);
                    model._appointment.starttime = model._appointment.starttime.Add(time1);
                    model._appointment.endtime = model._appointment.endtime.Add(time2);
                    db.appointments.AddObject(model._appointment);
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
            List<String> StudentID = new List<string>();
            foreach (student stud in students)
            {
                StudentID.Add(stud.studentid);
            }
            IEnumerable<employee> employees = db.employees;
            List<String> EmployeeID = new List<string>();
            foreach (employee emp in employees)
            {
                EmployeeID.Add(emp.employeeid);
            }
            CreateAppointmentRequestModel model = new CreateAppointmentRequestModel() { _appointment = appointment, _campus = list, StudID = StudentID, EmployeeID = EmployeeID, startTime = appointment.starttime.ToShortTimeString(), endTime = appointment.endtime.ToShortTimeString() };
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

    }
}
