using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;
using AdvisementSys.Models.Request;

namespace AdvisementSys.Controllers
{
    public class IssueController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Issue/

        public ViewResult Index()
        {
            var issues = db.issues.Include("student");
            IndexIssueRequestModel Model = new IndexIssueRequestModel() { _issue = issues, _employee = db.employees, _catagories = db.catagories, _date1 = DateTime.Now.ToShortDateString(), _date2 = DateTime.Now.ToShortDateString() };
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(IndexIssueRequestModel Model)
        {
            var issues = db.issues.Include("student");
            IEnumerable<issue> _issues = issues;
            if (Model._name != "")
                _issues = _issues.Where(i => i.issuename.Trim().ToUpper().Contains(Model._name.Trim().ToUpper()));
            Model._issue = _issues;
            return View(Model);
        }

        //
        // GET: /Issue/Details/5

        public ViewResult Details(Guid id)
        {
            issue issue = db.issues.Include("student").Single(i => i.issueid == id);
            student student = db.students.Single(i => i.studentid == issue.studentid);
            program program = db.programs.Single(i => i.programcode == issue.student.programcode);
            DetailsIssueRequestModel model = new DetailsIssueRequestModel() { _issue = issue, _student = student, _program = program };
            List<Forms> Forms = new List<Forms>();
            IEnumerable<applicationForReadmission> applicationForReadmission = db.applicationForReadmissions.Where(i => i.issueid == issue.issueid);
            foreach (applicationForReadmission form in applicationForReadmission)
            {
                Forms _Forms = new Forms();
                _Forms.Date = form.date;
                _Forms.FormType = "Application For Readmission";
                _Forms.Status = form.status;
                _Forms.Controller = "Readmission";
                _Forms.FormID = form.readmissionid;

                Forms.Add(_Forms);
            }
            IEnumerable<applicationForTermOrCompleteProgramWithdrawal> applicationForTermOrCompleteProgramWithdrawal = db.applicationForTermOrCompleteProgramWithdrawals.Where(i => i.issueid == issue.issueid);
            foreach (applicationForTermOrCompleteProgramWithdrawal form in applicationForTermOrCompleteProgramWithdrawal)
            {
                Forms _Forms = new Forms();
                _Forms.Date = form.date;
                _Forms.FormType = "Application For Term Or Complete Program Withdrawal";
                _Forms.Status = form.status;
                _Forms.Controller = "ProgramWithdrawal";
                _Forms.FormID = form.withdrawid;

                Forms.Add(_Forms);
            }
            IEnumerable<part_timeAnd_orAdditionalCourseRegistrationForm> part_timeAnd_orAdditionalCourseRegistrationForm = db.part_timeAnd_orAdditionalCourseRegistrationForm.Where(i => i.issueid == issue.issueid);
            foreach (part_timeAnd_orAdditionalCourseRegistrationForm form in part_timeAnd_orAdditionalCourseRegistrationForm)
            {
                Forms _Forms = new Forms();
                _Forms.Date = form.date;
                _Forms.FormType = "Part Time And/or Additional Course Registration Form";
                _Forms.Status = form.status;
                _Forms.Controller = "CourseRegistration";
                _Forms.FormID = form.registrationid;

                Forms.Add(_Forms);
            }
            IEnumerable<probationaryContractPlan> probationaryContractPlan = db.probationaryContractPlans.Where(i => i.issueid == issue.issueid);
            foreach (probationaryContractPlan form in probationaryContractPlan)
            {
                Forms _Forms = new Forms();
                _Forms.Date = form.date;
                _Forms.FormType = "Probationary Contract Plan";
                _Forms.Status = form.status;
                _Forms.Controller = "ProbationaryContract";
                _Forms.FormID = form.advisementid;

                Forms.Add(_Forms);
            }
            IEnumerable<requestForLateEnrolment> requestForLateEnrolment = db.requestForLateEnrolments.Where(i => i.issueid == issue.issueid);
            foreach (requestForLateEnrolment form in requestForLateEnrolment)
            {
                Forms _Forms = new Forms();
                _Forms.Date = form.date;
                _Forms.FormType = "Request For Late Enrollment Form";
                _Forms.Status = form.status;
                _Forms.Controller = "LateEnrollment";
                _Forms.FormID = form.enrolementid;

                Forms.Add(_Forms);
            }

            model._Forms = Forms.OrderByDescending(f => f.Date);
            return View(model);
        }

        //
        // GET: /Issue/Create

        public ActionResult Create(String id)
        {
            student student = db.students.Single(i => i.studentid == id);
            issue issue = new issue() { studentid = id, date = DateTime.Now };
            program program = db.programs.Single(i => i.programcode == student.programcode);
            IEnumerable<catagory> catagory = db.catagories;
            IEnumerable<employee> employee = db.employees;
            CreateIssueRequestModel model = new CreateIssueRequestModel() { _issue = issue, _student = student, _program = program, _catagory = catagory, _employee = employee };
            return View(model);
        }

        //
        // POST: /Issue/Create

        [HttpPost]
        public ActionResult Create(CreateIssueRequestModel model, String id)
        {
            if (ModelState.IsValid)
            {
                model._issue.issueid = Guid.NewGuid();
                model._issue.studentid = id;
                db.issues.AddObject(model._issue);
                db.SaveChanges();
                return RedirectToAction("Details/" + id, "Student");
            }

            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", model._issue.studentid);
            return View(model);
        }

        //
        // GET: /Issue/Edit/5

        public ActionResult Edit(Guid id)
        {
            issue issue = db.issues.Include("student").Single(i => i.issueid == id);
            student student = db.students.Single(i => i.studentid == issue.studentid);
            program program = db.programs.Single(i => i.programcode == student.programcode);
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", issue.studentid);
            IEnumerable<catagory> catagory = db.catagories;
            IEnumerable<employee> employee = db.employees;
            EditIssueRequestModel model = new EditIssueRequestModel() { _issue = issue, _program = program, _student = student, _catagory = catagory, _employee = employee };
            return View(model);
        }

        //
        // POST: /Issue/Edit/5

        [HttpPost]
        public ActionResult Edit(EditIssueRequestModel model)
        {
            if (ModelState.IsValid)
            {
                db.issues.Attach(model._issue);
                db.ObjectStateManager.ChangeObjectState(model._issue, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + model._issue.issueid);
            }
            ViewBag.studentid = new SelectList(db.students, "studentid", "fname", model._issue.studentid);
            return View(model._issue);
        }

        //
        // GET: /Issue/Delete/5

        public ActionResult Delete(Guid id)
        {
            issue issue = db.issues.Single(i => i.issueid == id);
            return View(issue);
        }

        //
        // POST: /Issue/Delete/5

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
    }
}