using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdvisementSys.Models;

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

        public ViewResult Details(Guid id)
        {
            part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
            return View(part_timeand_oradditionalcourseregistrationform);
        }

        //
        // GET: /CourseRegistration/Create

        public ActionResult Create()
        {
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid");
            return View();
        } 

        //
        // POST: /CourseRegistration/Create

        [HttpPost]
        public ActionResult Create(part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform)
        {
            if (ModelState.IsValid)
            {
                part_timeand_oradditionalcourseregistrationform.registrationid = Guid.NewGuid();
                db.part_timeAnd_orAdditionalCourseRegistrationForm.AddObject(part_timeand_oradditionalcourseregistrationform);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", part_timeand_oradditionalcourseregistrationform.issueid);
            return View(part_timeand_oradditionalcourseregistrationform);
        }
        
        //
        // GET: /CourseRegistration/Edit/5
 
        public ActionResult Edit(Guid id)
        {
            part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform = db.part_timeAnd_orAdditionalCourseRegistrationForm.Single(p => p.registrationid == id);
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", part_timeand_oradditionalcourseregistrationform.issueid);
            return View(part_timeand_oradditionalcourseregistrationform);
        }

        //
        // POST: /CourseRegistration/Edit/5

        [HttpPost]
        public ActionResult Edit(part_timeAnd_orAdditionalCourseRegistrationForm part_timeand_oradditionalcourseregistrationform)
        {
            if (ModelState.IsValid)
            {
                db.part_timeAnd_orAdditionalCourseRegistrationForm.Attach(part_timeand_oradditionalcourseregistrationform);
                db.ObjectStateManager.ChangeObjectState(part_timeand_oradditionalcourseregistrationform, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.issueid = new SelectList(db.issues, "issueid", "studentid", part_timeand_oradditionalcourseregistrationform.issueid);
            return View(part_timeand_oradditionalcourseregistrationform);
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