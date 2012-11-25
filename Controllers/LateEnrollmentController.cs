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
    public class LateEnrollmentController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /LateEnrollment/
        /// <summary>
        /// Not in use
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var requestforlateenrolments = db.requestForLateEnrolments.Include("issue");
            return View(requestforlateenrolments.ToList());
        }

        //
        // GET: /LateEnrollment/Details/5
        /// <summary>
        /// Gets a single entry by an ID
        /// </summary>
        /// <param name="id">EnrollementID</param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
                    issue issue = db.issues.Single(i => i.issueid == requestforlateenrolment.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsLateEnrollementModel Model = new DetailsLateEnrollementModel() { _requestForLateEnrolment = requestforlateenrolment, _student = student, _note = db.notes.Include("employee").Where(note => note.formid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name), _date = DateTime.Now };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(DetailsLateEnrollementModel _model)
        {

            if (ModelState.IsValid)
            {
            try
            {
                Guid? id = _model._NewNote.formid;
                _model._NewNote.noteid = Guid.NewGuid();
                _model._NewNote.employeeid = User.Identity.Name;
                _model._NewNote.dates = DateTime.Now;
                _model._NewNote.controller = "LateEnrollment";
                db.notes.AddObject(_model._NewNote);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = id });

            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
            }

            return View(_model);
        }

        //
        // GET: /LateEnrollment/Create
        /// <summary>
        /// Gets all data required for Date View
        /// </summary>
        /// <param name="id">IssueID</param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            try
            {
                if (id != null)
                {
                    requestForLateEnrolment requestForLateEnrolment = new requestForLateEnrolment() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateLateEnrollementModel Model = new CreateLateEnrollementModel() { _requestForLateEnrolment = requestForLateEnrolment, _student = student };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        } 

        //
        // POST: /LateEnrollment/Create
        /// <summary>
        /// Submits the requests
        /// </summary>
        /// <param name="_CreateLateEnrollementModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateLateEnrollementModel _CreateLateEnrollementModel)
        {
            if (ModelState.IsValid)
            {
                _CreateLateEnrollementModel._requestForLateEnrolment.enrolementid = Guid.NewGuid();
                db.requestForLateEnrolments.AddObject(_CreateLateEnrollementModel._requestForLateEnrolment);
                db.SaveChanges();
                return RedirectToAction("Details/" + _CreateLateEnrollementModel._requestForLateEnrolment.enrolementid);  
            }

            return View(_CreateLateEnrollementModel);
        }
        
        //
        // GET: /LateEnrollment/Edit/5
        /// <summary>
        /// Generates Edit View a grabs all required data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id != null)
                {
                    requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
                    issue issue = db.issues.Single(i => i.issueid == requestforlateenrolment.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    EditLateEnrollementModel Model = new EditLateEnrollementModel() { _requestForLateEnrolment = requestforlateenrolment, _student = student };
                    return View(Model);
                }
                else
                {
                    return RedirectToAction("Index", "Issue");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Issue");
            }
        }

        //
        // POST: /LateEnrollment/Edit/5
        /// <summary>
        /// Submits Edit Request
        /// </summary>
        /// <param name="_EditLateEnrollementModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditLateEnrollementModel _EditLateEnrollementModel)
        {
            if (ModelState.IsValid)
            {
                db.requestForLateEnrolments.Attach(_EditLateEnrollementModel._requestForLateEnrolment);
                db.ObjectStateManager.ChangeObjectState(_EditLateEnrollementModel._requestForLateEnrolment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + _EditLateEnrollementModel._requestForLateEnrolment.enrolementid);
            }

            return View(_EditLateEnrollementModel);
        }

        //
        // GET: /LateEnrollment/Delete/5
        /// <summary>
        /// Not in use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            return View(requestforlateenrolment);
        }

        //
        // POST: /LateEnrollment/Delete/5
        /// <summary>
        /// For future implementations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            requestForLateEnrolment requestforlateenrolment = db.requestForLateEnrolments.Single(r => r.enrolementid == id);
            db.requestForLateEnrolments.DeleteObject(requestforlateenrolment);
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