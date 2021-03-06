﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using AdvisementSys.Models.Request;
using System.Text;

namespace AdvisementSys.Controllers
{
    public class IssueController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Issue/
        /// <summary>
        /// Ensures tha field have the proper information
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            try
            {
                var issues = db.issues.Include("student");
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
                var program = db.programs;
                var employee = db.employees;
                List<String> employees = new List<String>();
                employees.Add("Any");
                foreach (employee emp in employee)
                {
                    employees.Add(emp.employeeid.Trim() + " - " + emp.fname.Trim() + " " + emp.lname.Trim());
                }
                List<catagory> catagories = new List<catagory>();
                catagory cat = new catagory() { catagory1 = "Any" };
                catagories.Add(cat);
                catagories.AddRange(db.catagories);
                List<String> collection = new List<String>();
                collection.Add("Any");
                foreach (program prog in program)
                {
                    collection.Add(prog.programcode.ToString().Trim() + " - " + prog.programname.ToString().Trim());
                }
                IndexIssueRequestModel Model = new IndexIssueRequestModel() { _issue = _issues.OrderByDescending(i => i.Status), _employee = employees, _catagories = catagories, _date1 = issues.OrderByDescending(i => i.date).First().date, _date2 = issues.OrderBy(i => i.date).First().date, _programcode = collection };
                return View(Model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        /// <summary>
        /// goes the througgh the search paramaters and returns just the results that fullfil the criteria
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(IndexIssueRequestModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var issues = db.issues.Include("student");
                    var program = db.programs;
                    var employee = db.employees;
                    List<String> employees = new List<String>();
                    employees.Add("Any");
                    foreach (employee emp in employee)
                    {
                        employees.Add(emp.employeeid.Trim() + " - " + emp.fname.Trim() + " " + emp.lname.Trim());
                    }
                    List<catagory> catagories = new List<catagory>();
                    catagory cat = new catagory() { catagory1 = "Any" };
                    catagories.Add(cat);
                    catagories.AddRange(db.catagories);
                    List<String> collection = new List<String>();
                    collection.Add("Any");
                    foreach (program prog in program)
                    {
                        collection.Add(prog.programcode.ToString().Trim() + " - " + prog.programname.ToString().Trim());
                    }
                    IEnumerable<issue> _issues = issues;
                    if (Model._name != null)
                        _issues = _issues.Where(i => i.issuename.Trim().ToUpper().Contains(Model._name.Trim().ToUpper()));
                    if (Model._employeeid != "Any")
                    {
                        StringBuilder sb = new StringBuilder(Model._employeeid.Trim().ToUpper());
                        sb.Remove(9, sb.Length - 9);
                        _issues = _issues.Where(i => i.employeeid.Trim().ToUpper().Contains(sb.ToString()));
                    }
                    if (Model._status != "Any")
                        _issues = _issues.Where(i => i.status.Trim().ToUpper().Contains(Model._status.Trim().ToUpper()));
                    if (Model._urgency != "Any")
                        _issues = _issues.Where(i => i.urgency.Trim().ToUpper().Contains(Model._urgency.Trim().ToUpper()));
                    if (Model._category != "Any")
                        _issues = _issues.Where(i => i.catagory.Trim().ToUpper().Contains(Model._category.Trim().ToUpper()));
                    if (Model._selectedpcode != "Any")
                    {
                        StringBuilder sb = new StringBuilder(Model._selectedpcode.Trim().ToUpper());
                        sb.Remove(5, sb.Length - 5);
                        _issues = _issues.Where(i => i.student.programcode.Trim().ToUpper().Contains(sb.ToString()));
                    }
                    if ((Model._date1 != null) && (Model._date2 != null))
                    {
                        if (Model._date1 > Model._date2)
                        {
                            _issues = _issues.Where(i => i.date >= Model._date2 && i.date <= Model._date1);
                        }
                        else if (Model._date2 > Model._date1)
                        {
                            _issues = _issues.Where(i => i.date <= Model._date2 && i.date >= Model._date1);
                        }
                        else
                        {
                            _issues = _issues.Where(i => i.date == Model._date1);
                        }
                    }
                    List<IssuesPOCO> _issue = new List<IssuesPOCO>();
                    foreach (issue i in _issues.OrderByDescending(e => e.date))
                    {
                        IssuesPOCO temp = new IssuesPOCO();
                        temp.IssueID = i.issueid;
                        temp.Name = i.issuename;
                        temp.Date = ConvertToUnixTimestamp(i.date).ToString();
                        temp.Status = i.status;
                        temp.Urgency = i.urgency;

                        _issue.Add(temp);
                    }
                    Model._issue = _issue;
                    Model._employee = employees;
                    Model._catagories = catagories;
                    Model._programcode = collection;


                    return View(Model);
                }
                return View(Model);
            }
            catch (Exception ex)
            {
                return View(Model);
            }
        }

