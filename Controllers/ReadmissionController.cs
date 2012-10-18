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
    public class ReadmissionController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /Readmission/

        /// <summary>
        /// Not Really in use.
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            var applicationforreadmissions = db.applicationForReadmissions.Include("issue");
            return View(applicationforreadmissions.ToList());
        }

        //
        // GET: /Readmission/Details/5
        /// <summary>
        /// Displays all information to the user. Acts as Main view for the form.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationforreadmission.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsReadmissionForm Model = new DetailsReadmissionForm() { _applicationForReadmission = applicationforreadmission, _student = student, _note = db.notes.Include("employee").Where(note => note.formid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name), _date = DateTime.Now };
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
        /// <param name="_model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Details(DetailsReadmissionForm _model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Guid? id = _model._NewNote.formid;
                    _model._NewNote.noteid = Guid.NewGuid();
                    _model._NewNote.employeeid = User.Identity.Name;
                    _model._NewNote.dates = DateTime.Now;
                    _model._NewNote.controller = "Readmission";
                    db.notes.AddObject(_model._NewNote);
                    db.SaveChanges();

                    applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationforreadmission.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsReadmissionForm Model = new DetailsReadmissionForm() { _applicationForReadmission = applicationforreadmission, _student = student, _note = db.notes.Include("employee").Where(note => note.formid == id).OrderByDescending(f => f.dates), _employee = db.employees.Single(e => e.employeeid == User.Identity.Name), _date = DateTime.Now };
                    return View(Model);

                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Issue");
                }
            }

            return View(_model);
        }

        //
        // GET: /Readmission/Create
        /// <summary>
        /// Create view for readmission form. Ensures that Model is properly populated with data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Create(Guid id)
        {
            try
            {
                if (id != null)
                {
                    List<campu> _campus = new List<campu>();
                    _campus.Add(new campu() { cname = "Please Select a Campus" });
                    _campus.AddRange(db.campus);
                    applicationForReadmission applicationForReadmission = new applicationForReadmission() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateReadmissionForm Model = new CreateReadmissionForm() { _applicationForReadmission = applicationForReadmission, _student = student, _campus = _campus };
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
        // POST: /Readmission/Create
        /// <summary>
        /// submits create request to the database.
        /// </summary>
        /// <param name="_CreateReadmissionForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(CreateReadmissionForm _CreateReadmissionForm)
        {
            if (ModelState.IsValid)
            {
                if (_CreateReadmissionForm._applicationForReadmission.term == "Please Select") _CreateReadmissionForm._applicationForReadmission.term = null;
                if (_CreateReadmissionForm._applicationForReadmission.recommendedsemyear == "Please Select") _CreateReadmissionForm._applicationForReadmission.recommendedsemyear = null;
                if (_CreateReadmissionForm._applicationForReadmission.recomendedcampus == "Please Select a Campus") _CreateReadmissionForm._applicationForReadmission.recomendedcampus = null;
                _CreateReadmissionForm._applicationForReadmission.readmissionid = Guid.NewGuid();
                db.applicationForReadmissions.AddObject(_CreateReadmissionForm._applicationForReadmission);
                db.SaveChanges();
                return RedirectToAction("Details/" + _CreateReadmissionForm._applicationForReadmission.readmissionid);
            }

            return View(_CreateReadmissionForm);
        }

        //
        // GET: /Readmission/Edit/5
        /// <summary>
        /// Edit view for readmission form. Ensures that Model is properly populated with data.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationforreadmission.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    EditReadmissionForm Model = new EditReadmissionForm() { _applicationForReadmission = applicationforreadmission, _student = student, _campus = db.campus };
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
        // POST: /Readmission/Edit/5
        /// <summary>
        /// Submits Edit request to the Database.
        /// </summary>
        /// <param name="_EditReadmissionForm"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(EditReadmissionForm _EditReadmissionForm)
        {
            if (ModelState.IsValid)
            {
                if (_EditReadmissionForm._applicationForReadmission.term == "Please Select") _EditReadmissionForm._applicationForReadmission.term = null;
                if (_EditReadmissionForm._applicationForReadmission.recommendedsemyear == "Please Select") _EditReadmissionForm._applicationForReadmission.recommendedsemyear = null;
                if (_EditReadmissionForm._applicationForReadmission.recomendedcampus == "Please Select a Campus") _EditReadmissionForm._applicationForReadmission.recomendedcampus = null;
                db.applicationForReadmissions.Attach(_EditReadmissionForm._applicationForReadmission);
                db.ObjectStateManager.ChangeObjectState(_EditReadmissionForm._applicationForReadmission, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + _EditReadmissionForm._applicationForReadmission.readmissionid);
            }

            return View(_EditReadmissionForm);
        }

        //
        // GET: /Readmission/Delete/5
        /// <summary>
        /// Not In use
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            return View(applicationforreadmission);
        }

        //
        // POST: /Readmission/Delete/5
        /// <summary>
        /// Not In use will be in use in the future.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            db.applicationForReadmissions.DeleteObject(applicationforreadmission);
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