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

        public ViewResult Index()
        {
            var applicationforreadmissions = db.applicationForReadmissions.Include("issue");
            return View(applicationforreadmissions.ToList());
        }

        //
        // GET: /Readmission/Details/5

        public ActionResult Details(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
                    issue issue = db.issues.Single(i => i.issueid == applicationforreadmission.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsReadmissionForm Model = new DetailsReadmissionForm() { _applicationForReadmission = applicationforreadmission, _student = student };
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
        // GET: /Readmission/Create

        public ActionResult Create(Guid id)
        {
            try
            {
                if (id != null)
                {
                    applicationForReadmission applicationForReadmission = new applicationForReadmission() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateReadmissionForm Model = new CreateReadmissionForm() { _applicationForReadmission = applicationForReadmission, _student = student, _campus = db.campus };
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
 
        public ActionResult Delete(Guid id)
        {
            applicationForReadmission applicationforreadmission = db.applicationForReadmissions.Single(a => a.readmissionid == id);
            return View(applicationforreadmission);
        }

        //
        // POST: /Readmission/Delete/5

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