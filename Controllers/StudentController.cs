using System;
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
    public class StudentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Student/
        /// <summary>
        /// Ensure the model has the proper object/information populated into it.
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            try
            {
                IEnumerable<campu> campus = db.campus;
                List<String> campusNames = new List<string>();
                campusNames.Add("Any");
                foreach (campu camp in campus)
                {
                    campusNames.Add(camp.cname);
                }
                var program = db.programs;
                List<String> collection = new List<String>();
                collection.Add("Any");
                foreach (program pcode in program)
                {
                    collection.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
                }
                IEnumerable<student> students = db.students.Include("campu").Include("program");
                students = students.Where(stud => stud.enrolled.Equals(true));
                List<StudentPOCO> student = new List<StudentPOCO>();
                foreach (student s in students)
                {
                    StudentPOCO temp = new StudentPOCO();
                    temp.StudentID = s.studentid;
                    temp.FName = s.fname;
                    temp.LName = s.lname;
                    temp.PhoneNum = s.phonenum;
                    temp.Prob = s.acadprobation;
                    temp.Prog = s.programcode + "-" + s.program.programname; ;
                    temp.FT = s.fulltimestatus;
                    temp.Campus = s.campus;
                    temp.Email = s.email;
                    temp.Enr = s.enrolled;

                    student.Add(temp);
                }

                IEnumerable<student> studentz = db.students.Include("campu").Include("program");
                List<String> StudID = new List<string>();
                foreach (student stud in studentz)
                {
                    StudID.Add(stud.studentid);
                }

                IndexStudentRequestModel model = new IndexStudentRequestModel() { _student = student, enrolled = true, programs = collection, _campus = campusNames, StudID = StudID };
                return View(model);
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //
        // POST: /Student
        /// <summary>
        /// Grabs all the results then uses the search paramaters to narrow down the results. (I know it could be better Issue Index is improved and in the future it will be even better)
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(IndexStudentRequestModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IEnumerable<campu> campus = db.campus;
                    List<String> campusNames = new List<string>();
                    campusNames.Add("Any");
                    foreach (campu camp in campus)
                    {
                        campusNames.Add(camp.cname);
                    }
                    model._campus = campusNames;
                    var program = db.programs;
                    List<String> collection = new List<String>();
                    collection.Add("Any");
                    foreach (program pcode in program)
                    {
                        collection.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
                    }
                    model.programs = collection;
                    IEnumerable<student> studentz = db.students.Include("campu").Include("program");
                    if (model.studentID != null)
                        studentz = studentz.Where(stud => stud.studentid.Trim().ToUpper().Contains(model.studentID.Trim().ToUpper()));
                    if (model.fName != null)
                        studentz = studentz.Where(stud => stud.fname.Trim().ToUpper().Contains(model.fName.Trim().ToUpper()));
                    if (model.lName != null)
                        studentz = studentz.Where(stud => stud.lname.Trim().ToUpper().Contains(model.lName.Trim().ToUpper()));
                    if (!model.programCode.Equals("Any"))
                    {
                        StringBuilder sb = new StringBuilder(model.programCode.Trim().ToUpper());
                        sb.Remove(5, sb.Length - 5);
                        studentz = studentz.Where(stud => stud.programcode.Trim().ToUpper().Contains(sb.ToString()));
                    }
                    if (!model.campus.Equals("Any"))
                    {
                        studentz = studentz.Where(stud => stud.campus.Trim().ToUpper().Contains(model.campus.Trim().ToUpper()));
                    }
                    if (model.email != null)
                        studentz = studentz.Where(stud => stud.email.Trim().ToUpper().Contains(model.email.Trim().ToUpper()));
                    if (model.acadprobation == true)
                    {
                        studentz = studentz.Where(stud => stud.acadprobation.Equals(true));
                    }
                    if (model.fulltimestatus == true)
                    {
                        studentz = studentz.Where(stud => stud.fulltimestatus.Equals(true));
                    }
                    if (model.enrolled == true)
                    {
                        studentz = studentz.Where(stud => stud.enrolled.Equals(true));
                    }
                    List<StudentPOCO> student = new List<StudentPOCO>();
                    foreach (student s in studentz)
                    {
                        StudentPOCO temp = new StudentPOCO();
                        temp.StudentID = s.studentid;
                        temp.FName = s.fname;
                        temp.LName = s.lname;
                        temp.PhoneNum = s.phonenum;
                        temp.Prob = s.acadprobation;
                        temp.Prog = s.programcode + "-" + s.program.programname;
                        temp.FT = s.fulltimestatus;
                        temp.Campus = s.campus;
                        temp.Email = s.email;
                        temp.Enr = s.enrolled;

                        student.Add(temp);
                    }
                    model._student = student;
                    IEnumerable<student> students = db.students.Include("campu").Include("program");
                    List<String> StudID = new List<string>();
                    foreach (student stud in students)
                    {
                        StudID.Add(stud.studentid);
                    }
                    model.StudID = StudID;  
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }

        //
        // GET: /Student/Details/5
        /// <summary>
        /// Grabs the object by ID and pushes the required objects in the Model (These are the worse comments I know but Im to tired)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(string id)
        {
            try
            {
                if (id != null)
                {
                    IEnumerable<issue> _issue = db.issues.Include("student").Where(i => i.studentid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == id);
                    List<IssuesPOCO> issue = new List<IssuesPOCO>();
                    foreach (issue i in _issue.OrderByDescending(e => e.date))
                    {
                        IssuesPOCO temp = new IssuesPOCO();
                        temp.IssueID = i.issueid;
                        temp.Name = i.issuename;
                        temp.Date = ConvertToUnixTimestamp(i.date).ToString();
                        temp.Status = i.status;
                        temp.Urgency = i.urgency;

                        issue.Add(temp);
                    }

                    DetailsStudentRequestModel details = new DetailsStudentRequestModel() { _student = student, _issue = issue.OrderByDescending(i => i.Status) };
                    return View(details);
                }
                else
                {
                    return RedirectToAction("Index", "Student");
                }
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index", "Student");
            }
        }

        //
        // GET: /Student/Create
        /// <summary>
        /// Grabs required data data to populate the ViewBag
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            ViewBag.campus = new SelectList(db.campus, "cname", "cname");
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname");

            //var program = db.programs;
            //List<String> collection = new List<String>();
            //foreach (program pcode in program)
            //{
            //    collection.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
            //}
            //ViewBag.programcode = new SelectList(collection);
            student student = new student() { enrolled = true, fulltimestatus = true };
            return View(student);
        }

        //
        // POST: /Student/Create
        /// <summary>
        /// Posts Create request
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.students.AddObject(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
                ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
                return View(student);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "Student");
            }
        }

        //
        // GET: /Student/Edit/5
        /// <summary>
        /// Grabs required data for Edit View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            try
            {
                if (id != null)
                {
                    student student = db.students.Single(s => s.studentid == id);
                    ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
                    ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
                    //var program = db.programs;
                    //List<String> collection = new List<String>();
                    //foreach (program pcode in program)
                    //{
                    //    collection.Add(pcode.programcode.ToString().Trim() + " - " + pcode.programname.ToString().Trim());
                    //}
                    //ViewBag.programcode = new SelectList(collection);
                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "Student");
                }
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index", "Student");
            }
        }

        //
        // POST: /Student/Edit/5
        /// <summary>
        /// Submits Edit to the Database
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(student student)
        {
            if (ModelState.IsValid)
            {
                db.students.Attach(student);
                db.ObjectStateManager.ChangeObjectState(student, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + student.studentid);
            }
            ViewBag.campus = new SelectList(db.campus, "cname", "cname", student.campus);
            ViewBag.programcode = new SelectList(db.programs, "programcode", "programname", student.programcode);
            return View(student);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
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