        //
        // GET: /Issue/Details/5
        /// <summary>
        /// Gets a single Issue and finds all forms that are associated with that issue
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ViewResult Details(Guid id)
        {
            issue issue = db.issues.Include("employee").Single(i => i.issueid == id);
            student student = db.students.Single(i => i.studentid == issue.studentid);
            program program = db.programs.Single(i => i.programcode == issue.student.programcode);
            String emp = issue.employeeid.ToString() + " - " + issue.employee.fname.ToString() + " " + issue.employee.lname.ToString();
            DetailsIssueRequestModel model = new DetailsIssueRequestModel() { _issue = issue, _student = student, _program = program, _EmployeeID = emp, _date = DateTime.Now, _note = db.notes.Include("employee").Where(note => note.issueid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name) };
            List<FormsPOCO> Forms = new List<FormsPOCO>();
            IEnumerable<applicationForReadmission> applicationForReadmission = db.applicationForReadmissions.Where(i => i.issueid == issue.issueid);
            foreach (applicationForReadmission form in applicationForReadmission)
            {
                FormsPOCO _Forms = new FormsPOCO();
                _Forms.Date = ConvertToUnixTimestamp(form.date).ToString();
                _Forms.FormType = "Application For Readmission";
                _Forms.Status = form.status;
                _Forms.Controller = "Readmission";
                _Forms.FormID = form.readmissionid;

                Forms.Add(_Forms);
            }
            IEnumerable<applicationForTermOrCompleteProgramWithdrawal> applicationForTermOrCompleteProgramWithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Where(i => i.issueid == issue.issueid);
            foreach (applicationForTermOrCompleteProgramWithdrawal form in applicationForTermOrCompleteProgramWithdrawal)
            {
                FormsPOCO _Forms = new FormsPOCO();
                _Forms.Date = ConvertToUnixTimestamp(form.date).ToString();
                _Forms.FormType = "Application For Term Or Complete Program Withdrawal";
                _Forms.Status = form.status;
                _Forms.Controller = "ProgramWithdrawal";
                _Forms.FormID = form.withdrawid;

                Forms.Add(_Forms);
            }
            IEnumerable<part_timeAnd_orAdditionalCourseRegistrationForm> part_timeAnd_orAdditionalCourseRegistrationForm = db.part_timeAnd_orAdditionalCourseRegistrationForm.Where(i => i.issueid == issue.issueid);
            foreach (part_timeAnd_orAdditionalCourseRegistrationForm form in part_timeAnd_orAdditionalCourseRegistrationForm)
            {
                FormsPOCO _Forms = new FormsPOCO();
                _Forms.Date = ConvertToUnixTimestamp(form.date).ToString();
                _Forms.FormType = "Part Time And/or Additional Course Registration Form";
                _Forms.Status = form.status;
                _Forms.Controller = "CourseRegistration";
                _Forms.FormID = form.registrationid;

                Forms.Add(_Forms);
            }
            IEnumerable<probationaryContractPlan> probationaryContractPlan = db.probationaryContractPlans.Where(i => i.issueid == issue.issueid);
            foreach (probationaryContractPlan form in probationaryContractPlan)
            {
                FormsPOCO _Forms = new FormsPOCO();
                _Forms.Date = ConvertToUnixTimestamp(form.date).ToString();
                _Forms.FormType = "Probationary Contract Plan";
                _Forms.Status = form.status;
                _Forms.Controller = "ProbationaryContract";
                _Forms.FormID = form.advisementid;

                Forms.Add(_Forms);
            }
            IEnumerable<requestForLateEnrolment> requestForLateEnrolment = db.requestForLateEnrolments.Where(i => i.issueid == issue.issueid);
            foreach (requestForLateEnrolment form in requestForLateEnrolment)
            {
                FormsPOCO _Forms = new FormsPOCO();
                _Forms.Date = ConvertToUnixTimestamp(form.date).ToString();
                _Forms.FormType = "Request For Late Enrollment Form";
                _Forms.Status = form.status;
                _Forms.Controller = "LateEnrollment";
                _Forms.FormID = form.enrolementid;

                Forms.Add(_Forms);
            }

            model._Forms = Forms.OrderByDescending(f => f.Status);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(DetailsIssueRequestModel _model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Guid? id = _model._newNote.issueid;
                    _model._newNote.noteid = Guid.NewGuid();
                    _model._newNote.employeeid = User.Identity.Name;
                    _model._newNote.dates = DateTime.Now;
                    db.notes.AddObject(_model._newNote);
                    db.SaveChanges();

                    return RedirectToAction("Details", new { id = id });
                }

                return View(_model);
            }
            catch (Exception ex)
            {
                return View(_model);
            }
        }

        //
        // GET: /Issue/Create
        /// <summary>
        /// Gets all required Data for the create view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(String id)
        {
            student student = db.students.Single(i => i.studentid == id);
            issue issue = new issue() { studentid = id, date = DateTime.Now };
            program program = db.programs.Single(i => i.programcode == student.programcode);
            IEnumerable<catagory> catagory = db.catagories;
            IEnumerable<employee> employee = db.employees;
            List<String> employees = new List<String>();
            employees.Add("Please Select an Employee");
            foreach (employee emp in employee)
            {
                employees.Add(emp.employeeid.Trim() + " - " + emp.fname.Trim() + " " + emp.lname.Trim());
            }
            CreateIssueRequestModel model = new CreateIssueRequestModel() { _issue = issue, _student = student, _program = program, _catagory = catagory, _employee = employees };
            return View(model);
        }

        //
        // POST: /Issue/Create
        /// <summary>
        /// Submits the create request
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateIssueRequestModel model, String id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StringBuilder sb = new StringBuilder(model._issue.employeeid.Trim().ToUpper());
                    sb.Remove(9, sb.Length - 9);
                    model._issue.employeeid = sb.ToString();
                    model._issue.issueid = Guid.NewGuid();
                    model._issue.studentid = id;
                    db.issues.AddObject(model._issue);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + id, "Student");
                }
            }
            catch (Exception ex)
            {
                ViewBag.studentid = new SelectList(db.students, "studentid", "fname", model._issue.studentid);
                return View(model);
            }
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", model._issue.studentid);
            return View(model);
        }

        //
        // GET: /Issue/Edit/5
        /// <summary>
        /// Gets all data required for the Edit View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            issue issue = db.issues.Include("student").Single(i => i.issueid == id);
            student student = db.students.Single(i => i.studentid == issue.studentid);
            program program = db.programs.Single(i => i.programcode == student.programcode);
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", issue.studentid);
            IEnumerable<catagory> catagory = db.catagories;
            IEnumerable<employee> employee = db.employees;
            List<String> employees = new List<String>();
            employees.Add("Please Select an Employee");
            foreach (employee emp in employee)
            {
                employees.Add(emp.employeeid.Trim() + " - " + emp.fname.Trim() + " " + emp.lname.Trim());
            }
            EditIssueRequestModel model = new EditIssueRequestModel() { _issue = issue, _program = program, _student = student, _catagory = catagory, _employee = employees };
            return View(model);
        }

        //
        // POST: /Issue/Edit/5
        /// <summary>
        /// Submits the Edit View
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditIssueRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    StringBuilder sb = new StringBuilder(model._issue.employeeid.Trim().ToUpper());
                    sb.Remove(9, sb.Length - 9);
                    model._issue.employeeid = sb.ToString();
                    db.issues.Attach(model._issue);
                    db.ObjectStateManager.ChangeObjectState(model._issue, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + model._issue.issueid);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
        }

        //
        // GET: /Issue/Delete/5
        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            issue issue = db.issues.Single(i => i.issueid == id);
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5
        /// <summary>
        /// Not for Use (Be aware of castcading effects and Soft deletes would need to be done if delete is to be required)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            issue issue = db.issues.Single(i => i.issueid == id);
            db.issues.DeleteObject(issue);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditNote(Guid id)
        {
            note model = db.notes.Include("employee").Single(i => i.noteid == id);
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditNote(note model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.notes.Attach(model);
                    db.ObjectStateManager.ChangeObjectState(model, EntityState.Modified);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + model.issueid);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }
            return View(model);
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