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
    public class CourseRegistrationController : Controller
    {
        private Entities db = new Entities();

        //
        // GET: /CourseRegistration/

        public ViewResult Index()
        {
            var part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Include("issue");
            return View(part_timeand_oradditionalcourseregistrationform.ToList());
        }

        //
        // GET: /CourseRegistration/Details/5

        public ActionResult Details(Guid id)
        {
            try
            {
                if (id == null)
                {
                    part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
                    issue issue = db.issues.Single(i => i.issueid == part_timeand_oradditionalcourseregistrationform.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    DetailsCourseRegistrationModel Model = new DetailsCourseRegistrationModel() { _part_timeAnd_orAdditionalCourseRegistrationForm = part_timeand_oradditionalcourseregistrationform, _student = student };
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
        // GET: /CourseRegistration/Create

        public ActionResult Create(Guid id)
        {
            try
            {
                if (id == null)
                {
                    part_timeAnd_orAdditionalCourseRegistrationForm part_timeAnd_orAdditionalCourseRegistrationForm = new part_timeAnd_orAdditionalCourseRegistrationForm() { issueid = id, date = DateTime.Now };
                    issue issue = db.issues.Single(i => i.issueid == id);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    CreateCourseRegistrationModel Model = new CreateCourseRegistrationModel() { _part_timeAnd_orAdditionalCourseRegistrationForm = part_timeAnd_orAdditionalCourseRegistrationForm, _student = student };
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
        // POST: /CourseRegistration/Create

        [HttpPost]
        public ActionResult Create(CreateCourseRegistrationModel _CreateCourseRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                _CreateCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm.registrationid = Guid.NewGuid();
                db.part_timeAnd_orAdditionalCourseRegistrationForm.AddObject(_CreateCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm);
                db.SaveChanges();
                return RedirectToAction("Details/" + _CreateCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm.registrationid);  
            }

            return View(_CreateCourseRegistrationModel);
        }
        
        //
        // GET: /CourseRegistration/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            try
            {
                if (id == null)
                {
                    part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
                    issue issue = db.issues.Single(i => i.issueid == part_timeand_oradditionalcourseregistrationform.issueid);
                    student student = db.students.Include("program").Single(s => s.studentid == issue.studentid);
                    EditCourseRegistrationModel Model = new EditCourseRegistrationModel() { _part_timeAnd_orAdditionalCourseRegistrationForm = part_timeand_oradditionalcourseregistrationform, _student = student };
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
        // POST: /CourseRegistration/Edit/5

        [HttpPost]
        public ActionResult Edit(EditCourseRegistrationModel _EditCourseRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                db.part_timeAnd_orAdditionalCourseRegistrationForm.Attach(_EditCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm);
                db.ObjectStateManager.ChangeObjectState(_EditCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Details/" + _EditCourseRegistrationModel._part_timeAnd_orAdditionalCourseRegistrationForm.registrationid);
            }

            return View(_EditCourseRegistrationModel);
        }

        //
        // GET: /CourseRegistration/Delete/5
 
        public ActionResult Delete(Guid id)
        {
            part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
            return View(part_timeand_oradditionalcourseregistrationform);
        }

        //
        // POST: /CourseRegistration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
            db.part_timeAnd_orAdditionalCourseRegistrationForm.DeleteObject(part_timeand_oradditionalcourseregistrationform);
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